using System;
using ControleDeMedicamentos.WebApp.Compartilhado.Dominio;
using ControleDeMedicamentos.WebApp.ModuloFornecedores.Dominio;
using FluentResults;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedores.Aplicacao;

public class ServicoFornecedores
{
    private readonly IRepositorio<Fornecedores> repositorioFornecedores;

    public ServicoFornecedores(IRepositorio<Fornecedores> repositorioFornecedores)
    {
        this.repositorioFornecedores = repositorioFornecedores;
    }

    public List<ListarFornecedoresDto> SelecionarTodos()
    {
        List<Fornecedores> fornecedores = repositorioFornecedores.SelecionarTodos();

        return fornecedores
            .Select(f => new ListarFornecedoresDto(f.Id, f.Nome, f.Telefone, f.Cnpj))
            .ToList();
    }
}
