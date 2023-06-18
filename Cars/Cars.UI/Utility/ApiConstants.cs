namespace Cars.UI.Utility
{
    public class ApiConstants
    {
        public static string CouponAPIBase { get; set; }
        public static string AuthAPIBase { get; set; }
        public enum ApiTypeEnum
        {
            GET,
            POST, 
            PUT, 
            DELETE
        }
    }
}
