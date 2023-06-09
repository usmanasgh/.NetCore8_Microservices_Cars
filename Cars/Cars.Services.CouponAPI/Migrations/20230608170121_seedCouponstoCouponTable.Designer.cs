﻿// <auto-generated />
using Cars.Services.CouponAPI.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cars.Services.CouponAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230608170121_seedCouponstoCouponTable")]
    partial class seedCouponstoCouponTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.4.23259.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Cars.Services.CouponAPI.Models.Coupon", b =>
                {
                    b.Property<int>("CouponId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CouponId"));

                    b.Property<string>("CouponCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CouponName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DiscountAmount")
                        .HasColumnType("int");

                    b.Property<int>("MinAmount")
                        .HasColumnType("int");

                    b.HasKey("CouponId");

                    b.ToTable("Coupons");

                    b.HasData(
                        new
                        {
                            CouponId = 1,
                            CouponCode = "BMW10SD",
                            CouponName = "BMW 10% Special Discount",
                            DiscountAmount = 10000,
                            MinAmount = 30000
                        },
                        new
                        {
                            CouponId = 2,
                            CouponCode = "BMW5SD",
                            CouponName = "BMW 5% Special Discount",
                            DiscountAmount = 5000,
                            MinAmount = 35000
                        },
                        new
                        {
                            CouponId = 3,
                            CouponCode = "MBC20235",
                            CouponName = "Mercedes Benz C class 5",
                            DiscountAmount = 6000,
                            MinAmount = 42000
                        },
                        new
                        {
                            CouponId = 4,
                            CouponCode = "MBC20237",
                            CouponName = "Mercedes Benz C class 7",
                            DiscountAmount = 7100,
                            MinAmount = 46000
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
