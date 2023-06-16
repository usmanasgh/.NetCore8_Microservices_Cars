using Cars.UI.Models;

namespace Cars.UI.Service.IService
{
    public interface IBaseService
    {
        // MUA : Async method
        Task<ResponseDTO?> SendAsync(RequestDTO requestDTO);

    }
}
