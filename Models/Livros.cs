using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Models
{
    public class Livros
    {
        public int ID { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string ISBN { get; set; }
        public string Editora { get; set; }
        public string Descricao { get; set; }
        public int AnoPublicacao { get; set; }
        public int Quantidade { get; set; }
        public string Categoria { get; set; }
        
    }
}