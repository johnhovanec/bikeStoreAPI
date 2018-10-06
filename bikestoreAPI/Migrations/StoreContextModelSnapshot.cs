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

            modelBuilder.Entity("bikestoreAPI.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<bool>("Default");

                    b.Property<string>("Phone");

                    b.Property<string>("State");

                    b.Property<string>("Street");

                    b.Property<string>("Street2");

                    b.Property<int>("Type");

                    b.Property<int?>("UserId");

                    b.Property<string>("Zip");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("bikestoreAPI.Models.BackOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateExpected");

                    b.Property<int?>("ProductId");

                    b.Property<DateTime?>("TimeStamp");

                    b.HasKey("Id");

                    b.HasIndex("ProductId")
                        .IsUnique()
                        .HasFilter("[ProductId] IS NOT NULL");

                    b.ToTable("BackOrder");
                });

            modelBuilder.Entity("bikestoreAPI.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AddressId");

                    b.Property<int?>("PaymentMethodId");

                    b.Property<decimal>("ShippingCost");

                    b.Property<int?>("ShippingMethodId");

                    b.Property<int?>("ShoppingCartId");

                    b.Property<string>("SourceCode");

                    b.Property<decimal>("Subtotal");

                    b.Property<decimal>("Tax");

                    b.Property<DateTime?>("TimeStamp");

                    b.Property<decimal>("Total");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("PaymentMethodId");

                    b.HasIndex("ShippingMethodId");

                    b.HasIndex("ShoppingCartId");

                    b.HasIndex("UserId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("bikestoreAPI.Models.OrderProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color");

                    b.Property<int?>("OrderId");

                    b.Property<int?>("ProductId");

                    b.Property<int?>("Quantity");

                    b.Property<string>("Size");

                    b.Property<decimal?>("UnitPrice");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProduct");
                });

            modelBuilder.Entity("bikestoreAPI.Models.PaymentMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BankName");

                    b.Property<string>("CVVNumber");

                    b.Property<string>("CardNumber");

                    b.Property<int>("CardType");

                    b.Property<bool>("Default");

                    b.Property<string>("ExpDate");

                    b.Property<string>("NameOnCard");

                    b.Property<DateTime?>("TimeStamp");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("PaymentMethod");
                });

            modelBuilder.Entity("bikestoreAPI.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color");

                    b.Property<string>("Description");

                    b.Property<int?>("HomePageIndex");

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

            modelBuilder.Entity("bikestoreAPI.Models.ShippingMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<int>("Name");

                    b.Property<decimal>("Rate");

                    b.Property<string>("Restrictions");

                    b.HasKey("Id");

                    b.ToTable("ShippingMethod");
                });

            modelBuilder.Entity("bikestoreAPI.Models.ShoppingCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CartTimeStamp");

                    b.Property<bool?>("OrderPlaced");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ShoppingCart");
                });

            modelBuilder.Entity("bikestoreAPI.Models.ShoppingCartProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color");

                    b.Property<int?>("ProductId");

                    b.Property<int?>("Quantity");

                    b.Property<int?>("ShoppingCartId");

                    b.Property<string>("Size");

                    b.Property<decimal?>("UnitPrice");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("ShoppingCartId");

                    b.ToTable("ShoppingCartProduct");
                });

            modelBuilder.Entity("bikestoreAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("FName");

                    b.Property<int?>("FailedLogins");

                    b.Property<string>("LName");

                    b.Property<DateTime?>("LastLogin");

                    b.Property<string>("MName");

                    b.Property<string>("Password");

                    b.Property<string>("Phone");

                    b.Property<string>("PwdResetToken");

                    b.Property<DateTime?>("PwdTimeStamp");

                    b.Property<DateTime?>("PwdTokenTimeStamp");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("bikestoreAPI.Models.Address", b =>
                {
                    b.HasOne("bikestoreAPI.Models.User", "User")
                        .WithMany("Address")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("bikestoreAPI.Models.BackOrder", b =>
                {
                    b.HasOne("bikestoreAPI.Models.Product", "Product")
                        .WithOne("BackOrder")
                        .HasForeignKey("bikestoreAPI.Models.BackOrder", "ProductId");
                });

            modelBuilder.Entity("bikestoreAPI.Models.Order", b =>
                {
                    b.HasOne("bikestoreAPI.Models.Address", "Address")
                        .WithMany("Order")
                        .HasForeignKey("AddressId");

                    b.HasOne("bikestoreAPI.Models.PaymentMethod", "PaymentMethod")
                        .WithMany("Order")
                        .HasForeignKey("PaymentMethodId");

                    b.HasOne("bikestoreAPI.Models.ShippingMethod", "ShippingMethod")
                        .WithMany("Order")
                        .HasForeignKey("ShippingMethodId");

                    b.HasOne("bikestoreAPI.Models.ShoppingCart", "ShoppingCart")
                        .WithMany("Order")
                        .HasForeignKey("ShoppingCartId");

                    b.HasOne("bikestoreAPI.Models.User", "User")
                        .WithMany("Order")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("bikestoreAPI.Models.OrderProduct", b =>
                {
                    b.HasOne("bikestoreAPI.Models.Order", "Order")
                        .WithMany("OrderProduct")
                        .HasForeignKey("OrderId");

                    b.HasOne("bikestoreAPI.Models.Product", "Product")
                        .WithMany("OrderProduct")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("bikestoreAPI.Models.PaymentMethod", b =>
                {
                    b.HasOne("bikestoreAPI.Models.User", "User")
                        .WithMany("PaymentMethod")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("bikestoreAPI.Models.ShoppingCart", b =>
                {
                    b.HasOne("bikestoreAPI.Models.User", "User")
                        .WithMany("ShoppingCart")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("bikestoreAPI.Models.ShoppingCartProduct", b =>
                {
                    b.HasOne("bikestoreAPI.Models.Product", "Product")
                        .WithMany("ShoppingCartProduct")
                        .HasForeignKey("ProductId");

                    b.HasOne("bikestoreAPI.Models.ShoppingCart", "ShoppingCart")
                        .WithMany("ShoppingCartProduct")
                        .HasForeignKey("ShoppingCartId");
                });
#pragma warning restore 612, 618
        }
    }
}
