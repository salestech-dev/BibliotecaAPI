using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Models
{
    public class Emprestimo
    {
        public int ID { get; set; }
        public int UsuarioID { get; set; }
        public int LivroID { get; set; }
        public int DiaEmprestimo { get; set; }
        public int DiaDevolucao { get; set; }
        public string Status { get; set; }
        
    }
}