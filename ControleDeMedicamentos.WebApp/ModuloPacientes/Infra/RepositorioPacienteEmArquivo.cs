using System;
using System.Collections.Generic;
using System.Linq;
using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.WebApp.Compartilhado.Dominio;
using ControleDeMedicamentos.WebApp.Compartilhado.Infra.Arquivos;

namespace ControleDeMedicamentos.WebApp.ModuloPacientes;

public class RepositorioPacienteEmArquivo : RepositorioBaseEmArquivo<Paciente>, IRepositorio<Paciente>
{
    public RepositorioPacienteEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Paciente> CarregarRegistros()
    {
        return contexto.
    }
}