namespace Mango.Web.Service.IService
{
    // 存取tokken到cookies
    public interface ITokenProvider
    {

        void SetToken(string token);

        string? GetToken();

        void ClearToken();
    }
}
