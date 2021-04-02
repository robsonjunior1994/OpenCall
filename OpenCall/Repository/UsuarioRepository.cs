using OpenCall.Interface;
using OpenCall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenCall.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private AppContext _appContext;

        public UsuarioRepository(AppContext appContext)
        {
            _appContext = appContext;
        }

        public void Adicionar(Usuario usuario)
        {
            _appContext.Usuario.Add(usuario);
            _appContext.SaveChanges();
        }

        public void Atualizar(Usuario user)
        {
            _appContext.Usuario.Update(user);
            _appContext.SaveChanges();
        }

        public Usuario Get(int id)
        {
            return _appContext.Usuario.FirstOrDefault(usuario => usuario.Id == id);
        }

        public Usuario Login(Usuario UsuarioRecebido)
        {
           return _appContext.Usuario.FirstOrDefault
                (userBanco => userBanco.Email == UsuarioRecebido.Email && userBanco.Senha == UsuarioRecebido.Senha);
        }

        public Usuario GetUsuarioForKey(string key)
        {
            Usuario user = _appContext.Usuario.FirstOrDefault(usuario => usuario.Key == key);
            if( user != null)
            {
                return user;
            } else
            {
                return null;
            }

        }
    }
}
