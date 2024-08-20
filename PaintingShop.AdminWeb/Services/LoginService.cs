using System.Security.Claims;
using System.Security.Cryptography;
using Frontend.Blazor.Code;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using PaintingShop.AdminWeb.Models;

namespace PaintingShop.AdminWeb.Services;

public class LoginService
{
    private const string AccessToken = nameof(AccessToken);
    private const string RefreshToken = nameof(RefreshToken);

    private static ProtectedLocalStorage _localStorage;
    private readonly NavigationManager _navigation;
    private readonly IConfiguration _configuration;

    public LoginService(ProtectedLocalStorage localStorage, NavigationManager navigation, IConfiguration configuration)
    {
        _localStorage = localStorage;
        _navigation = navigation;
        _configuration = configuration;
    }
    public static async Task<bool> LoginAsync(LoginModel model)
    {

        await _localStorage.SetAsync(AccessToken, "");
        await _localStorage.SetAsync(RefreshToken, "");

        return true;
    }
    public async Task<List<Claim>> GetLoginInfoAsync()
    {
        var emptyResult = new List<Claim>();
        ProtectedBrowserStorageResult<string> accessToken;
        ProtectedBrowserStorageResult<string> refreshToken;
        try
        {
            accessToken = await _localStorage.GetAsync<string>(AccessToken);
            refreshToken = await _localStorage.GetAsync<string>(RefreshToken);
        }
        catch (CryptographicException)
        {
            await LogoutAsync();
            return emptyResult;
        }

        if (accessToken.Success is false || accessToken.Value == default)
            return emptyResult;

        var claims = JwtTokenHelper.ValidateDecodeToken(accessToken.Value, _configuration);

        if (claims.Count != 0)
            return claims;

        if (refreshToken.Value != default)
            if (string.IsNullOrWhiteSpace("") is false)
            {
                await _localStorage.SetAsync(AccessToken, "");
                await _localStorage.SetAsync(RefreshToken, "");
                claims = JwtTokenHelper.ValidateDecodeToken("", _configuration);
                return claims;
            }
            else
                await LogoutAsync();
        else
            await LogoutAsync();
        return claims;
    }

    public async Task LogoutAsync()
    {
        await RemoveAuthDataFromStorageAsync();
        _navigation.NavigateTo("/", true);
    }

    private async Task RemoveAuthDataFromStorageAsync()
    {
        await _localStorage.DeleteAsync(AccessToken);
        await _localStorage.DeleteAsync(RefreshToken);
    }
}