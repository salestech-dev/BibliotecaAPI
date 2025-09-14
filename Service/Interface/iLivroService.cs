using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Biblioteca.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteca.Services.Interface
{
    public interface ILivroService
    {
        Task<IEnumerable<Livros>> GetAllLivrosAsync();
        Task<Livros> GetLivroByIdAsync(int id);
        Task<Livros> CreateLivroAsync(Livros livro);
        Task UpdateLivroAsync(Livros livro);
        Task DeleteLivroAsync(int id);
        Task<IEnumerable<Livros>> GetLivrosByAutorAsync(string autor);
        Task<IEnumerable<Livros>> GetLivrosDisponiveisAsync();
        Task<IEnumerable<Livros>> GetLivrosByCategoriaAsync(string categoria);
        Task<int> GetQuantidadeDisponivelAsync(int id);
    }
}