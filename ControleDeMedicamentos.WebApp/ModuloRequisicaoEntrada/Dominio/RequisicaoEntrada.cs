using System;
using ControleDeMedicamentos.WebApp.Compartilhado.Dominio;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Dominio;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.Dominio;

public class RequisicaoEntrada : EntidadeBase<RequisicaoEntrada>
{
    public Medicamentos Medicamento { get; set; } = null!;
    public Funcionario Funcionario { get; set; } = null!;
    public int Quantidade { get; set; }
    public DateTime Data { get; set; }

    public RequisicaoEntrada()
    {
    }


    public RequisicaoEntrada(Medicamentos medicamento, Funcionario funcionario, int quantidade, DateTime data)
    {
        Medicamento = medicamento;
        Funcionario = funcionario;
        Quantidade = quantidade;
        Data = data;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (Data == default)
            erros.Add("O campo \"Data\" é obrigatória.");

        else if (Data.Date > DateTime.Today)
            erros.Add("O campo \"Data\" não pode ser futura.");

        if (Medicamento == null)
            erros.Add("O campo \"Medicamento\" é obrigatório.");

        if (Funcionario == null)
            erros.Add("O campo \"Funcionario\" é obrigatório.");

        if (Quantidade <= 0)
            erros.Add("O campo \"Quantidade\" deve conter um valor maior ou igual a 0.");

        return erros;
    }

    public override void Atualizar(RequisicaoEntrada entidadeAtualizada)
    {
        this.Medicamento = entidadeAtualizada.Medicamento;
        this.Funcionario = entidadeAtualizada.Funcionario;
        this.Quantidade = entidadeAtualizada.Quantidade;
        this.Data = entidadeAtualizada.Data;
    }
}
