using System.ComponentModel.DataAnnotations;
using ControleDeMedicamentos.WebApp.ModuloFornecedores.Dominio;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ControleDeMedicamentos.WebApp.ModuloRequisicaoEntrada.Apresentacao;

public record OpcaoMedicamentosViewModel(
    Guid Id,
    string Nome,
    Fornecedores Fornecedor
);

public record OpcaoFuncionariosViewModel(
    Guid Id,
    string Nome,
    string Cpf
);
public record ListarRequisicaoEntradaViewModel(
    Guid Id,
    DateTime Data,
    Guid MedicamentoId,
    string MedicamentoNome,
    Guid FuncionarioId,
    string FuncionarioNome,
    int Quantidade
);

public record CadastrarRequisicaoEntradaViewModel(
    [Required(ErrorMessage = "O campo \"Data\" é obrigatória.")]
    [DataType(DataType.Date)]
    DateTime? Data,

    [Required(ErrorMessage = "O campo \"Medicamento\" é obrigatório.")]
    Guid MedicamentoId,

    [Required(ErrorMessage = "O campo \"Funcionario\" é obrigatório.")]
    Guid FuncionarioId,

    [Required(ErrorMessage = "O campo \"Quantidade\" é obrigatória.")]
    [Range(1, int.MaxValue, ErrorMessage = "O campo \"Quantidade\" deve ser maior que zero.")]
    int Quantidade,

    [ValidateNever]
    List<OpcaoFuncionariosViewModel> Funcionarios,

    [ValidateNever]
    List<OpcaoMedicamentosViewModel> Medicamentos
);
