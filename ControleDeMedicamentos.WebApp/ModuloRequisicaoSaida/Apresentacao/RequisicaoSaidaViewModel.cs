

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleDeMedicamentosWeb.WebApp.ModuloRequisicaoSaida.Apresentacao;

public class ListarRequisicaoSaidaViewModel
{
    public Guid Id { get; set; }
    public DateTime Data { get; set; }
    public string Paciente { get; set; } // Talvez o nome seja só "Paciente"?
    public string Itens { get; set; }
    public int QuantidadeTotal { get; set; }
}

public class CadastrarRequisicaoSaidaViewModel
{
    public DateTime Data { get; set; } = DateTime.Now;
    public string PacienteId { get; set; }
    
    // Lista de Itens que o usuário selecionou na tela
    public List<ItemRequisicaoViewModel> Itens { get; set; } = new();

    [ValidateNever]
    public IEnumerable<SelectListItem> Pacientes { get; set; }

    [ValidateNever]
    public IEnumerable<SelectListItem> Medicamentos { get; set; }
}

public class ItemRequisicaoViewModel
{
    public string MedicamentoId { get; set; }

    [ValidateNever]
    public string NomeMedicamento { get; set; }
    public int Quantidade { get; set; }
}
public record OpcaoPacientesViewModel(
    Guid Id,
    string Nome,
    string CartaoSus

);