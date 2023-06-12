using ControleDeContatos.Filters;
using ControleDeContatos.Helper;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeContatos.Controllers
{
    [PaginaParaUsuarioLogado]
    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;
        private readonly ISessao _sessao;

        public ContatoController(IContatoRepositorio contatoRepositorio, ISessao sessao)
        {
            _contatoRepositorio = contatoRepositorio;
            _sessao = sessao;
        }
        public IActionResult Index()
        { 
            UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
            List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos(usuarioLogado.Id);
            return View(contatos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }

        public IActionResult Deletar(int id)
        {
            try
            {
                if (_contatoRepositorio.Deletar(id))
                {
                    TempData["Success"] = "Contato deletado com sucesso!";
                }
                else
                {
                    TempData["Fail"] = "Algo de errado não está certo";
                }
                
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["Fail"] = ("Ocorreu um erro: {0}", erro.Message);
                return RedirectToAction("Index");
            }
        }

        public IActionResult Apagar(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                    contato.UsuarioId = usuarioLogado.Id;
                    _contatoRepositorio.Adicionar(contato);
                    TempData["Success"] = "Contato Cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(contato);
            }
            catch(System.Exception erro)
            {
                TempData["Fail"] = ("Ocorreu um erro: {0}", erro.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Alterar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                    contato.UsuarioId = usuarioLogado.Id;
                    _contatoRepositorio.Atualizar(contato);
                    TempData["Success"] = "Contato alterado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View("Editar", contato);
            }catch(System.Exception erro)
            {
                TempData["Fail"] = ("Ocorreu um erro: {0}", erro.Message);
                return RedirectToAction("Index");
            }
        }
    }
}
