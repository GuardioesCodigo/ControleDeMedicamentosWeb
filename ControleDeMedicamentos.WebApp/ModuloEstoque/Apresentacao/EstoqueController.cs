using ControleDeMedicamentos.WebApp.ModuloRequisicaoEntrada.Aplicacao;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.Apresentacao;


public class EstoqueController : Controller
{
    private readonly ServicoRequisicaoEntrada servicoRequisicaoEntrada;

    public EstoqueController(ServicoRequisicaoEntrada servicoEntrada)
    {
        servicoRequisicaoEntrada = servicoEntrada;
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
                    e.MedicamentoNome,
                    "Entrada",
                    e.Quantidade
                )
            );
        }

        movimentacoes = movimentacoes
            .OrderByDescending(x => x.Data)
            .ToList();

        return View(movimentacoes);
    }

}

