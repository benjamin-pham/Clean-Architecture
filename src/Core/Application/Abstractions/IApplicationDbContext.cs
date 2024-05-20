using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Abstractions;
public interface IApplicationDbContext
{
    DatabaseFacade Database { get; }
}