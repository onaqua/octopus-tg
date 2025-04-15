namespace Kernel.Auth;

public interface IUserCookiesReader
{
    public string GetAuthorizationCookie();
}
