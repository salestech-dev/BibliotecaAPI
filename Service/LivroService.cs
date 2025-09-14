// Services/LivroService.cs
using Biblioteca.Services.Interface;
using Biblioteca.Interfaces;
using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteca.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepository;

        public LivroService(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public async Task<IEnumerable<Livros>> GetAllLivrosAsync()
        {
            return await _livroRepository.GetAllAsync();
        }

        public async Task<Livros> GetLivroByIdAsync(int id)
        {
            var livro = await _livroRepository.GetByIdAsync(id);
            if (livro == null)
            {
                throw new KeyNotFoundException("Livro não encontrado");
            }
            return livro;
        }

        public async Task<Livros> CreateLivroAsync(Livros livro)
        {
            // Validar se ISBN já existe
            if (await _livroRepository.IsbnExistsAsync(livro.ISBN))
            {
                throw new InvalidOperationException("ISBN já cadastrado");
            }

            // Garantir que disponibilidade corresponde com quantidade
            livro.Disponivel = livro.Quantidade > 0;

            return await _livroRepository.AddAsync(livro);
        }

        public async Task UpdateLivroAsync(Livros livro)
        {
            if (!await _livroRepository.ExistsAsync(livro.ID))
            {
                throw new KeyNotFoundException("Livro não encontrado");
            }

            // Atualizar disponibilidade baseada na quantidade
            livro.Disponivel = livro.Quantidade > 0;

            await _livroRepository.UpdateAsync(livro);
        }

        public async Task DeleteLivroAsync(int id)
        {
            if (!await _livroRepository.ExistsAsync(id))
            {
                throw new KeyNotFoundException("Livro não encontrado");
            }

            await _livroRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Livros>> GetLivrosByAutorAsync(string autor)
        {
            return await _livroRepository.GetByAutorAsync(autor);
        }

        public async Task<IEnumerable<Livros>> GetLivrosDisponiveisAsync()
        {
            return await _livroRepository.GetDisponiveisAsync();
        }

        public async Task<IEnumerable<Livros>> GetLivrosByCategoriaAsync(string categoria)
        {
            return await _livroRepository.GetByCategoriaAsync(categoria);
        }

        public async Task<int> GetQuantidadeDisponivelAsync(int id)
        {
            var livro = await GetLivroByIdAsync(id);
            return livro.Quantidade;
        }

        // Novo método para atualizar quantidade após empréstimo/devolução
        public async Task AtualizarQuantidadeAsync(int id, int novaQuantidade)
        {
            var livro = await GetLivroByIdAsync(id);
            livro.Quantidade = novaQuantidade;
            livro.Disponivel = novaQuantidade > 0;
            await UpdateLivroAsync(livro);
        }
    }
}