namespace Cars.UI.Models
{
    public class CouponDTO
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public string CouponName { get; set; }
        public int DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
