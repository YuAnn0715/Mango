namespace Mango.Web.Service.IService
{
    // 存取tokken到cookies
    public interface ITokenProvider
    {
        //調用並set token
        void SetToken(string token);
        //檢索token
        string? GetToken();
        //清除token
        void ClearToken();
    }
}
