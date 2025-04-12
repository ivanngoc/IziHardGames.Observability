using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebApi0
{
    public class SimpleDbContext : DbContext
    {
        public DbSet<EntityGen1> EntityGen1s { get; set; }
        public SimpleDbContext(DbContextOptions<SimpleDbContext> options) : base(options)
        {

        }
    }

    public class EntityGen1
    {
        [Key]
        public Guid guid { get; set; }
    }
}
