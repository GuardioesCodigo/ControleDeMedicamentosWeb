using System;
using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloEstoque.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos.Apresentacao;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.Apresentacao;

public class RequisicaoEntradaProfile : Profile
{
    public RequisicaoEntradaProfile()
    {
        CreateMap<OpcaoFuncionariosDto, OpcaoFuncionariosViewModel>();
        CreateMap<OpcaoMedicamentosDto, OpcaoMedicamentosViewModel>();

        CreateMap<ListarRequisicaoEntradaDto, ListarRequisicaoEntradaViewModel>();
    }
}
