using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenCall.ReponseRequest
{
    public class RequestChamado
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public string Tipo { get; set; }
        public string Endereco { get; set; }
        public string Descricao { get; set; }
    }
}
