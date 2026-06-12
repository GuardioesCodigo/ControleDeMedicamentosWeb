using System;
using ControleDeMedicamentos.WebApp.Compartilhado.Dominio;
using ControleDeMedicamentos.WebApp.Compartilhado.Infra.Arquivos;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloFuncionario.Infra;

public class RepositorioFuncionarioEmArquivo : RepositorioBaseEmArquivo<Funcionario>, IRepositorioFuncionario
{
    public RepositorioFuncionarioEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Funcionario> CarregarRegistros()
    {
        return contexto.funcionarios;
    }
}
