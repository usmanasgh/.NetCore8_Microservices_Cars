namespace Cars.Services.CouponAPI.Utility
{
    public class ApiConstants
    {
        public static string CouponAPIBase { get; set; }
        public enum ApiTypeEnum
        {
            GET,
            POST, 
            PUT, 
            DELETE
        }
    }
}
