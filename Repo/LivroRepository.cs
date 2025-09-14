// Repositories/LivroRepository.cs
using Microsoft.EntityFrameworkCore;
using Biblioteca.Interfaces;
using Biblioteca.Models;
using Biblioteca.Db;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Repositories
{
    public class LivroRepository : Repository<Livros>, iLivroRepository
    {
        public LivroRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Livros>> GetByAutorAsync(string autor)
        {
            return await _dbSet
                .Where(l => l.Autor.Contains(autor))
                .ToListAsync();
        }

        public async Task<IEnumerable<Livros>> GetDisponiveisAsync()
        {
            return await _dbSet
                .Where(l => l.Disponivel && l.Quantidade > 0)
                .ToListAsync();
        }

        public async Task<IEnumerable<Livros>> GetByCategoriaAsync(string categoria)
        {
            return await _dbSet
                .Where(l => l.Categoria == categoria)
                .ToListAsync();
        }

        public async Task<bool> IsbnExistsAsync(string isbn)
        {
            return await _dbSet
                .AnyAsync(l => l.ISBN == isbn);
        }

        public async Task<IEnumerable<Livros>> SearchAsync(string searchTerm)
        {
            return await _dbSet
                .Where(l => l.Titulo.Contains(searchTerm) || 
                           l.Autor.Contains(searchTerm) ||
                           l.ISBN.Contains(searchTerm) ||
                           l.Categoria.Contains(searchTerm))
                .ToListAsync();
        }

        // Override para incluir relacionamentos se necessário
        public override async Task<Livros> GetByIdAsync(int id)
        {
            return await _dbSet
                .FirstOrDefaultAsync(l => l.ID == id);
        }
    }

    internal interface iLivroRepository
    {
    }
}