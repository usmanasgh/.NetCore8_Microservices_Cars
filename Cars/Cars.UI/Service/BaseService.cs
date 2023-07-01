using Cars.UI.Models;
using Cars.UI.Service.IService;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;
using static Cars.UI.Utility.ApiConstants;
using ResponseDTO = Cars.UI.Models.ResponseDTO;

namespace Cars.UI.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProvider _tokenProvider;
        public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
        {
            _httpClientFactory = httpClientFactory;
            _tokenProvider = tokenProvider;
        }
        public async Task<ResponseDTO?> SendAsync(RequestDTO requestDTO, bool withBearer = true)
        {
            try
            {
                HttpClient httpClient = _httpClientFactory.CreateClient("CarsAPI");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
                // token
                if(withBearer)
                {
                    var token = _tokenProvider.GetToken();
                    message.Headers.Add("Authorization", $"Bearer {token}");
                }

                message.RequestUri = new Uri(requestDTO.Url);

                if (requestDTO.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDTO.Data), Encoding.UTF8, "application/json");
                }

                HttpResponseMessage? apiResponse = null;

                switch (requestDTO.ApiType)
                {
                    case ApiTypeEnum.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiTypeEnum.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    case ApiTypeEnum.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                apiResponse = await httpClient.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case System.Net.HttpStatusCode.NotFound:
                        return new() { Success = false, Message = "Not Found" };
                    case System.Net.HttpStatusCode.Forbidden:
                        return new() { Success = false, Message = "Access Denied" };
                    case System.Net.HttpStatusCode.Unauthorized:
                        return new() { Success = false, Message = "Unauthorized" };
                    case System.Net.HttpStatusCode.InternalServerError:
                        return new() { Success = false, Message = "Internal Server Error" };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDTO = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);
                        return apiResponseDTO;
                }
            }
            catch (Exception ex)
            {
                var dto = new ResponseDTO
                {
                    Message = ex.Message.ToString(),
                    Success = false
                };
                return dto;
            }
        }
    }
}