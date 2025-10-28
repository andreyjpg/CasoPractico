using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MinimalAPI.ServiceLocator.Services.Contracts;

namespace MinimalAPI.ServiceLocator.ServiceFactory
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly IServiceProvider _provider;
        public ServiceFactory(IServiceProvider provider) 
        {
            _provider = provider;
        }

        public object Create(string key)
        {
            try
            {
                var serviceKey = char.ToUpper(key[0]) + key.Substring(1);
                var dtoType = Type.GetType($"MinimalAPI.Models.DTOs.{serviceKey}DTO, MinimalAPI.Models");

                if (dtoType == null)
                    throw new ArgumentException($"DTO not found for key '{key}'");

                var serviceType = typeof(IService<>).MakeGenericType(dtoType);


                return _provider.GetRequiredService(serviceType);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ServiceFactory] Error creating '{key}': {ex.Message}");
                throw;
            }
           

            

        }
    }
}
