using OpenCall.ReponseRequest;
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
        public int IdUser { get; set; }
        public bool Ativo { get; set; }

        public Chamado() { }
        public Chamado(RequestChamado rc)
        {
            Data = DateTime.Now;
            Protocolo = DateTime.Now.ToString("HH"+"mm"+"ss"+"ff"+"d"+"M"+"yyyy");
            Status = "aberto";
            Ativo = true;

            Tipo = rc.Tipo;
            Endereco = rc.Endereco;
            Descricao = rc.Descricao;
        }

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
                    //if (User.EhValido())
                    //{
                        return true;
                    //}
                }
            }

            return false;
        }

    }

}
