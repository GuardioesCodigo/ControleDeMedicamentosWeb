using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos.Aplicacao;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamentos.Apresentacao;

public class MedicamentosProfile : Profile
{
    public MedicamentosProfile()
    {
        CreateMap<ListarMedicamentosDto ,ListarMedicamentosViewModel>();
    }
}
