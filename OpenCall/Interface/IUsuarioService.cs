using OpenCall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenCall.Interface
{
    public interface IUsuarioService
    {
        Usuario Login(Usuario usuario);
        bool ValidaKey(string key);
        Usuario GetForKey(string key);

    }
}
