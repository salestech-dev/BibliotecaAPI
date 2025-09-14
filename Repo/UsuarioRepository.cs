// Repositories/UsuarioRepository.cs
using Microsoft.EntityFrameworkCore;
using Biblioteca.Interfaces;
using Biblioteca.Models;
using Biblioteca.Db;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Usuario> GetByEmailAsync(string email)
        {
            return await _dbSet
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Usuario> GetByCpfAsync(string cpf)
        {
            return await _dbSet
                .FirstOrDefaultAsync(u => u.CPF == cpf);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _dbSet
                .AnyAsync(u => u.Email == email);
        }

        public async Task<bool> CpfExistsAsync(string cpf)
        {
            return await _dbSet
                .AnyAsync(u => u.CPF == cpf);
        }

        public async Task<IEnumerable<Usuario>> GetByNomeAsync(string nome)
        {
            return await _dbSet
                .Where(u => u.Nome.Contains(nome))
                .ToListAsync();
        }
    }
}