namespace POS.Frontend.Services
{
    public interface ILoginService
    {
        Task Login(string token);

        Task Logout();
    }
}
