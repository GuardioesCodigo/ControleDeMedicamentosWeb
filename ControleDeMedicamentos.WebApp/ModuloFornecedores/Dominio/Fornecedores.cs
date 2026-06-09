using ControleDeMedicamentos.WebApp.Compartilhado.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedores.Dominio;

public class Fornecedores : EntidadeBase<Fornecedores>
{

    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;

    public Fornecedores() { }

    public Fornecedores(
        string nome, 
        string telefone, 
        string cnpj
    )
    {
        Nome = nome;
        Telefone = telefone;
        Cnpj = cnpj;
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
        else
        {
            int contadorDigitos = 0;

            foreach (char caractere in Telefone)
            {
                if (char.IsDigit(caractere))
                    contadorDigitos++;
                else if (caractere != '(' &&
                        caractere != ')' &&
                        caractere != '-' &&
                        caractere != ' ')
                {
                    erros.Add("O campo \"Telefone\" possui um formato inválido.");
                    break;
                }
            }

            if (contadorDigitos < 10 || contadorDigitos > 11)
                erros.Add("O campo \"Telefone\" deve conter 10 ou 11 dígitos.");
        }

        if (string.IsNullOrWhiteSpace(Cnpj))
        {
            erros.Add("O campo \"CNPJ\" deve ser preenchido.");
        }
        else
        {
            int contadorDigitos = 0;

            foreach (char caractere in Cnpj)
            {
                if (char.IsDigit(caractere))
                    contadorDigitos++;
            }

            if (contadorDigitos != 14)
                erros.Add("O campo \"CNPJ\" deve conter 14 dígitos.");
        }

        return erros;
    }

    public override void Atualizar(Fornecedores entidadeAtualizada)
    {
        Fornecedores fornecedorAtualizado = (Fornecedores)entidadeAtualizada;

        Nome = fornecedorAtualizado.Nome;
        Telefone = fornecedorAtualizado.Telefone;
        Cnpj = fornecedorAtualizado.Cnpj;
    }
}