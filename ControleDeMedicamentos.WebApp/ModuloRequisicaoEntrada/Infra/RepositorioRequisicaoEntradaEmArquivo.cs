using System;
using ControleDeMedicamentos.WebApp.Compartilhado.Infra.Arquivos;
using ControleDeMedicamentos.WebApp.ModuloEstoque.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.Infra;

public class RepositorioRequisicaoEntradaEmArquivo : RepositorioBaseEmArquivo<RequisicaoEntrada>, IRepositorioRequisicaoEntrada
{
    public RepositorioRequisicaoEntradaEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<RequisicaoEntrada> CarregarRegistros()
    {
        return contexto.requisicaoEntradas;
    }
}
