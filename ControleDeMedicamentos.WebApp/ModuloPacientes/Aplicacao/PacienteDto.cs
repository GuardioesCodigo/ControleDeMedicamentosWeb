namespace ControleDeMedicamentos.WebApp.ModuloPacientes.DTO;

// DTO para cadastrar um novo paciente (sem Id, pois ele é gerado pelo sistema)
public class CadastrarPacienteDTO
{
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string CartaoSus { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
}

// DTO para Editar (obrigatoriamente precisa do Id para localizar o registro no banco)
public class EditarPacienteDTO
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string CartaoSus { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
}

// DTO para Listagens (Otimizado)
public class ListarPacienteDTO
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string CartaoSus { get; set; } = string.Empty;
}

// DTO para confirmação de exclusão
public class ExcluirPacienteDTO
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string CartaoSus { get; set; } = string.Empty;
}