using Biblioteca.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteca.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario> GetByEmailAsync(string email);
        Task<Usuario> GetByCpfAsync(string cpf);
        Task<bool> EmailExistsAsync(string email);
        Task<bool> CpfExistsAsync(string cpf);
        Task<IEnumerable<Usuario>> GetByNomeAsync(string nome);
    }
}