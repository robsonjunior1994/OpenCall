﻿using OpenCall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenCall.Interface
{
    public interface IUsuarioRepository
    {
        void Adicionar(Usuario usuario);
        Usuario Get(int id);
        Usuario Login(Usuario usuario);
        void Atualizar(Usuario user);
        Usuario GetUsuarioForKey(string key);
    }
}
