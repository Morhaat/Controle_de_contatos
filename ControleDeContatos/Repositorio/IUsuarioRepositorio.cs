using ControleDeContatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeContatos.Repositorio
{
    public interface IUsuarioRepositorio
    {
        List<UsuarioModel> BuscarTodos();
        UsuarioModel Adicionar(UsuarioModel usuario);
        UsuarioModel Atualizar(UsuarioModel usuario);
        bool Deletar(int id);
        UsuarioModel ListarPorId(int id);
        UsuarioModel BuscaPorLogin(string login);
        UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel);
    }
}
