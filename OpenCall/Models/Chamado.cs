using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OpenCall.Models
{
    public class Chamado
    {
        public int Id { get; set; }
        [Required]
        public string Protocolo { get; set; }
        [Required]
        public string Tipo { get; set; }
        [Required]
        public string Endereco { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public string Status { get; set; }
        public DateTime Data { get; set; }


        internal bool EhValido()
        {
            if (Tipo != "água" || Tipo != "esgoto" || Tipo != "luz" 
                || Tipo != "convivência" || Tipo != "outros" 
                && string.IsNullOrEmpty(Endereco) && string.IsNullOrEmpty(Descricao))
            {
                return false;
            }
            return true;
        }

    }

}
