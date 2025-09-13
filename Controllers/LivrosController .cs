using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Biblioteca.Db;  // ← AJUSTADO: usando Biblioteca.Db em vez de BibliotecaAPI.Data
using Biblioteca.Models; // ← AJUSTADO: usando Biblioteca.Models
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteca.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivrosController : ControllerBase
    {
        //  Contexto ajustado para usar ApplicationDbContext do namespace correto
        private readonly ApplicationDbContext _context;

        //  Injeção de dependência - o ASP.NET fornece automaticamente o contexto
        public LivrosController(ApplicationDbContext context)
        {
            _context = context;
        }

        //  GET: api/livros - Lista TODOS os livros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livros>>> GetLivros()
        {
            //  Busca assíncrona de todos os livros no banco
            return await _context.Livros.ToListAsync();
        }

        //  GET: api/livros/5 - Busca livro por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Livros>> GetLivro(int id)
        {
            // 🔎 FindAsync busca pela chave primária (mais eficiente)
            var livro = await _context.Livros.FindAsync(id);

            if (livro == null)
            {
                return NotFound(); //  Retorna 404 se não encontrar
            }

            return livro; // Retorna 200 + dados do livro
        }

        // 🌟 POST: api/livros - Cria NOVO livro
        [HttpPost]
        public async Task<ActionResult<Livros>> PostLivro(Livros livro)
        {
            // ➕ Adiciona o novo livro ao contexto (ainda não salvou no banco)
            _context.Livros.Add(livro);
            
            // 💾 Salva as mudanças no banco de dados (INSERT)
            await _context.SaveChangesAsync();

            // ✅ Retorna 201 Created + URL do novo recurso + dados
            return CreatedAtAction("GetLivro", new { id = livro.ID }, livro);
        }

        // 🌟 PUT: api/livros/5 - Atualiza livro existente
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLivro(int id, Livros livro)
        {
            // ⚠️ Valida se o ID da URL coincide com o ID do objeto
            if (id != livro.ID)
            {
                return BadRequest(); //  Retorna 400 Bad Request
            }

            // 🎯 Marca o objeto como modificado para o Entity Framework
            _context.Entry(livro).State = EntityState.Modified;

            try
            {
                //  Tenta salvar as mudanças no banco (UPDATE)
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //  Se o livro não existir mais no banco
                if (!LivroExists(id))
                {
                    return NotFound(); // Retorna 404
                }
                else
                {
                    throw; //  Propaga outros erros
                }
            }

            return NoContent(); //  Retorna 204 No Content (sucesso)
        }

        //  DELETE: api/livros/5 - Remove livro
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLivro(int id)
        {
            //  Primeiro busca o livro pelo ID
            var livro = await _context.Livros.FindAsync(id);
            
            if (livro == null)
            {
                return NotFound(); //  Retorna 404 se não existir
            }

            //  Marca o livro para exclusão
            _context.Livros.Remove(livro);
            
            //  Executa o DELETE no banco
            await _context.SaveChangesAsync();

            return NoContent(); //  Retorna 204 No Content
        }

        //  Método auxiliar PRIVADO para verificar se livro existe
        private bool LivroExists(int id)
        {
            // ✅ Verifica se existe algum livro com o ID especificado
            return _context.Livros.Any(e => e.ID == id);
        }
    }
}