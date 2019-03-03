using System.Collections.Generic;

namespace CJ.Exp.ServiceModels.Users
{
  public class AuthResultSM
  {
    public bool Succeeded { get; set; }
    public bool IsLockedOut { get; set; }
    public bool SilentFail { get; set; }
    public bool UserNotFound { get; set; }
    public List<ProcessingErrorSM> Errors { get; set; }

    public void SetGenericFail()
    {
      Succeeded = false;
    }
    public void SetUserNotFound()
    {
      SetGenericFail();
      UserNotFound = true;
    }
  }
}