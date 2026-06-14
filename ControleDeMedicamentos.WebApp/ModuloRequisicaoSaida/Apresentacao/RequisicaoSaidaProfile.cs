using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloRequisicaoSaidae.Aplicacao;
using ControleDeMedicamentosWeb.WebApp.ModuloRequisicaoSaida.Apresentacao;
using ControleDeMedicamentos.WebApp.ModuloEstoque.RequisicoesSaida.Dominio; // Ajuste para o namespace correto da sua Entidade/Dto

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.RequisicoesSaida.Dominio;

public class RequisicaoSaidaProfile : Profile
{
   public RequisicaoSaidaProfile()
    {
        // Cadastro (Mantido)
        CreateMap<CadastrarRequisicaoSaidaViewModel, CadastrarRequisicaoSaidaDto>();
        CreateMap<ItemRequisicaoViewModel, ItemRequisicaoDto>();

        // Listagem (Ajustado para o seu record)
        CreateMap<ListarRequisicaoSaidaDto, ListarRequisicaoSaidaViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Paciente, opt => opt.MapFrom(src => src.PacienteNome))
            .ForMember(dest => dest.Itens, opt => opt.MapFrom(src => src.ResumoMedicamentos))
            .ForMember(dest => dest.QuantidadeTotal, opt => opt.MapFrom(src => src.QuantidadeTotal));
    }
}