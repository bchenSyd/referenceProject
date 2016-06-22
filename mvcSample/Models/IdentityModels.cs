using System.Data.Entity;
using System.Dynamic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace mvcSample.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Email { get; set; } // email field
        public Tenant Tenant { get; set; } 
    }

    public class Tenant
    {
        //scalar property for change tracking
        public  virtual int Id { get; set; }
        public  virtual  string TenantName { get; set; }
        public  virtual  string TenantDescription { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //here is the place where database schema get updated;
            //code first mode

            base.OnModelCreating(modelBuilder);
        }
    }
}