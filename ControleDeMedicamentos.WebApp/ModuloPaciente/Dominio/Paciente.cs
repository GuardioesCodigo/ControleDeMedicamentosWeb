using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ControleDeMedicamentos.WebApp.Compartilhado.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloPacientes.Dominio;

public class Paciente : EntidadeBase<Paciente>
{
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string CartaoSus { get; set; }
    public string Cpf { get; set; } // Padronizado como 'Cpf' (minúsculas no restante)

    // Construtor vazio (necessário para o AutoMapper)
    public Paciente() { }

    public Paciente(string nome, string telefone, string cartaoSus, string cpf)
    {
        Nome = nome;
        Telefone = telefone;
        CartaoSus = cartaoSus;
        Cpf = cpf;
    }

    public override void Atualizar(Paciente entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Telefone = entidadeAtualizada.Telefone;
        CartaoSus = entidadeAtualizada.CartaoSus;
        Cpf = entidadeAtualizada.Cpf;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo \"Nome\" deve ser preenchido.");
        else if (Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 3 e 100 caracteres.");

        if (string.IsNullOrWhiteSpace(Telefone))
        {
            erros.Add("O campo \"Telefone\" deve ser preenchido.");
        }
        else if (!Regex.IsMatch(Telefone, @"^\d{11}$"))
        {
            erros.Add("O campo \"Telefone\" deve conter exatamente 11 dígitos.");
        }

        if (string.IsNullOrWhiteSpace(CartaoSus))
        {
            erros.Add("O campo \"Cartão SUS\" deve ser preenchido.");
        }
        else if (!Regex.IsMatch(CartaoSus, @"^\d{15}$"))
        {
            erros.Add("O campo \"Cartão SUS\" deve conter exatamente 15 dígitos.");
        }

        if (string.IsNullOrWhiteSpace(Cpf))
        {
            erros.Add("O campo \"Cpf\" deve ser preenchido.");
        }
        else if (!Regex.IsMatch(Cpf, @"^\d{11}$"))
        {
            erros.Add("O campo \"Cpf\" deve conter exatamente 11 dígitos numéricos.");
        }

        return erros;
    }
}