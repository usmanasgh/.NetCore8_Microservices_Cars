namespace Cars.Services.CouponAPI.Models.DTO
{
    public class CouponDTO
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public string CouponName { get; set; }
        public string DiscountAmount { get; set; }
        public string MinAmount { get; set; }

        //public DateTime AuditCreate {get; set; }
        //public DateTime AuditAlter { get; set; }
    }
}
