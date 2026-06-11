using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloFornecedores.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloFornecedores.Apresentacao;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos.Aplicacao;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamentos.Apresentacao;

public class MedicamentosProfile : Profile
{
    public MedicamentosProfile()
    {
        CreateMap<OpcaoFornecedoresDto, OpcaoFornecedoresViewModel>();
        CreateMap<ListarMedicamentosDto ,ListarMedicamentosViewModel>();

        CreateMap<CadastrarMedicamentosViewModel, CadastrarMedicamentosDto>();
        CreateMap<EditarMedicamentosViewModel, EditarMedicamentosDto>();

        CreateMap<DetalhesMedicamentosDto, EditarMedicamentosViewModel>()
            .ForCtorParam("Fornecedores", opt => opt.MapFrom(_ => new List<OpcaoFornecedoresViewModel>()));
    }
}
