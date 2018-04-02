using System;
using System.Threading.Tasks;

namespace DataLayer.DbContext.Abstractions
{
    public interface IDbInitializer : IDisposable
    {
        Task InitializeAsync();
    }
}
