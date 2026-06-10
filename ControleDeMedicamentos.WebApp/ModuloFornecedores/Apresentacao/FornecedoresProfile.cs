using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloFornecedores.Aplicacao;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedores.Apresentacao;

public class FornecedoresProfile : Profile
{
    public FornecedoresProfile()
    {
        CreateMap<ListarFornecedoresDto ,ListarFornecedoresViewModel>();
        CreateMap<CadastrarFornecedoresViewModel, CadastrarFornecedoresDto>();
        CreateMap<EditarFornecedoresViewModel, EditarFornecedoresDto>();

        CreateMap<DetalhesFornecedoresDto, EditarFornecedoresViewModel>();
        CreateMap<DetalhesFornecedoresDto, ExcluirFornecedoresViewModel>();
    }
}
