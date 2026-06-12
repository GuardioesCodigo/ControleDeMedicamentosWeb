using ControleDeMedicamentos.WebApp.ModuloPacientes.Dominio;
using ControleDeMedicamentos.WebApp.Compartilhado.Infra.Arquivos;
using ControleDeMedicamentos.WebApp.Compartilhado.Dominio;
using ControleDeMedicamentos.WebApp.ModuloPacientes.Apresentacao; // Onde está o seu ContextoJson
using AutoMapper;
namespace ControleDeMedicamentos.WebApp.ModuloPacientes.Aplicacao;

public class ServicoPaciente
{
    private readonly IRepositorio<Paciente> _repositorio;
    private readonly ContextoJson _contexto;
    private readonly IMapper _mapper;

    // Construtor simples: apenas o que você precisa
    public ServicoPaciente(IRepositorio<Paciente> repositorio, ContextoJson contexto, IMapper mapper)
    {
        _repositorio = repositorio;
        _contexto = contexto;
        _mapper = mapper;
    }

    public void Cadastrar(Paciente paciente)
    {
        if (paciente.Id == Guid.Empty)
            paciente.Id = Guid.NewGuid();
        
        // 1. Validação de formato (Campos obrigatórios, Regex, etc)
        var erros = paciente.Validar();
        if (erros.Count > 0)
            throw new Exception(string.Join(" | ", erros));

        // 2. Validação de Regra de Negócio: Cartão SUS
        if (_repositorio.SelecionarTodos().Any(x => x.CartaoSus == paciente.CartaoSus))
            throw new Exception("Já existe um paciente com este cartão do SUS.");
        
        // 3. ADICIONE ESTA VALIDAÇÃO: CPF único
        if (_repositorio.SelecionarTodos().Any(x => x.Cpf == paciente.Cpf))
            throw new Exception("Já existe um paciente cadastrado com este CPF.");

        // 4. Persistência
        _repositorio.Cadastrar(paciente);
        _contexto.Salvar();
    }
    public void Excluir(Guid id)
    {
        _repositorio.Excluir(id);
        _contexto.Salvar();
    }

    public void Editar(EditarPacienteViewModel model)
    {
        // 1. Mapeia a ViewModel para o objeto de domínio (Entidade)
        var pacienteEditado = _mapper.Map<Paciente>(model);

        // 2. Valida o objeto de domínio
        var erros = pacienteEditado.Validar();
        if (erros.Count > 0)
            throw new Exception(string.Join(" | ", erros));

        // 3. Regra de Negócio
        if (_repositorio.SelecionarTodos().Any(x => x.Cpf == pacienteEditado.Cpf && x.Id != pacienteEditado.Id))
            throw new Exception("Já existe outro paciente com este CPF.");

        // 4. Persiste
        _repositorio.Editar(pacienteEditado.Id, pacienteEditado);
        _contexto.Salvar();
    }

    public List<Paciente> SelecionarTodos() => _repositorio.SelecionarTodos();
    
    public Paciente? SelecionarPorId(Guid id) => _repositorio.SelecionarPorId(id);
}