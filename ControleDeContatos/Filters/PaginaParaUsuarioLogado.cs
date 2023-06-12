using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using ControleDeContatos.Models;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;

namespace ControleDeContatos.Filters
{
    public class PaginaParaUsuarioLogado : ActionFilterAttribute
    {
        //public override void OnActionExecuting(ActionExecutingContext context)
        //{
        //    string token = context.HttpContext.Session.GetString("sessao");
        //    string sessaoUsuario = context.HttpContext.Session.GetString("sessaoUsuarioLogado");

        //    public override TokenValidationResult ValidateTokenAsync(string token, TokenValidationParameters validationParameters)
        //    {
        //    if (string.IsNullOrEmpty(token))
        //    {
        //        context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
        //    }
        //    else
        //    {

        //    }*/

        //    if (string.IsNullOrEmpty(sessaoUsuario))
        //    {
        //        context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
        //    }
        //    else
        //    {   UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
        //        if(usuario == null)
        //        {
        //            context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Login" }, { "action", "Index" } });
        //        }
        //    }
        //    base.OnActionExecuting(context);
        //} 
    }
}
