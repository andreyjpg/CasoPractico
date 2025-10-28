using MinimalAPI.Architecture.Providers;
using MinimalAPI.ServiceLocator.Services.Contracts;
using MinimalAPI.Architecture;
using MinimalAPI.DTO;

namespace MinimalAPI.ServiceLocator.Services;

public interface ITicketsService
{
    Task<IEnumerable<TicketDTO>> GetDataAsync();
    Task<bool> CreateDataAsync(string content);
    Task<bool> UpdateDataAsync(string id, string content);
    Task<bool> DeleteDataAsync(string id);
}

public class TicketsService(IRestProvider restProvider, IConfiguration configuration)
    : IService<TicketDTO>, ITicketsService
{
    public async Task<IEnumerable<TicketDTO>> GetDataAsync()
    {
        var url = configuration.GetStringFromAppSettings("APIS", "Tickets");
        var response = await restProvider.GetAsync(url, null);
        return await JsonProvider.DeserializeAsync<IEnumerable<TicketDTO>>(response);
    }

    public async Task<bool> CreateDataAsync(string content)
    {
        var url = configuration.GetStringFromAppSettings("APIS", "Tickets");
        var response = await restProvider.PostAsync(url, content);
        return bool.Parse(response);
    }

    public async Task<bool> UpdateDataAsync(string id, string content)
    {
        var url = configuration.GetStringFromAppSettings("APIS", "Tickets");
        var response = await restProvider.PutAsync(url, id, content);
        return bool.Parse(response);
    }

    public async Task<bool> DeleteDataAsync(string id)
    {
        var url = configuration.GetStringFromAppSettings("APIS", "Tickets");
        var response = await restProvider.DeleteAsync(url, id);
        return bool.Parse(response);
    }
}