using System;
using ControleDeMedicamentos.WebApp.Compartilhado.Dominio;
using ControleDeMedicamentos.WebApp.Compartilhado.Infra.Arquivos;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamentos.Infra;

public class RepositorioMedicamentosEmArquivo : RepositorioBaseEmArquivo<Medicamentos>, IRepositorioMedicamentos
{
    public RepositorioMedicamentosEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Medicamentos> CarregarRegistros()
    {
        return contexto.medicamentos;
    }
}
