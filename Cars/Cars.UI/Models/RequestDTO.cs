using static Cars.Services.CouponAPI.Utility.ApiConstants;
using System.Security.AccessControl;

namespace Cars.UI.Models
{
    public class RequestDTO
    {
        public ApiTypeEnum ApiType { get; set; } = ApiTypeEnum.GET;

        public string Url { get; set; }

        public CouponDTO Data { get; set; }

        public string AccessToken { get; set; }
    }
}
