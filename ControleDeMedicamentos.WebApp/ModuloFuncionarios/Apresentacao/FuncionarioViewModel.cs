using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentos.WebApp.ModuloFuncionarios.Apresentacao;

public class FuncionarioViewModel
{
    public Guid Id { get; set; }
    
    public string CPF { get; set; } = string.Empty;
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Formato de telefone inválido.")]
    [Required(ErrorMessage = "O telefone é obrigatório.")]
    public string Telefone { get; set; } = string.Empty;

    [Required(ErrorMessage = "O CPF é obrigatório.")]
    public string Cpf { get; set; } = string.Empty;
}

public class ListarFuncionarioViewModel
{
    public Guid Id { get; set; } // Necessário para gerar o link de Editar/Excluir

    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
}

public class ExcluirFuncionarioViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;

}


public class EditarFuncionarioViewModel
{
    public Guid Id { get; set; } // Obrigatório para o Editar

    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string Nome { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Formato de telefone inválido.")]
    [Required(ErrorMessage = "O telefone é obrigatório.")]
    public string Telefone { get; set; } = string.Empty;

    [Required(ErrorMessage = "O CPF é obrigatório.")]
    public string Cpf { get; set; } = string.Empty;
}