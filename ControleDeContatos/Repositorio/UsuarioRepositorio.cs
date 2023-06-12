using ControleDeContatos.Data;
using ControleDeContatos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeContatos.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _bancocontext;

        public UsuarioRepositorio(BancoContext bancocontext)
        {
            _bancocontext = bancocontext;
        }

        public List<UsuarioModel> BuscarTodos()
        {
            return _bancocontext.Usuarios.Include(x => x.Contatos).ToList();
        }

        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            var usuarioDb = usuario;
            usuarioDb.DataCadastro = DateAndTime.Now;
            usuario.SetSenhaHash();
            if(usuarioDb.Perfil == 0)
            {
                usuarioDb.Perfil = Enums.PerfilEnum.Padrao;            
            }
            _bancocontext.Usuarios.Add(usuarioDb);
            _bancocontext.SaveChanges();
            return usuarioDb;
        }
        
        public bool Deletar(int id)
        {
            UsuarioModel usuarioDb = ListarPorId(id);
            if (usuarioDb == null) throw new System.Exception("Houve um erro na Remoção do usuário!");
            _bancocontext.Usuarios.Remove(usuarioDb);
            _bancocontext.SaveChanges();

            return true;
        }

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            UsuarioModel usuarioDb = ListarPorId(usuario.Id);
            if(usuarioDb == null) throw new System.Exception("Houve um erro na atualização do usuário!");

            usuarioDb.Nome = usuario.Nome;
            usuarioDb.Login = usuario.Login;
            usuarioDb.Email = usuario.Email;
            usuarioDb.DataAtualizacao = DateAndTime.Now;
            usuarioDb.Perfil = usuario.Perfil;
            _bancocontext.Usuarios.Update(usuarioDb);
            _bancocontext.SaveChanges();

            return usuarioDb;

        }

        public UsuarioModel ListarPorId(int id)
        {
            return _bancocontext.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        public UsuarioModel BuscaPorLogin(string login)
        {
            return _bancocontext.Usuarios.FirstOrDefault(x => x.Login == login);
        }

        public UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel)
        {
            UsuarioModel usuarioDb = ListarPorId(alterarSenhaModel.Id);
            if(usuarioDb == null) throw new Exception("Usuário não encontrado!");

            if(!usuarioDb.SenhaValida(alterarSenhaModel.SenhaAtual)) throw new Exception("Senha atual não confere!");

            if(usuarioDb.SenhaValida(alterarSenhaModel.NovaSenha)) throw new Exception("A nova senha não deve ser a mesma que a atual!");

            usuarioDb.SetNovaSenha(alterarSenhaModel.NovaSenha);
            usuarioDb.DataAtualizacao = DateTime.Now;

            _bancocontext.Usuarios.Update(usuarioDb);
            _bancocontext.SaveChanges();

            return usuarioDb;

        }
    }
}
