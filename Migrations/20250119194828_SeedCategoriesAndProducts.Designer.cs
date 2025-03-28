﻿// <auto-generated />
using E_commercePlants.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace E_commercePlants.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250119194828_SeedCategoriesAndProducts")]
    partial class SeedCategoriesAndProducts
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("E_commercePlants.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Slug")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Shirts",
                            Slug = "shirts"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Fruit",
                            Slug = "fruit"
                        });
                });

            modelBuilder.Entity("E_commercePlants.Models.Page", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("Slug")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Body = "This is the home page",
                            Order = 0,
                            Slug = "home",
                            Title = "Home"
                        },
                        new
                        {
                            Id = 2,
                            Body = "This is the about page",
                            Order = 0,
                            Slug = "about",
                            Title = "About"
                        },
                        new
                        {
                            Id = 3,
                            Body = "This is the services page",
                            Order = 0,
                            Slug = "services",
                            Title = "Services"
                        },
                        new
                        {
                            Id = 4,
                            Body = "This is the contact page",
                            Order = 0,
                            Slug = "contact",
                            Title = "Contact"
                        });
                });

            modelBuilder.Entity("E_commercePlants.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(8,2)");

                    b.Property<string>("Slug")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 2,
                            Description = "Juicy apples",
                            Image = "apple1.jpg",
                            Name = "Apples",
                            Price = 1.50m,
                            Slug = "apples"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 2,
                            Description = "Juicy grapefruit",
                            Image = "grapefruit1.jpg",
                            Name = "Grapefruit",
                            Price = 2m,
                            Slug = "grapefruit"
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 2,
                            Description = "Fresh grapes",
                            Image = "grapes1.jpg",
                            Name = "Grapes",
                            Price = 1.80m,
                            Slug = "grapes"
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 2,
                            Description = "Fresh oranges",
                            Image = "orange1.jpg",
                            Name = "Oranges",
                            Price = 1.50m,
                            Slug = "oranges"
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 1,
                            Description = "Nice blue t-shirt",
                            Image = "blue1.jpg",
                            Name = "Blue shirt",
                            Price = 7.99m,
                            Slug = "blue-shirt"
                        },
                        new
                        {
                            Id = 6,
                            CategoryId = 1,
                            Description = "Nice red t-shirt",
                            Image = "red1.jpg",
                            Name = "Red shirt",
                            Price = 8.99m,
                            Slug = "red-shirt"
                        },
                        new
                        {
                            Id = 7,
                            CategoryId = 1,
                            Description = "Nice green t-shirt",
                            Image = "green1.png",
                            Name = "Green shirt",
                            Price = 9.99m,
                            Slug = "green-shirt"
                        },
                        new
                        {
                            Id = 8,
                            CategoryId = 1,
                            Description = "Nice pink t-shirt",
                            Image = "pink1.png",
                            Name = "Pink shirt",
                            Price = 10.99m,
                            Slug = "pink-shirt"
                        });
                });

            modelBuilder.Entity("E_commercePlants.Models.Product", b =>
                {
                    b.HasOne("E_commercePlants.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
