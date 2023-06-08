using Cars.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Cars.Services.CouponAPI.DAL
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 
        
        
        }

        public DbSet<Coupon> Coupons { get; set; }

        // MUA : Lets seed to our database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 1,
                CouponCode = "BMW10SD",
                CouponName = "BMW 10% Special Discount",
                DiscountAmount = 10000,
                MinAmount = 30000,

            });

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 2,
                CouponCode = "BMW5SD",
                CouponName = "BMW 5% Special Discount",
                DiscountAmount = 5000,
                MinAmount = 35000,

            });

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 3,
                CouponCode = "MBC20235",
                CouponName = "Mercedes Benz C class 5",
                DiscountAmount = 6000,
                MinAmount = 42000,

            });

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 4,
                CouponCode = "MBC20237",
                CouponName = "Mercedes Benz C class 7",
                DiscountAmount = 7100,
                MinAmount = 46000,

            });
        }
    }
}
