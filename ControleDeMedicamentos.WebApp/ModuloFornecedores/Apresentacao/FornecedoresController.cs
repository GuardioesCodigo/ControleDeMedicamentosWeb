using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloFornecedores.Aplicacao;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedores.Apresentacao
{
    public class FornecedoresController(IMapper mapeador, ServicoFornecedores servicoFornecedores) : Controller
    {
        [HttpGet]
        public ActionResult Listar()
        {
            List<ListarFornecedoresDto> dtos = servicoFornecedores.SelecionarTodos();

            List<ListarFornecedoresViewModel> listarVms = mapeador.Map<List<ListarFornecedoresDto>>(dtos)
                .Select(f => new ListarFornecedoresViewModel(f.Id, f.Nome, f.Telefone, f.CNPJ))
                .ToList();

            return View(listarVms);
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            CadastrarFornecedoresViewModel cadastrarVm = new CadastrarFornecedoresViewModel(
                string.Empty, 
                string.Empty,
                string.Empty);

            return View(cadastrarVm);
        }

        [HttpPost]
        public ActionResult Cadastrar(CadastrarFornecedoresViewModel cadastrarVm)
        {
            if (!ModelState.IsValid)
                return View(cadastrarVm);

            CadastrarFornecedoresDto dtos = mapeador.Map<CadastrarFornecedoresDto> (cadastrarVm);
            Result resultado = servicoFornecedores.Cadastrar(dtos);

          if (resultado.IsFailed)
            {
                foreach (IError erro in resultado.Errors)
                {
                    string campo =
                        erro.Metadata["Campo"] is string
                            ? erro.Metadata["Campo"].ToString()!
                            : string.Empty;

                    ModelState.AddModelError(campo, erro.Message);
                }

                return View(cadastrarVm);
            }

            return RedirectToAction(nameof(Listar)); 
        }   
    }
}
