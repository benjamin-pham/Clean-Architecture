using Application.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder builder) =>
        builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

    public DbSet<User> User { get; set; }
}
