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
        public string Protocolo { get; set; }
        public string Tipo { get; set; }
        public string Endereco { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }
        public DateTime Data { get; set; }


        internal bool EhValido()
        {
            var teste = string.IsNullOrEmpty(Descricao);

            if (Tipo == "água" || Tipo == "esgoto" || Tipo == "luz" 
                || Tipo == "convivência" || Tipo == "outro")
            {
                 if ( 
                    string.IsNullOrEmpty(Endereco) == false
                    && string.IsNullOrEmpty(Descricao) == false
                    && string.IsNullOrEmpty(Status) == false
                    && string.IsNullOrEmpty(Protocolo) == false
                    && string.IsNullOrEmpty(Tipo) == false
                    )
                {

                    return true;

                }
            }

            return false;
        }

    }

}
