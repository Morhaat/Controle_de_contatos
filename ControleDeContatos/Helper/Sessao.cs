using ControleDeContatos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeContatos.Helper
{
    public class Sessao : ISessao
    {
        private readonly IHttpContextAccessor _httoContext;
        private readonly IConfiguration _configuration;

        public Sessao(IHttpContextAccessor httpContext, IConfiguration configuration)
        {
            _httoContext = httpContext;
            _configuration = configuration;
        }
        public UsuarioModel BuscarSessaoDoUsuario()
        {
            string sessaoUsuario = _httoContext.HttpContext.Session.GetString("sessaoUsuarioLogado");
            if (string.IsNullOrEmpty(sessaoUsuario)) return null;

            return JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
        }

        public string BuscarToken()
        {
            string sessaoUsuario = _httoContext.HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(sessaoUsuario)) return null;
            return sessaoUsuario;
        }

        public void CriarSessaoDoUsuario(UsuarioModel usuario)
        {
            var _secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var _issuer = _configuration["JWT:Issuer"];
            var _audience = _configuration["JWT:Audience"];

            var signInCredentials = new SigningCredentials(_secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOtions = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new[] { 
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim(ClaimTypes.Role, usuario.Perfil.ToString()),
                }),
            Expires = DateTime.UtcNow.AddMinutes(5),
            SigningCredentials = signInCredentials
    };
            var token = new JwtSecurityTokenHandler().CreateToken(tokeOtions);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            string valor = JsonConvert.SerializeObject(usuario);
            _httoContext.HttpContext.Session.SetString("sessaoUsuarioLogado", valor);
            _httoContext.HttpContext.Session.SetString("token", tokenString);
        }

        public void RemoveSessaoDoUsuario()
        {
            _httoContext.HttpContext.Session.Remove("sessaoUsuarioLogado");
            _httoContext.HttpContext.Session.Remove("token");
        }
    }
}
