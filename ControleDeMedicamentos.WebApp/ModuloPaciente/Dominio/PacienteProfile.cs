using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloPacientes.Views;

namespace ControleDeMedicamentos.WebApp.ModuloPacientes.Dominio;

public class PacienteProfile : Profile
{
    public PacienteProfile()
    {
        // Define o mapeamento entre a Entidade e a ViewModel de Listagem
        CreateMap<Paciente, ListarPacienteViewModel>();
        CreateMap<Paciente, PacienteViewModel>().ReverseMap();
        CreateMap<Paciente, ExcluirPacienteViewModel>();
    }
}