using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamentos.Apresentacao;


public record OpcaoFornecedoresViewModel(
    Guid Id,
    string Nome,
    string Cnpj
);  

public record ListarMedicamentosViewModel(
    Guid Id,
    string Nome,
    string Descricao,
    int Quantidade,
    Guid FornecedorId,
    string FornecedorNome
);

public record CadastrarMedicamentosViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "Selecione um fornecedor.")]
    Guid FornecedorId,

    [Required(ErrorMessage = "O campo \"Descricao\" deve ser preenchido.")]
    [StringLength(255, MinimumLength = 5, ErrorMessage = "O campo \"Descricao\" deve conter entre 5 e 255 caracteres.")]
    string Descricao,
 
    [Range(0,int.MaxValue, ErrorMessage = "O campo \"Preço Aproximado\" deve conter um valor maior ou igual a 0.")]
    int Quantidade,

    [ValidateNever]
    List<OpcaoFornecedoresViewModel> Fornecedores
);

public record EditarMedicamentosViewModel(
    Guid Id,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "Selecione um fornecedor.")]
    Guid FornecedorId,

    [Required(ErrorMessage = "O campo \"Descricao\" deve ser preenchido.")]
    [StringLength(255, MinimumLength = 5, ErrorMessage = "O campo \"Descricao\" deve conter entre 5 e 255 caracteres.")]
    string Descricao,
 
    [Range(0,int.MaxValue, ErrorMessage = "O campo \"Preço Aproximado\" deve conter um valor maior ou igual a 0.")]
    int Quantidade,

    [ValidateNever]
    List<OpcaoFornecedoresViewModel> Fornecedores
);

public record ExcluirMedicamentosViewModel(
    Guid Id,
    string Nome,
    string Descricao,
    int Quantidade,
    Guid FornecedorId,
    string FornecedorNome
);