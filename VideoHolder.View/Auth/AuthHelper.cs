using Microsoft.AspNetCore.Identity;
using VideoHolder.Data.Entities;
using VideoHolder.Data.Repos.Interfaces;
using VideoHolder.Shared.Dtos;
using VideoHolder.View.Auth.Interfaces;

namespace VideoHolder.View.Auth;

public class AuthHelper : IAuthHelper
{
    private readonly IAccountRepo _accountRepo;
    private readonly IPasswordHasher<Account> _passwordHasher;

    public AuthHelper(
        IAccountRepo accountRepo,
        IPasswordHasher<Account> passwordHasher
        )
    {
        _accountRepo = accountRepo;
        _passwordHasher = passwordHasher;
    }

    public async Task<Account> GetAccountByLogin(string login)
    {
        return await _accountRepo.GetAccountByLoginAsync(login);
    }

    public async Task<bool> Login(LoginDto loginDto)
    {
        var account = await _accountRepo.GetAccountByLoginAsync(loginDto.Login);
        if (account is null) return false;
        var passResult = _passwordHasher.VerifyHashedPassword(account, account.PasswordHash, loginDto.Password);
        return passResult == PasswordVerificationResult.Success;
    }

    public async Task<bool> Register(RegisterDto registerDto)
    {
        if (registerDto.Password != registerDto.RepeatPassword) return false;

        var account = new Account()
        {
            Id = Guid.NewGuid(),
            Login = registerDto.Login
        };
        account.PasswordHash = _passwordHasher.HashPassword(account, registerDto.Password);

        return (await _accountRepo.Create(account)) is not null;
    }
}