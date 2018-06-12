using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Module3.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Module3.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<TaskModel> Tasks { get; set; }
    }
}
