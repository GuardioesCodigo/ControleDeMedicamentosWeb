using System;

namespace ControleDeMedicamentos.WebApp.ModuloFuncionario.Aplicacao;

// DTO para cadastrar um novo paciente (sem Id, pois ele é gerado pelo sistema)
public class CadastrarFuncionarioDTO
{
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
}

// DTO para Editar (obrigatoriamente precisa do Id para localizar o registro no banco)
public class EditarFuncionarioDTO
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
}

// DTO para Listagens (Otimizado)
public class ListarFuncionarioDTO
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
}

// DTO para confirmação de exclusão
public class ExcluirFuncionarioDTO
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
}