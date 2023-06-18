using System.Security.AccessControl;
using static Cars.UI.Utility.ApiConstants;

namespace Cars.UI.Models
{
    public class RequestDTO
    {
        public ApiTypeEnum ApiType { get; set; } = ApiTypeEnum.GET;

        public string Url { get; set; }

        public object Data { get; set; }

        public string AccessToken { get; set; }
    }
}
