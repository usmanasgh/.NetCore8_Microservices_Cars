using Cars.UI.Service.IService;
using Cars.UI.Utility;
using Newtonsoft.Json.Linq;

namespace Cars.UI.Service
{
    public class TokenProvider : ITokenProvider
    {
        
        private readonly IHttpContextAccessor _contextAccessor;

        public TokenProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public void ClearToken()
        {
            _contextAccessor.HttpContext?.Response.Cookies.Delete(ApiConstants.TokenCookie);
        }

        public string? GetToken()
        {
            string? token = null;

            bool? hasToken = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(ApiConstants.TokenCookie, out token);

            return hasToken is true ? token : null;
        }

        public void SetToken(string token)
        {
            _contextAccessor.HttpContext?.Response.Cookies.Append(ApiConstants.TokenCookie, token);
        }
    }
}
