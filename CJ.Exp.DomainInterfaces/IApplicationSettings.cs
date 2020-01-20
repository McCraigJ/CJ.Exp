namespace CJ.Exp.DomainInterfaces
{
  public interface IApplicationSettings
  {
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }
  }
}
