using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Biblioteca.Db;

namespace Biblioteca
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            
            // Use sua connection string diretamente aqui
            var connectionString = "Server=localhost;Database=biblioteca;User=root;Password=Filhos02@;";
            
            optionsBuilder.UseMySql(
                connectionString,
                new MySqlServerVersion(new Version(8, 0, 21))
            );

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}