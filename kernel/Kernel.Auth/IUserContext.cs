namespace Kernel.Auth;

public interface IUserContext : IUserCookiesReader, IUserCookiesWriter
{
    public Guid Id { get; }

    public string Name { get; }

    public string Email { get; }

    public IReadOnlyCollection<string> Roles { get; }
}
