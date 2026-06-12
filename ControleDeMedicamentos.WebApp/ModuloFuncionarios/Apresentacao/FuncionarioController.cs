using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Dominio;
using ControleDeMedicamentos.WebApp.ModuloFuncionarios.Apresentacao;

namespace ControleDeMedicamentos.WebApp.ModuloFuncionario.Apresentacao
{
    public class FuncionariosController : Controller
    {
        private readonly ServicoFuncionario _servico;
        private readonly IMapper _mapper;

        public FuncionariosController(ServicoFuncionario servico, IMapper mapper)
        {
            _servico = servico;
            _mapper = mapper;
        }

        public IActionResult Listar()
        {
            var funcionarios = _servico.SelecionarTodos();
            var model = _mapper.Map<IEnumerable<FuncionarioViewModel>>(funcionarios);
            return View(model);
        }

        [HttpGet]
        public IActionResult Cadastrar() => View(new FuncionarioViewModel());

        [HttpPost]
        public IActionResult Cadastrar(FuncionarioViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                // O controller apenas passa o modelo para o serviço
                _servico.Cadastrar(model); 
                return RedirectToAction("Listar");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Editar(Guid id)
        {
            var funcionario = _servico.SelecionarPorId(id);
            if (funcionario == null) return NotFound();

            var model = _mapper.Map<FuncionarioViewModel>(funcionario);
            return View(model);
        }

        [HttpPost]
        public IActionResult Editar(Guid id, FuncionarioViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                model.Id = id;
                var funcionario = _mapper.Map<Funcionario>(model);
                _servico.Editar(funcionario);
                return RedirectToAction("Listar");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Excluir(Guid id)
        {
            var funcionario = _servico.SelecionarPorId(id);
            if (funcionario == null) return NotFound();

            var model = _mapper.Map<ExcluirFuncionarioViewModel>(funcionario);
            return View(model);
        }

        [HttpPost, ActionName("Excluir")]
        public IActionResult ConfirmarExcluir(Guid id)
        {
            _servico.Excluir(id);
            return RedirectToAction("Listar");
        }
    }
}