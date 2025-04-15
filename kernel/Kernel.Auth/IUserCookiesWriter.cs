namespace Kernel.Auth;

public interface IUserCookiesWriter
{
    public string SetAuthorizationCookie(string value);
}
