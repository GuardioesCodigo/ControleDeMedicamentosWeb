using AutoMapper;
using ControleDeMedicamentos.WebApp.Compartilhado.Apresentacao.Extensions;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos.Aplicacao;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamentos.Apresentacao
{
    public class MedicamentosController(ServicoMedicamentos servicoMedicamentos, IMapper mapeador) : Controller
    {
        [HttpGet]
        public ActionResult Listar()
        {
            List<ListarMedicamentosDto> dtos = servicoMedicamentos.SelecionarTodos();
            List<ListarMedicamentosViewModel> listarVms = mapeador.Map<List<ListarMedicamentosViewModel>>(dtos);
            
            return View(listarVms);
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            CadastrarMedicamentosViewModel cadastrarVm = new CadastrarMedicamentosViewModel(
                string.Empty,
                Guid.Empty,
                string.Empty,
                0,
                SelecionarFornecedores()
            );

            return View(cadastrarVm);
        }

        [HttpPost]
        public ActionResult Cadastrar(CadastrarMedicamentosViewModel cadastrarVm)
        {
            if (!ModelState.IsValid)
                return View(cadastrarVm with {Fornecedores = SelecionarFornecedores() });

            CadastrarMedicamentosDto dto = mapeador.Map<CadastrarMedicamentosDto>(cadastrarVm);

            Result resultado = servicoMedicamentos.Cadastrar(dto);

            if (resultado.IsFailed)
            {
                ModelState.AddModelError(resultado);

                ModelState.Remove(nameof(CadastrarMedicamentosViewModel.Fornecedores));

                return View(cadastrarVm with {Fornecedores = SelecionarFornecedores() });
            }

            return RedirectToAction(nameof(Listar));
        }

        private List<OpcaoFornecedoresViewModel> SelecionarFornecedores()
        {
            List<OpcaoFornecedoresDto> dtos = servicoMedicamentos.SelecionarFornecedores();

            return mapeador.Map<List<OpcaoFornecedoresViewModel>>(dtos);
        }

    }
}
