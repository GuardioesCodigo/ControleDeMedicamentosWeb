using System;
using System.ComponentModel.DataAnnotations;
namespace ControleDeMedicamentos.WebApp.ModuloPacientes.Views;

public record PacienteViewModel
{
    [Required(ErrorMessage = "O CPF é obrigatório.")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter exatamente 11 dígitos.")]
    public string CPF { get; set; }

    [Required(ErrorMessage = "O cartão do SUS é obrigatório.")]
    [RegularExpression(@"^\d{15}$", ErrorMessage = "O cartão do SUS deve conter exatamente 15 dígitos.")]
    public string CartaoSus { get; set; }

    [Required(ErrorMessage = "O telefone é obrigatório.")]
    [RegularExpression(@"^\(\d{2}\) \d{4,5}-\d{4}$", ErrorMessage = "Formato de telefone inválido. Use (XX) XXXX-XXXX ou (XX) XXXXX-XXXX.")]
    public string Telefone { get; set; }
    }
