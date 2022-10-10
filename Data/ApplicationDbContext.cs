using BulkyBookWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Data
{
    // This is the configuration to make the DB 
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }
        
        // This is the to add Model Category as Table Categories in DB
        public DbSet<Category> Categories { get; set; }
    }
}
