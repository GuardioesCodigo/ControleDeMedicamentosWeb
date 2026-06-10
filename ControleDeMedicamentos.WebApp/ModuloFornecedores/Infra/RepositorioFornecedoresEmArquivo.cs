using System;
using ControleDeMedicamentos.WebApp.Compartilhado.Dominio;
using ControleDeMedicamentos.WebApp.Compartilhado.Infra.Arquivos;
using ControleDeMedicamentos.WebApp.ModuloFornecedores.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedores.Infra;

public class RepositorioFornecedoresEmArquivo : RepositorioBaseEmArquivo<Fornecedores>, IRepositorioFornecedores
{
    public RepositorioFornecedoresEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Fornecedores> CarregarRegistros()
    {
        return contexto.fornecedores;
    }
}