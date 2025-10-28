using Ticket = MinimalAPI.Data.Models.Ticket;


namespace MinimalAPI.Data.Repositories;
public interface IRepositoryTicket
{
    Task<bool> UpsertAsync(Ticket entity, bool isUpdating);
    Task<bool> CreateAsync(Ticket entity);
    Task<bool> DeleteAsync(Ticket entity);
    Task<IEnumerable<Ticket>> ReadAsync();
    Task<Ticket> FindAsync(int id);
    Task<bool> UpdateAsync(Ticket entity);
    Task<bool> UpdateManyAsync(IEnumerable<Ticket> entities);
    Task<bool> ExistsAsync(Ticket entity);
}

public class RepositoryTicket : RepositoryBase<Ticket>, IRepositoryTicket
{
}