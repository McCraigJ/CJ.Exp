namespace CJ.Exp.DomainInterfaces
{
  public interface ILanguage
  {
    void Initialise(bool checkLastModified = false);

    string GetText(string path);
  }
}
