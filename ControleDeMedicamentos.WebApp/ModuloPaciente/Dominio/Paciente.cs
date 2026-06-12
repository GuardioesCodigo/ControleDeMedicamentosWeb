using System;
using System.Text.RegularExpressions;
using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.Compartilhado.Dominio;
using Microsoft.VisualBasic;

namespace ControleDeMedicamentos.WebApp.ModuloPacientes.Dominio;

public class Paciente : EntidadeBase<Paciente>
{
    public string Nome { get; set; } = string.Empty;

    public string Telefone { get; set; } = string.Empty;

    public string CartaoSus { get; set; } = string.Empty;

    public string CPF { get; set; } = string.Empty;
    public override void Atualizar(Paciente entidadeAtualizada)
    {

        Nome = entidadeAtualizada.Nome;
        Telefone = entidadeAtualizada.Telefone;
        CartaoSus = entidadeAtualizada.CartaoSus;
        CPF = entidadeAtualizada.CPF;
    }

        public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        // Validação de Nome
        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O nome é obrigatório.");
        else if (Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O nome deve ter entre 3 e 100 caracteres.");

        // Validação de Telefone
        if (string.IsNullOrWhiteSpace(Telefone))
            erros.Add("O campo \"Telefone\" deve ser preenchido.");
        else if (!Regex.IsMatch(Telefone, @"^\d{11}$"))
            erros.Add("O campo \"Telefone\" deve conter exatamente 11 dígitos numéricos.");

        // Validação de Cartão SUS (15 dígitos)
        if (string.IsNullOrWhiteSpace(CartaoSus))
            erros.Add("O campo \"Cartão SUS\" deve ser preenchido.");
        else if (!Regex.IsMatch(CartaoSus, @"^\d{15}$"))
            erros.Add("O cartão SUS deve conter exatamente 15 dígitos numéricos.");

        // Validação de CPF (11 dígitos)
        if (string.IsNullOrWhiteSpace(CPF))
            erros.Add("O campo \"CPF\" deve ser preenchido.");
        else if (!Regex.IsMatch(CPF, @"^\d{11}$"))
            erros.Add("O CPF deve conter exatamente 11 dígitos numéricos.");

        return erros;
    }

}