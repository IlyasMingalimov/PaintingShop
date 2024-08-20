using Frontend.Blazor.Code;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace PaintingShop.AdminWeb.Services;

public class CustomAuthorizationStateProvider : AuthenticationStateProvider
{
	private readonly LoginService _loginService;

	public CustomAuthorizationStateProvider(LoginService loginService)
	{
		_loginService = loginService;
	}

	public override async Task<AuthenticationState> GetAuthenticationStateAsync()
	{
		var claims = await _loginService.GetLoginInfoAsync();
		var claimsIdentity = claims.Count != 0
			? new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme, "name", "role")
			: new ClaimsIdentity();
		var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
		return new AuthenticationState(claimsPrincipal);
	}
}
