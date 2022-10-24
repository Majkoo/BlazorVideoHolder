namespace VideoHolder.View.Auth.Interfaces;


public interface IAuthContextProvider
{
    Task<string> GetUserId();
    Task<string> GetUserName();
}