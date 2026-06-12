using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Dominio;
using ControleDeMedicamentos.WebApp.ModuloFuncionarios.Apresentacao;

namespace ControleDeMedicamentos.WebApp.ModuloFuncionarios.Dominio;

public class FuncionarioProfile : Profile
{
    public FuncionarioProfile()
    {
        // Se você não tiver uma ViewModel de Listagem específica, 
        // use a FuncionarioViewModel para tudo
        CreateMap<Funcionario, FuncionarioViewModel>().ReverseMap();
        CreateMap<Funcionario, ExcluirFuncionarioViewModel>();
        
        // Se você tiver uma classe separada para Listar, mantenha assim:
        // CreateMap<Funcionario, ListarFuncionarioViewModel>();
    }
}