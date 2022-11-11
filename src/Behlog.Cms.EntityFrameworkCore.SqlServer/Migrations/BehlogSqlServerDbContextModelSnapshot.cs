﻿// <auto-generated />
using System;
using Behlog.Cms.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Behlog.Cms.EntityFrameworkCore.SqlServer.Migrations
{
    [DbContext(typeof(BehlogSqlServerDbContext))]
    partial class BehlogSqlServerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Behlog.Cms.Domain.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("AuthorUserId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<int>("BodyTypeId")
                        .HasColumnType("int");

                    b.Property<Guid>("ContentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedByIp")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CreatedByUserId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime?>("LastStatusChangedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastUpdatedByIp")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastUpdatedByUserId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("WebUrl")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(2000)");

                    b.HasKey("Id");

                    b.HasIndex("BodyTypeId");

                    b.HasIndex("ContentId");

                    b.HasIndex("StatusId");

                    b.ToTable("Comment", (string)null);
                });

            modelBuilder.Entity("Behlog.Cms.Domain.CommentStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CommentStatus");
                });

            modelBuilder.Entity("Behlog.Cms.Domain.Content", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AltTitle")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("AuthorUserId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Body")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("nTEXT");

                    b.Property<int>("BodyType")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<Guid>("ContentTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedByIp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedByUserId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IconName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("LangCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(10)");

                    b.Property<Guid>("LangId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("LastStatusChangedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastUpdatedByIp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastUpdatedByUserId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("OrderNum")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("PublishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ViewPath")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<Guid>("WebsiteId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ContentTypeId");

                    b.HasIndex("LangId");

                    b.ToTable("Content", (string)null);
                });

            modelBuilder.Entity("Behlog.Cms.Domain.ContentBodyType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ContentBodyType");
                });

            modelBuilder.Entity("Behlog.Cms.Domain.ContentCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AltTitle")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(500)");

                    b.Property<Guid?>("ContentTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<Guid>("LangId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LanguageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("LastStatusChangedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(256)");

                    b.Property<Guid?>("WebsiteId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ContentTypeId");

                    b.HasIndex("LanguageId");

                    b.ToTable("ContentCategory", (string)null);
                });

            modelBuilder.Entity("Behlog.Cms.Domain.ContentCategoryItem", b =>
                {
                    b.Property<Guid>("ContentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ContentId", "CategoryId");

                    b.ToTable("ContentCategoryItem", (string)null);
                });

            modelBuilder.Entity("Behlog.Cms.Domain.ContentTag", b =>
                {
                    b.Property<Guid>("ContentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TagId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ContentId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("ContentTag", (string)null);
                });

            modelBuilder.Entity("Behlog.Cms.Domain.FileUpload", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AltTitle")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("AlternateFilePath")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("CreatedByIp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedByUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<long>("FileSize")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("LastStatusChangedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastUpdatedByIp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastUpdatedByUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(4000)");

                    b.HasKey("Id");

                    b.ToTable("FileUpload", (string)null);
                });

            modelBuilder.Entity("Behlog.Cms.Domain.Language", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("IsoCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Language", (string)null);
                });

            modelBuilder.Entity("Behlog.Cms.Domain.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedByIp")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CreatedByUserId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LangId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("Id");

                    b.HasIndex("LangId");

                    b.ToTable("Tag", (string)null);
                });

            modelBuilder.Entity("Behlog.Core.ContentType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("LangCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(20)");

                    b.Property<Guid>("LangId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("LastStatusChangedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("SystemName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("LangId");

                    b.ToTable("ContentType", (string)null);
                });

            modelBuilder.Entity("Behlog.Cms.Domain.Comment", b =>
                {
                    b.HasOne("Behlog.Cms.Domain.ContentBodyType", "BodyType")
                        .WithMany()
                        .HasForeignKey("BodyTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Behlog.Cms.Domain.Content", "Content")
                        .WithMany()
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Behlog.Cms.Domain.CommentStatus", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BodyType");

                    b.Navigation("Content");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Behlog.Cms.Domain.Content", b =>
                {
                    b.HasOne("Behlog.Core.ContentType", "ContentType")
                        .WithMany()
                        .HasForeignKey("ContentTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Behlog.Cms.Domain.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LangId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.OwnsMany("Behlog.Cms.Domain.ContentFile", "Files", b1 =>
                        {
                            b1.Property<Guid>("ContentId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("FileId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasMaxLength(2000)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(2000)");

                            b1.Property<string>("FileName")
                                .IsRequired()
                                .HasMaxLength(2000)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(2000)");

                            b1.Property<string>("Title")
                                .IsRequired()
                                .HasMaxLength(1000)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(1000)");

                            b1.HasKey("ContentId", "FileId");

                            b1.ToTable("ContentFile", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("ContentId");
                        });

                    b.OwnsMany("Behlog.Cms.Domain.ContentLike", "Likes", b1 =>
                        {
                            b1.Property<long>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<long>("Id"), 1L, 1);

                            b1.Property<Guid>("ContentId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("CreatedDate")
                                .HasColumnType("datetime2");

                            b1.Property<string>("IpAddress")
                                .IsRequired()
                                .HasMaxLength(100)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("SessionId")
                                .IsRequired()
                                .HasMaxLength(1000)
                                .HasColumnType("nvarchar(1000)");

                            b1.Property<string>("UserId")
                                .IsRequired()
                                .HasMaxLength(256)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(256)");

                            b1.HasKey("Id");

                            b1.HasIndex("ContentId");

                            b1.ToTable("ContentLike", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("ContentId");
                        });

                    b.OwnsMany("Behlog.Cms.Domain.ContentMeta", "Meta", b1 =>
                        {
                            b1.Property<Guid>("OwnerId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Category")
                                .IsRequired()
                                .HasMaxLength(256)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(256)");

                            b1.Property<Guid>("ContentId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasMaxLength(2000)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(2000)");

                            b1.Property<string>("LangCode")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<Guid?>("LangId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("MetaKey")
                                .IsRequired()
                                .HasMaxLength(256)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(256)");

                            b1.Property<string>("MetaType")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("MetaValue")
                                .IsRequired()
                                .HasMaxLength(4000)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(4000)");

                            b1.Property<int>("OrderNum")
                                .HasColumnType("int");

                            b1.Property<int>("Status")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasDefaultValue(1);

                            b1.Property<string>("Title")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("OwnerId");

                            b1.HasIndex("ContentId");

                            b1.ToTable("ContentMeta", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("ContentId");
                        });

                    b.Navigation("ContentType");

                    b.Navigation("Files");

                    b.Navigation("Language");

                    b.Navigation("Likes");

                    b.Navigation("Meta");
                });

            modelBuilder.Entity("Behlog.Cms.Domain.ContentCategory", b =>
                {
                    b.HasOne("Behlog.Core.ContentType", null)
                        .WithMany()
                        .HasForeignKey("ContentTypeId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Behlog.Cms.Domain.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");
                });

            modelBuilder.Entity("Behlog.Cms.Domain.ContentCategoryItem", b =>
                {
                    b.HasOne("Behlog.Cms.Domain.Content", "Content")
                        .WithMany("Categories")
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Behlog.Cms.Domain.ContentCategory", "Category")
                        .WithMany("Contents")
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Content");
                });

            modelBuilder.Entity("Behlog.Cms.Domain.ContentTag", b =>
                {
                    b.HasOne("Behlog.Cms.Domain.Content", "Content")
                        .WithMany()
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Behlog.Cms.Domain.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Content");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("Behlog.Cms.Domain.Tag", b =>
                {
                    b.HasOne("Behlog.Cms.Domain.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LangId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Language");
                });

            modelBuilder.Entity("Behlog.Core.ContentType", b =>
                {
                    b.HasOne("Behlog.Cms.Domain.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LangId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Language");
                });

            modelBuilder.Entity("Behlog.Cms.Domain.Content", b =>
                {
                    b.Navigation("Categories");
                });

            modelBuilder.Entity("Behlog.Cms.Domain.ContentCategory", b =>
                {
                    b.Navigation("Contents");
                });
#pragma warning restore 612, 618
        }
    }
}
