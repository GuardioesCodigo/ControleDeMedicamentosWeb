using ControleDeMedicamentos.WebApp.Compartilhado.Dominio;
using ControleDeMedicamentos.WebApp.Compartilhado.Infra.Arquivos;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Dominio;
using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloFuncionarios.Apresentacao;

public class ServicoFuncionario
{
    private readonly IRepositorio<Funcionario> _repositorio;
    private readonly ContextoJson _contexto;
    private readonly IMapper _mapper;

    public ServicoFuncionario(IRepositorio<Funcionario> repositorio, ContextoJson contexto, IMapper mapper)
    {
        _repositorio = repositorio;
        _contexto = contexto;
        _mapper = mapper;
    }

    public void Cadastrar(FuncionarioViewModel model)
    {
        // 1. Mapeia a ViewModel para o domínio
        var novoFuncionario = _mapper.Map<Funcionario>(model);

        // 2. Executa as validações do domínio
        var erros = novoFuncionario.Validar();
        if (erros.Count > 0)
            throw new Exception(string.Join(" | ", erros));
        
        // 3. Validação de regra de negócio (CPF único)
        if (_repositorio.SelecionarTodos().Any(x => x.Cpf == novoFuncionario.Cpf))
            throw new Exception("Já existe um funcionário com este CPF.");

        // 4. Persistência
        novoFuncionario.Id = Guid.NewGuid();
        _repositorio.Cadastrar(novoFuncionario);
        _contexto.Salvar();
    }

    public void Editar(EditarFuncionarioViewModel model)
    {
        // 1. Mapeia a ViewModel para o domínio
        var funcionarioEditado = _mapper.Map<Funcionario>(model);

        // 2. Executa as validações do domínio
        var erros = funcionarioEditado.Validar();
        if (erros.Count > 0)
            throw new Exception(string.Join(" | ", erros));

        // 3. Validação de regra de negócio (CPF único, ignorando o próprio funcionário)
        bool existeOutroComCpf = _repositorio.SelecionarTodos()
            .Any(x => x.Cpf == funcionarioEditado.Cpf && x.Id != funcionarioEditado.Id);

        if (existeOutroComCpf)
            throw new Exception("Já existe outro funcionário cadastrado com este CPF.");

        // 4. Persistência (não geramos Guid.NewGuid pois o ID já existe)
        _repositorio.Editar(funcionarioEditado.Id, funcionarioEditado);
        _contexto.Salvar();
    }

    

    public void Excluir(Guid id)
    {
        _repositorio.Excluir(id);
        _contexto.Salvar();
    }

    public List<Funcionario> SelecionarTodos() => _repositorio.SelecionarTodos();
    
    public Funcionario? SelecionarPorId(Guid id) => _repositorio.SelecionarPorId(id);

    // Implementar Editar e Excluir seguindo a mesma lógica...
}