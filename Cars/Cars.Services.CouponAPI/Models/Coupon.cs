using System.ComponentModel.DataAnnotations;

namespace Cars.Services.CouponAPI.Models
{
    public class Coupon
    {
        // Returning the DTO - Data Transfer Object only - Don't place extra properties to make service light.
        
        [Key]
        public int CouponId { get; set; }
        [Required]
        public string CouponCode { get; set; }
        public string CouponName { get; set; }
        [Required]
        public int DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
