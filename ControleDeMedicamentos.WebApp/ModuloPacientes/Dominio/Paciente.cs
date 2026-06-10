using System;
using System.Text.RegularExpressions;
using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.Compartilhado.Dominio;
using Microsoft.VisualBasic;

namespace ControleDeMedicamentos.WebApp.ModuloPacientes;

public class Paciente : EntidadeBase
{
    public string Nome { get; set; } = string.Empty;

    public string Telefone { get; set; } = string.Empty;

    public string CartaoSus { get; set; } = string.Empty;

    public string CPF { get; set; } = string.Empty;
    public override void AtualizarDados(EntidadeBase entidadeAtualizada)
    {
        Paciente pacienteAtualizado = (Paciente)entidadeAtualizada;

        Nome = pacienteAtualizado.Nome;
        Telefone = pacienteAtualizado.Telefone;
        CartaoSus = pacienteAtualizado.CartaoSus;
        CPF = pacienteAtualizado.CPF;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O nome é obrigatório.");

        else if (Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O nome deve ter entre 3 e 100 caracteres.");

        bool telefoneValido = Regex.IsMatch(Telefone, @"^\(\d{2}\)\s\d{4,5}-\d{4}$");

        if (!telefoneValido)
            erros.Add("Telefone inválido.");

        bool susValido = Regex.IsMatch(CartaoSus, @"^\d{15}$");

        if (!susValido)
            erros.Add("Cartão SUS deve conter 15 digitos.");

        bool cpfValido = Regex.IsMatch(CPF, @"^\d{11}$");

        if (!cpfValido)
            erros.Add("CPF deve conter 11 dígitos.");

        return erros;
    }

}