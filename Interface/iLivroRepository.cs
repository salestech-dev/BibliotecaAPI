// Interfaces/ILivroRepository.cs
using Biblioteca.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteca.Interfaces
{
    public interface ILivroRepository : IRepository<Livros>
    {
        Task<IEnumerable<Livros>> GetByAutorAsync(string autor);
        Task<IEnumerable<Livros>> GetDisponiveisAsync();
        Task<IEnumerable<Livros>> GetByCategoriaAsync(string categoria);
        Task<bool> IsbnExistsAsync(string isbn);
        Task<IEnumerable<Livros>> SearchAsync(string searchTerm);
    }
}