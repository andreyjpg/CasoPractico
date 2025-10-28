namespace MinimalAPI.ServiceLocator.ServiceFactory
{
    public interface IServiceFactory
    {
        object Create(string key);
    }
}
