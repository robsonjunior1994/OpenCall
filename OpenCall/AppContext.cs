using Microsoft.EntityFrameworkCore;
using OpenCall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenCall
{
    public class AppContext : DbContext
    {
        public DbSet<Chamado> Chamados { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AppChamadoDB;Trusted_Connection=true;");
        }
    }
}
