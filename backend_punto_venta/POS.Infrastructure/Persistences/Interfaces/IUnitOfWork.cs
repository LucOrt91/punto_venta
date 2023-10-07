using POS.Infrastructure.FileStorage;

namespace POS.Infrastructure.Persistences.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        IUserRepository User { get; }
        IAzureStorage Storage{ get; }
        IProvideRepository Provider { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
