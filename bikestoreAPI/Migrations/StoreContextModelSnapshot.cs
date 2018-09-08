﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using bikestoreAPI.Models;

namespace bikestoreAPI.Migrations
{
    [DbContext(typeof(StoreContext))]
    partial class StoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("bikestoreAPI.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("Email");

                    b.Property<int?>("FailedLogins");

                    b.Property<DateTime?>("LastLogin");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<string>("State");

                    b.Property<string>("Zip");

                    b.HasKey("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("bikestoreAPI.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color");

                    b.Property<string>("Description");

                    b.Property<string>("ImagePath");

                    b.Property<int?>("InventoryQuantity");

                    b.Property<string>("Manufacturer");

                    b.Property<string>("Model");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.Property<string>("Rating");

                    b.Property<string>("Size");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("bikestoreAPI.Models.ShoppingCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CartTimeStamp");

                    b.Property<int>("CustomerId");

                    b.Property<bool?>("OrderPlaced");

                    b.Property<DateTime?>("OrderPlacedTimeStamp");

                    b.Property<string>("PaymentMethod");

                    b.Property<decimal>("Shipping");

                    b.Property<decimal>("Tax");

                    b.Property<decimal>("Total");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("ShoppingCart");
                });

            modelBuilder.Entity("bikestoreAPI.Models.ShoppingCartProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color");

                    b.Property<int>("ProductId");

                    b.Property<int?>("Quantity");

                    b.Property<int>("ShoppingCartId");

                    b.Property<string>("Size");

                    b.Property<decimal?>("UnitPrice");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("ShoppingCartId");

                    b.ToTable("ShoppingCartProduct");
                });

            modelBuilder.Entity("bikestoreAPI.Models.ShoppingCart", b =>
                {
                    b.HasOne("bikestoreAPI.Models.Customer", "Customer")
                        .WithOne("ShoppingCart")
                        .HasForeignKey("bikestoreAPI.Models.ShoppingCart", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("bikestoreAPI.Models.ShoppingCartProduct", b =>
                {
                    b.HasOne("bikestoreAPI.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("bikestoreAPI.Models.ShoppingCart", "ShoppingCart")
                        .WithMany()
                        .HasForeignKey("ShoppingCartId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
