﻿using OpenCall.Interface;
using OpenCall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenCall.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IChamadoRepository _chamadoRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository, IChamadoRepository chamadoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _chamadoRepository = chamadoRepository;

        }

        public Usuario Login(Usuario usuario)
        {
            if(!string.IsNullOrEmpty(usuario.Email) && !string.IsNullOrEmpty(usuario.Senha))
            {
                var user = _usuarioRepository.Login(usuario);
                Random randNum = new Random(2);
                user.DataKey = DateTime.Now.AddDays(1);
                user.Key = MD5Hash.Hash.Content(usuario.Email+DateTime.Now.ToString("HH" + "mm" + "ss" + "ff" + "d" + "M" + "yyyy"));
                _usuarioRepository.Atualizar(user);
                return user;
            } else
            {
                return null;
            }

        }

        public bool ValidaKey(string key)
        {
            Usuario user = _usuarioRepository.GetUsuarioForKey(key);
            if(user == null || user.DataKey < DateTime.Now)
            {
                return false;
            } else
            {
                return true;
            }

        }

        public Usuario GetForKey(string key)
        {
            Usuario user = _usuarioRepository.GetUsuarioForKey(key);
            return user;
        }
    }
}
