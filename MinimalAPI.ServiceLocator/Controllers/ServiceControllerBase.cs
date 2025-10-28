using Microsoft.AspNetCore.Mvc;
using MinimalAPI.DTO;                       
using MinimalAPI.ServiceLocator.Helper;
using Azure.Core.Serialization;
using System.Text.Json;

namespace MinimalAPI.ServiceLocator.Controllers
{
    public class ServiceControllerBase : ControllerBase
    {

        protected readonly Dictionary<string, Func<Task<IEnumerable<object>>>> ListResolvers;
        protected readonly Dictionary<string, Func<object, Task<object>>> CreateResolvers;
        protected readonly Dictionary<string, Func<string, object, Task<bool>>> UpdateResolvers;
        protected readonly Dictionary<string, Func<string, Task<bool>>> DeleteResolvers;

        protected ServiceControllerBase(IServiceMapper serviceMapper)
        {
            // ReadResolvers 
            ListResolvers = new()
            {
                ["tickets"] = async () =>
                {
                    // defer resolution until invocation time
                    var service = await serviceMapper.GetServiceAsync<TicketDTO>("tickets");
                    var data = await service.GetDataAsync();
                    return data.Cast<object>();
                }
            };

            // CreateResolvers

            CreateResolvers = new()
            {
                ["tickets.cud"] = async (body) =>
                {
                    var service = await serviceMapper.GetServiceAsync<TicketDTO>("tickets.cud");
                    var json = JsonSerializer.Serialize(body);

                    var created = await service.CreateDataAsync(json);
                    return created!;
                }
            };

            // UpdateResolvers

            UpdateResolvers = new()
            {
                ["tickets.cud"] = async (id, body) =>
                {
                    var service = await serviceMapper.GetServiceAsync<TicketDTO>("tickets.cud");
                    var dto = (TicketDTO)body;
                    var json = JsonSerializer.Serialize(body);

                    var ok = await service.UpdateDataAsync(id, json);
                    return ok;
                }
            };

            //DeleteResolvers

            DeleteResolvers = new()
            {
                ["tickets.cud"] = async (id) =>
                {
                    var service = await serviceMapper.GetServiceAsync<TicketDTO>("tickets.cud");
                    var ok = await service.DeleteDataAsync(id);
                    return ok;
                }
            };
        }
    }
}

//TicketDTO should be defined in MinimalAPI.Core.DTOs namespace.
//Awaiting further merge