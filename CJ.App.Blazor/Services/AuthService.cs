using Blazored.LocalStorage;
using CJ.Exp.ApiModels;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CJ.App.Blazor.Services
{
  public class AuthService : IAuthService
  {
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly ILocalStorageService _localStorage;

    public AuthService(HttpClient httpClient,
      AuthenticationStateProvider authenticationStateProvider,
      ILocalStorageService localStorage)
    {
      _httpClient = httpClient;
      _authenticationStateProvider = authenticationStateProvider;
      _localStorage = localStorage;
    }

    public async Task<ApiResponseAM<LoginResponseAM>> Login(LoginAM loginModel)
    {
      Console.WriteLine($"Login as {loginModel.Email}");
      var loginAsJson = JsonSerializer.Serialize(loginModel);
      var response = await _httpClient.PostAsync("http://localhost:1112/api/Users/Login", new StringContent(loginAsJson, Encoding.UTF8, "application/json"));

      var loginResult = JsonSerializer.Deserialize<ApiResponseAM<LoginResponseAM>>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
      if (loginResult.Success)
      {
        await _localStorage.SetItemAsync("authToken", loginResult.Data.Token);
        await _localStorage.SetItemAsync("refreshToken", loginResult.Data.RefreshToken);
        ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Email);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Data.Token);
      }



      return loginResult;
    }


    public async Task Logout()
    {
      await _localStorage.RemoveItemAsync("authToken");
      ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
      _httpClient.DefaultRequestHeaders.Authorization = null;
    }
  }
}
