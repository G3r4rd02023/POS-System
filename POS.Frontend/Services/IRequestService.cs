
namespace POS.Frontend.Services
{
    public interface IRequestService
    {
        Task<object> DeleteAsync(string url);
        Task<T> GetAsync<T>(string url);
        Task<T> GetByIdAsync<T>(string url, int id);
        Task<HttpResponseMessage> PostAsync<T>(string url, T model);
        Task<HttpResponseMessage> PutAsync<T>(string url, T model);
    }
}