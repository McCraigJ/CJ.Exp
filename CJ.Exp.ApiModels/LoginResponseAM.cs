namespace CJ.Exp.ApiModels
{
  public class LoginResponseAM : ApiResponseModelBase
  {
    public string Id { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public string FirstName { get; set; }
  }
}
