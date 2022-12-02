using System.Globalization;
using Behlog.Cms.Domain;
using Behlog.Core.Models;
using Behlog.Extensions;

namespace Behlog.Cms.FileUploads.Internal;


internal class FileUploader
{
    private readonly BehlogOptions _options;
    private readonly IWebHostEnvironment _env;
    private string _uploadRoot;

    public FileUploader(BehlogOptions options, IWebHostEnvironment env)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _env = env ?? throw new ArgumentNullException(nameof(env));
        _uploadRoot = $"{_env.ContentRootPath}/uploads";
    }

    private string createDirectory(string contentType)
    {
        if (!Directory.Exists(_uploadRoot))
        {
            Directory.CreateDirectory(_uploadRoot);
        }

        var contentTypeDirPath = $"{_uploadRoot}/{contentType}";
        if (contentType.IsNotNullOrEmpty() && !Directory.Exists(contentTypeDirPath))
        {
            Directory.CreateDirectory(contentTypeDirPath);
        }

        return contentTypeDirPath;
    }
    
    public FileUploaderResult Upload(IFormFile fileData, string contentType, FileType fileType)
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


    private FileUploaderResult getResult(
        IFormFile fileData, string contentType, FileType fileType)
    {
        fileData.ThrowExceptionIfArgumentIsNull(nameof(fileData));
        //TODO : need to check if fileData length is zero !?

        var root = createDirectory(contentType);
        var fileName = getFileName(fileData, root);
        var result = new FileUploaderResult
        {
            FileName = fileName,
            FilePath = $"{root}/{fileName}",
            FileSize = fileData.Length,
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
        var filePath = $"{targetDirectory}/{result}";
        var fileName = Path.GetFileNameWithoutExtension(result);
        var fileExt = Path.GetExtension(result);
        
        int i = 1;
        while (File.Exists(filePath))
        {
            result = $"{fileName}_{i}{fileExt}";
            filePath = $"{targetDirectory}/{result}";
        }

        return result;
    }
    
    private BehlogFileUploadItemConfig? getConfig(FileType fileType)
    {
        if (_options.FileUploadsConfig == null || _options.FileUploadsConfig.Length == 0)
            return null;

        return _options.FileUploadsConfig.FirstOrDefault(
            _ => _.Name.ToUpperInvariant() == fileType.Name.ToUpperInvariant());
    }
}