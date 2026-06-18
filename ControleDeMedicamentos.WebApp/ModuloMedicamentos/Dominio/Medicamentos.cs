using System;
using ControleDeMedicamentos.WebApp.Compartilhado.Dominio;
using ControleDeMedicamentos.WebApp.ModuloFornecedores.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamentos.Dominio;

public class Medicamento : EntidadeBase<Medicamento>
{
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public Fornecedor Fornecedor { get; set; } = null!;
    public bool EstaEmFalta => Quantidade < 20;

    public void AdicionarQuantidade(int quantidade)
    {
        if (quantidade <= 0)
            throw new Exception("Quantidade inválida.");

        Quantidade += quantidade;
    }

    public Medicamento() { }

    public Medicamento(string nome, string descricao, int quantidade, Fornecedor fornecedor)
    {
        Nome = nome;
        Descricao = descricao;
        Quantidade = quantidade;
        Fornecedor = fornecedor;
    }
    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo \"Nome\" deve ser preenchido.");

        if (Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 3 e 100 caracteres.");

        if (Descricao.Length < 5 || Descricao.Length > 255)
            erros.Add("O campo \"Descrição\" deve conter entre 5 e 255 caracteres.");

        if (Quantidade < 0)
            erros.Add("O campo \"Quantidade\" deve conter um valor maior ou igual a 0.");

        if (Fornecedor == null)
            erros.Add("O campo \"Fornecedor\" deve ser preenchido.");

        return erros;
    }
    
    public override void Atualizar(Medicamento entidadeAtualizada)
    {
        this.Nome = entidadeAtualizada.Nome;
        this.Descricao = entidadeAtualizada.Descricao;
        this.Quantidade = entidadeAtualizada.Quantidade;
        this.Fornecedor = entidadeAtualizada.Fornecedor;
    }

    public void SubtrairQuantidade(int quantidade)
    {
        if (quantidade <= 0)
            throw new Exception("A quantidade para retirada deve ser maior que zero.");

        if (quantidade > Quantidade)
            throw new Exception($"Estoque insuficiente. Disponível: {Quantidade}");

        Quantidade -= quantidade;
    }
}
