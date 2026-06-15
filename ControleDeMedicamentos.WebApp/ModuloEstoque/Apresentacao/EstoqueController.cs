using ControleDeMedicamentos.WebApp.ModuloRequisicaoEntrada.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloRequisicaoSaida.Aplicacao;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.Apresentacao;


public class EstoqueController : Controller
{
    private readonly ServicoRequisicaoEntrada servicoRequisicaoEntrada;
    private readonly ServicoRequisicaoSaida servicoRequisicaoSaida;

    public EstoqueController(ServicoRequisicaoEntrada servicoEntrada, ServicoRequisicaoSaida servicoRequisicaoSaida)
    {
        servicoRequisicaoEntrada = servicoEntrada;
        this.servicoRequisicaoSaida = servicoRequisicaoSaida;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        return View();
    }

    [HttpGet]
    public IActionResult ListarMovimentacoes()
    {
        List<ListarMovimentacoesViewModel> movimentacoes = new();

        var entradas = servicoRequisicaoEntrada.SelecionarTodos();

        foreach (var e in entradas)
        {
            movimentacoes.Add(
                new ListarMovimentacoesViewModel(
                    e.Data,
                    "Entrada",
                    e.MedicamentoNome,
                    e.Quantidade
                )
            );
        }

        var saidas = servicoRequisicaoSaida.SelecionarTodos();

        foreach (var s in saidas)
        {
            movimentacoes.Add(
                new ListarMovimentacoesViewModel(
                    s.Data,
                    "Saída",
                    $"{s.PacienteNome} - {s.ResumoMedicamentos}",
                    s.QuantidadeTotal
                )
            );
        }

        movimentacoes = movimentacoes
            .OrderByDescending(x => x.Data)
            .ToList();

        return View(movimentacoes);
    }

}

