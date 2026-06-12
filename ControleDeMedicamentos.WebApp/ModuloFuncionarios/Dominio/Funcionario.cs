using System;
using ControleDeMedicamentos.WebApp.Compartilhado.Dominio;
using System.Linq; // Necessário para o .All()
using System.Text.RegularExpressions;
namespace ControleDeMedicamentos.WebApp.ModuloFuncionario.Dominio;

public class Funcionario : EntidadeBase<Funcionario>
{
    public string Nome {get; set;}
    public string Telefone {get; set;}
    public string Cpf {get ;set;}
    public Funcionario(string nome, string telefone, string cpf)
    {
        Nome = nome;
        Telefone = telefone;
        Cpf = cpf;
    }

    public override void Atualizar(Funcionario entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Telefone = entidadeAtualizada.Telefone;
        Cpf = entidadeAtualizada.Cpf;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        // Validação de Nome
        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo \"Nome\" deve ser preenchido.");
        else if (Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 3 e 100 caracteres.");

        // Validação de Telefone (Regex para 11 dígitos)
        if (string.IsNullOrWhiteSpace(Telefone))
        {
            erros.Add("O campo \"Telefone\" deve ser preenchido.");
        }
        else if (!Regex.IsMatch(Telefone, @"^\d{11}$"))
        {
            erros.Add("O campo \"Telefone\" deve conter exatamente 11 dígitos.");
        }

        // Validação de CPF (Regex para 11 dígitos)
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