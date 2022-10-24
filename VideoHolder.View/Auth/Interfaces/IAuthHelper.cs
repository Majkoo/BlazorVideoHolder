using VideoHolder.Data.Entities;
using VideoHolder.Shared.Dtos;

namespace VideoHolder.View.Auth.Interfaces;

public interface IAuthHelper
{
    Task<Account> GetAccountByLogin(string login);
    Task<bool> Login(LoginDto loginDto);
    Task<bool> Register(RegisterDto registerDto);
}