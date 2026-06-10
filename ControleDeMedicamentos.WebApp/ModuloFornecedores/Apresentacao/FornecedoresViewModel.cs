using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedores.Apresentacao;

public record ListarFornecedoresViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string CNPJ
);

public record CadastrarFornecedoresViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Telefone\" deve ser preenchido.")]
    [RegularExpression( @"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$", ErrorMessage = "O campo \"Telefone\" deve estar em um formato válido.")]
    string Telefone,

    [Required(ErrorMessage = "O campo \"CNPJ\" deve ser preenchido.")]
    [RegularExpression( @"^\d{14}$", ErrorMessage = "O campo \"CNPJ\" deve conter 14 dígitos.")]
    string CNPJ
);