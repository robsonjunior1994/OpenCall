using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OpenCall.Interface
{
    public interface IAutenticacaoService
    {
        Task<HttpResponseMessage> BuscarUsuario(string userKey);
        Task<bool> ValidarKey(string userKey);
    }
}
