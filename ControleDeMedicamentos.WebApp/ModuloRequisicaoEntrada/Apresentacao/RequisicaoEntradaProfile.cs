using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloRequisicaoEntrada.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloRequisicaoEntrada.Apresentacao;


namespace ControleDeMedicamentos.WebApp.ModuloRequisicaoEntradae.Apresentacao;

public class RequisicaoEntradaProfile : Profile
{
    public RequisicaoEntradaProfile()
    {
        CreateMap<CadastrarRequisicaoEntradaDto, CadastrarRequisicaoEntradaViewModel>();
        CreateMap<CadastrarRequisicaoEntradaViewModel, CadastrarRequisicaoEntradaDto>();

        CreateMap<ListarRequisicaoEntradaDto, ListarRequisicaoEntradaViewModel>();

        CreateMap<OpcaoFuncionariosDto, OpcaoFuncionariosViewModel>();
        CreateMap<OpcaoMedicamentosDto, OpcaoMedicamentosViewModel>();
    }
}
