using System;
using ControleDeMedicamentos.WebApp.Compartilhado.Dominio;

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

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo \"Nome\" deve ser preenchido.");
        
        else if (Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 3 e 100 caracteres.");

        if (string.IsNullOrWhiteSpace(Telefone))
            erros.Add("O campo \"Telefone\" deve ser preenchido.");

        int contadorDigitosTelefone = 0;
        bool contemLetraOuSimboloTelefone = false;

        for (int i = 0; i < Telefone.Length; i++)
        {
            char caractereAtual = Telefone[i];

            if (char.IsDigit(caractereAtual))
                contadorDigitosTelefone++;

            else
            {
                contemLetraOuSimboloTelefone = true;
                break;
            }
        }
        if (contadorDigitosTelefone < 10 || contadorDigitosTelefone > 11)
            erros.Add("O campo \"Telefone\" deve conter entre 10 e 11 caracteres");
        
        if(contemLetraOuSimboloTelefone)
            erros.Add("O campo \"Telefone\" deve conter apenas números");

        if (string.IsNullOrWhiteSpace(Cpf))
            erros.Add("O campo \"Cpf\" deve ser preenchido");

        int contadorDigitosCpf = 0;
        bool contemLetraOuSimboloCpf = false;

        for (int i = 0; i < Cpf.Length; i++)
        {
            char caractereAtual = Cpf[i];

            if (char.IsDigit(caractereAtual))
                contadorDigitosCpf++;

            else 
            {
                contemLetraOuSimboloCpf = true;
                break;
            }
        }

        if (contadorDigitosCpf != 11)
            erros.Add("O campo \"Cpf\" deve conter 11 caracteres.");

        if(contemLetraOuSimboloCpf)
            erros.Add("O campo \"Cpf\" deve conter apenas números");

        return erros;
    }
}