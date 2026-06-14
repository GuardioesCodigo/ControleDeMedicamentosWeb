using System;
using ControleDeMedicamentos.WebApp.Compartilhado.Infra.Arquivos;
using ControleDeMedicamentos.WebApp.ModuloRequisicaoSaida.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloRequisicaoSaida.Infra;

public class RepositorioRequisicaoSaidaEmArquivo : RepositorioBaseEmArquivo<RequisicaoSaida>, IRepositorioRequisicaoSaida
{
    public RepositorioRequisicaoSaidaEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<RequisicaoSaida> CarregarRegistros()
    {
        return contexto.requisicaoSaida;
    }

    
}
