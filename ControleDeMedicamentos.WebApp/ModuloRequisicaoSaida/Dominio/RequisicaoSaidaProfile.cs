using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloRequisicaoSaidae.Aplicacao;
using ControleDeMedicamentosWeb.WebApp.ModuloRequisicaoSaida.Apresentacao;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.RequisicoesSaida.Apresentacao;

public class RequisicaoSaidaProfile : Profile
{
    public RequisicaoSaidaProfile()
    {
     
        CreateMap<CadastrarRequisicaoSaidaViewModel, CadastrarRequisicaoSaidaDto>();
        CreateMap<ItemRequisicaoViewModel, ItemRequisicaoDto>();
        CreateMap<ListarRequisicaoSaidaDto, ListarRequisicaoSaidaViewModel>();

    }
}