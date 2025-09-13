using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Biblioteca.Db
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Biblioteca.Models.Livros> Livros { get; set; }
        public DbSet<Biblioteca.Models.Usuario> Usuarios { get; set; }
        public DbSet<Biblioteca.Models.Emprestimo> Emprestimos { get; set; }
    }
}