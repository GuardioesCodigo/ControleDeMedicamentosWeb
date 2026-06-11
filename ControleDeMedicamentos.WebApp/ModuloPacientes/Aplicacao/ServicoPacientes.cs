using ControleDeMedicamentos.WebApp.ModuloPacientes.Dominio;
using ControleDeMedicamentos.WebApp.Compartilhado.Infra.Arquivos;
using ControleDeMedicamentos.WebApp.Compartilhado.Dominio; // Onde está o seu ContextoJson

namespace ControleDeMedicamentos.WebApp.ModuloPacientes.Aplicacao;

public class ServicoPaciente
{
    private readonly IRepositorio<Paciente> _repositorio;
    private readonly ContextoJson _contexto;

    // Construtor simples: apenas o que você precisa
    public ServicoPaciente(IRepositorio<Paciente> repositorio, ContextoJson contexto)
    {
        _repositorio = repositorio;
        _contexto = contexto;
    }

    public void Cadastrar(Paciente novoPaciente)
    {
        if (_repositorio.SelecionarTodos().Any(x => x.CartaoSus == novoPaciente.CartaoSus))
            throw new Exception("Já existe um paciente com este cartão do SUS.");

        if (_repositorio.SelecionarTodos().Any(x => x.CPF == novoPaciente.CPF))
            throw new Exception("Já existe um paciente com este CPF.");

        _repositorio.Cadastrar(novoPaciente);
        _contexto.Salvar(); // O Serviço garante que a alteração seja persistida
    }

    public void Editar(Paciente pacienteAtualizado)
    {
        if (_repositorio.SelecionarTodos().Any(p => p.CartaoSus == pacienteAtualizado.CartaoSus && p.Id != pacienteAtualizado.Id))
            throw new Exception("Já existe outro paciente com este cartão do SUS.");

        _repositorio.Editar(pacienteAtualizado.Id, pacienteAtualizado);
        _contexto.Salvar();
    }

    public void Excluir(Guid id)
    {
        _repositorio.Excluir(id);
        _contexto.Salvar();
    }

    public List<Paciente> SelecionarTodos() => _repositorio.SelecionarTodos();
    
    public Paciente? SelecionarPorId(Guid id) => _repositorio.SelecionarPorId(id);
}