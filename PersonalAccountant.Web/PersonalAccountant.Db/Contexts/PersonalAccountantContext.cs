using Microsoft.EntityFrameworkCore;
using PersonalAccountant.Db.Models;

namespace PersonalAccountant.Db.Contexts
{
    public class PersonalAccountantContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public PersonalAccountantContext(DbContextOptions<PersonalAccountantContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
