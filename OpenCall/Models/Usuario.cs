using OpenCall.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenCall.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Key { get; set; }
        public DateTime DataKey { get; set; }

        internal bool EhValido()
        {
            if(
                Id > 0
                && string.IsNullOrEmpty(Nome) == false
                && string.IsNullOrEmpty(Sobrenome) == false
                && string.IsNullOrEmpty(Email) == false
                && string.IsNullOrEmpty(Senha) == false
              )
            {
                if(Nome != Sobrenome && Email.Length > 10)
                {
                    return true;
                }

                else
                {
                    return false;
                }

            }

            else
            {
                return false;
            }
        }

    }
}
