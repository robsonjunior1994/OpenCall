using OpenCall.Interface;
using OpenCall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OpenCall.Services
{
    public class AutenticacaoService : IAutenticacaoService
    {
        readonly HttpClient _client;
        public AutenticacaoService(HttpClient client)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:44357");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<HttpResponseMessage> BuscarUsuario(string userKey)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("/api/usuario/validarchave", userKey);

            if(response.StatusCode.ToString() == "OK")
            {
                return response;
            }

            return null;
        }

        public async Task<bool> ValidarKey(string userKey)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("/api/usuario/validarchave", userKey);

            if (response.StatusCode.ToString() == "OK")
            {
                return true;
            }

            return false;
        }
    }
}
