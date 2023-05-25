﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApp.Contexts;

#nullable disable

namespace WebApp.Migrations.Product
{
    [DbContext(typeof(ProductContext))]
    partial class ProductContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApp.Models.Entities.PriceEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal?>("DiscountPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsDiscountPrice")
                        .HasColumnType("bit");

                    b.Property<bool>("IsOrdinaryPrice")
                        .HasColumnType("bit");

                    b.Property<bool>("IsOriginalPrice")
                        .HasColumnType("bit");

                    b.Property<decimal?>("OrdinaryPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("OriginalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("ProductPrices");
                });

            modelBuilder.Entity("WebApp.Models.Entities.ProductCategoryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("ProductCategories");
                });

            modelBuilder.Entity("WebApp.Models.Entities.ProductEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<byte[]>("ImageData")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ImageMimeType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProductCategoryEntityId")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SKU")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ProductCategoryEntityId");

                    b.ToTable("ProductItems");
                });

            modelBuilder.Entity("WebApp.Models.Entities.ProductTagEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("TagId");

                    b.ToTable("ProductTags");
                });

            modelBuilder.Entity("WebApp.Models.Entities.TagEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ProductEntityId")
                        .HasColumnType("int");

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductEntityId");

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            TagName = "New"
                        },
                        new
                        {
                            Id = 2,
                            TagName = "Featured"
                        },
                        new
                        {
                            Id = 3,
                            TagName = "Popular"
                        });
                });

            modelBuilder.Entity("WebApp.Models.Entities.PriceEntity", b =>
                {
                    b.HasOne("WebApp.Models.Entities.ProductEntity", "Product")
                        .WithOne("Price")
                        .HasForeignKey("WebApp.Models.Entities.PriceEntity", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WebApp.Models.Entities.ProductCategoryEntity", b =>
                {
                    b.HasOne("WebApp.Models.Entities.ProductEntity", "Product")
                        .WithOne("Category")
                        .HasForeignKey("WebApp.Models.Entities.ProductCategoryEntity", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WebApp.Models.Entities.ProductEntity", b =>
                {
                    b.HasOne("WebApp.Models.Entities.ProductCategoryEntity", null)
                        .WithMany("RelatedProducts")
                        .HasForeignKey("ProductCategoryEntityId");
                });

            modelBuilder.Entity("WebApp.Models.Entities.ProductTagEntity", b =>
                {
                    b.HasOne("WebApp.Models.Entities.ProductEntity", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApp.Models.Entities.TagEntity", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("WebApp.Models.Entities.TagEntity", b =>
                {
                    b.HasOne("WebApp.Models.Entities.ProductEntity", null)
                        .WithMany("Tags")
                        .HasForeignKey("ProductEntityId");
                });

            modelBuilder.Entity("WebApp.Models.Entities.ProductCategoryEntity", b =>
                {
                    b.Navigation("RelatedProducts");
                });

            modelBuilder.Entity("WebApp.Models.Entities.ProductEntity", b =>
                {
                    b.Navigation("Category");

                    b.Navigation("Price");

                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}
