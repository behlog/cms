using Behlog.Cms.Extensions;

namespace Behlog.Cms.FileUploads.Internal;

/// <summary>
/// Helper class to Upload files physically on the server (wwwroot) based on the ContentType.
/// (ContentType refers to <see cref="ContentType"/> not file's actual content-type!)
/// </summary>
public class FileUploader
{
    private readonly BehlogOptions _options;
    private readonly IWebHostEnvironment _env;
    private readonly IBehlogApplicationContext _appContext;
    private string _uploadRoot;
    private string _appBaseUrl;
    private const string _rootDirName = "uploads";

    public FileUploader(BehlogOptions options, IWebHostEnvironment env, IBehlogApplicationContext appContext)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _env = env ?? throw new ArgumentNullException(nameof(env));
        _appContext = appContext ?? throw new ArgumentNullException(nameof(appContext));
        _uploadRoot = $"{_env.WebRootPath}{Path.DirectorySeparatorChar}{_rootDirName}";
        _appBaseUrl = appContext.BaseUrl;
    }

    private string createDirectory(string contentType)
    {
        if (!Directory.Exists(_uploadRoot))
        {
            Directory.CreateDirectory(_uploadRoot);
        }

        var contentTypeDirPath = $"{_uploadRoot}";
        

        if (contentType.IsNotNullOrEmpty())
        {
            if (_uploadRoot.EndsWith(Path.DirectorySeparatorChar))
                contentTypeDirPath += $"{contentType}";
            else
                contentTypeDirPath += $"{Path.DirectorySeparatorChar}{contentType}";

            if(!Directory.Exists(contentTypeDirPath)) {
                Directory.CreateDirectory(contentTypeDirPath);
            }
        }

        return contentTypeDirPath;
    }

    private string getFileUrl(string contentType, string filename)
    {
        if (_appBaseUrl.EndsWith("/"))
            return $"{_appBaseUrl}{_rootDirName}/{contentType}/{filename}";

        return $"{_appBaseUrl}/{_rootDirName}/{contentType}/{filename}";
    }
    
    public FileUploaderResult Upload(IFormFile fileData, string contentType, FileTypeEnum fileType)
    {
        var result = getResult(fileData, contentType, fileType);

        try
        {
            using var stream = new FileStream(result.FilePath, FileMode.Create);
            fileData.CopyTo(stream);
            stream.Flush();
            stream.Close();
        }
        catch (Exception ex)
        {
            return result.WithError($"Error : {ex.GetBaseException().Message}");
        }

        return result.Finished();
    }


    public async Task<FileUploaderResult> UploadAsync(
        IFormFile fileData, string contentType, FileTypeEnum fileType)
    {
        var result = getResult(fileData, contentType, fileType);
        
        try
        {
            using var stream = new FileStream(result.FilePath, FileMode.Create);
            await fileData.CopyToAsync(stream);
            await stream.FlushAsync();
            stream.Close();
        }
        catch (Exception ex)
        {
            return await Task.FromResult(
                result.WithError($"Error : {ex.GetBaseException().Message}")
                );
        }

        return await Task.FromResult(result.Finished());
    }
    

    private FileUploaderResult getResult(
        IFormFile fileData, string contentType, FileTypeEnum fileType)
    {
        fileData.ThrowExceptionIfArgumentIsNull(nameof(fileData));
        if (fileData.Length <= 0)
            throw new BehlogFileUploadException("FileData length cannot be zero or negative.");
        
        var root = createDirectory(contentType);
        var fileName = getFileName(fileData, root);
        if (!root.EndsWith(Path.DirectorySeparatorChar))
            root += $"{Path.DirectorySeparatorChar}";

        var result = new FileUploaderResult
        {
            FileName = fileName,
            FilePath = $"{root}{fileName}",
            FileSize = fileData.Length,
            FileUrl = getFileUrl(contentType, fileName),
            Extension = Path.GetExtension(fileName)
        };

        var config = getConfig(fileType);
        if (config != null)
        {
            if (config.Extensions.IsNotNullOrEmpty() && 
                !config.Extensions.Contains(result.Extension))
            {
                return result.WithError($"FileType '{result.Extension}' not supported.");
            }

            if (config.MaxSize.HasValue && result.FileSize > config.MaxSize.Value)
            {
                return result.WithError($"FileSize exceeded maximum size configured.");
            }
        }

        return result;
    }
    
    private string getFileName(IFormFile fileData, string targetDirectory)
    {
        fileData.ThrowExceptionIfArgumentIsNull(nameof(fileData));

        var result = fileData.FileName;
        if (!targetDirectory.EndsWith(Path.DirectorySeparatorChar))
            targetDirectory += $"{Path.DirectorySeparatorChar}";

        var filePath = $"{targetDirectory}{result}";
        var fileName = Path.GetFileNameWithoutExtension(result);
        var fileExt = Path.GetExtension(result);
        
        int i = 1;
        while (File.Exists(filePath))
        {
            result = $"{fileName}_{i}{fileExt}";
            filePath = $"{targetDirectory}{result}";
        }

        return result;
    }
    
    private BehlogFileUploadItemConfig? getConfig(FileTypeEnum fileType)
    {
        if (_options.FileUploadsConfig == null || _options.FileUploadsConfig.Length == 0)
            return null;

        return _options.FileUploadsConfig.FirstOrDefault(
            _ => _.Name.ToUpperInvariant() == fileType.GetFileTypeName().ToUpperInvariant());
    }
}