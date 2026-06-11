using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentos.WebApp.ModuloPacientes.Views;

// Usada para Cadastrar e Editar (contém todos os dados e validações)
public class PacienteViewModel
{
    public Guid? Id { get; set; } // O ID é nulo no cadastro, mas preenchido na edição

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O telefone é obrigatório.")]
    public string Telefone { get; set; } = string.Empty;

    [Required(ErrorMessage = "O cartão do SUS é obrigatório.")]
    public string CartaoSus { get; set; } = string.Empty;

    [Required(ErrorMessage = "O CPF é obrigatório.")]
    public string CPF { get; set; } = string.Empty;
}

// Usada na Listagem (Index) - O foco aqui é exibir os dados e permitir ações
public class ListarPacienteViewModel
{
    public Guid Id { get; set; } // Necessário para gerar o link de Editar/Excluir

    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string CartaoSus { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
}

// Usada para confirmar a exclusão (evita erros e confirma a intenção)
public class ExcluirPacienteViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string CartaoSus { get; set; } = string.Empty;
}