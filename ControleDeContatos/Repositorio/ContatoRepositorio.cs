using ControleDeContatos.Data;
using ControleDeContatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeContatos.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _bancocontext;

        public ContatoRepositorio(BancoContext bancocontext)
        {
            _bancocontext = bancocontext;
        }

        public List<ContatoModel> BuscarTodos(int UsuarioId)
        {
            return _bancocontext.Contatos.Where(x => x.UsuarioId == UsuarioId).ToList();
        }

        public ContatoModel Adicionar(ContatoModel contato)
        {
            _bancocontext.Contatos.Add(contato);
            _bancocontext.SaveChanges();
            return contato;
        }
        
        public bool Deletar(int id)
        {
            ContatoModel contatoDb = ListarPorId(id);
            if (contatoDb == null) throw new System.Exception("Houve um erro na Remoção do contato!");
            _bancocontext.Contatos.Remove(contatoDb);
            _bancocontext.SaveChanges();

            return true;
        }

        public ContatoModel Atualizar(ContatoModel contato)
        {
            ContatoModel contatoDb = ListarPorId(contato.Id);
            if(contatoDb == null) throw new System.Exception("Houve um erro na atualização do contato!");

            contatoDb.Nome = contato.Nome;
            contatoDb.Email = contato.Email;
            contatoDb.Celular = contato.Celular;

            _bancocontext.Contatos.Update(contatoDb);
            _bancocontext.SaveChanges();

            return contatoDb;

        }

        public ContatoModel ListarPorId(int id)
        {
            return _bancocontext.Contatos.FirstOrDefault(x => x.Id == id);
        }
    }
}
