﻿namespace Cars.UI.Utility
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

        #region "Roles"

        public const string RoleAdmin = "ADMIN";

        public const string RoleCustomer = "CUSTOMER";

        public const string TokenCookie = "JWTToken";
        
        #endregion
    }
}
