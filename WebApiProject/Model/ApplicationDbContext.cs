using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApiProject.Model
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext():base("ProductDb")
        {

        }
        public DbSet<Product> Product { get; set; }
    }
}