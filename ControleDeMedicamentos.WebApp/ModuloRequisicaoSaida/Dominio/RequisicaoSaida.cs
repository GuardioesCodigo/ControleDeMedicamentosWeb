using ControleDeMedicamentos.WebApp.Compartilhado.Dominio;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos.Dominio;
using ControleDeMedicamentos.WebApp.ModuloPacientes.Dominio;
namespace ControleDeMedicamentos.WebApp.ModuloRequisicaoSaida.Dominio;

public class RequisicaoSaida : EntidadeBase<RequisicaoSaida>
{
    public DateTime Data { get; set; }
    public Guid PacienteId { get; set; }
    public Guid MedicamentoId { get; set; }
    public Paciente Paciente { get; set; }
    public Medicamentos Medicamentos { get; set; }
    public List<ItemRequisicaoSaida> Itens { get; set; } = new();

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();
        if (Data == default) erros.Add("A data é obrigatória.");
        if (PacienteId == Guid.Empty) erros.Add("Selecione um paciente.");
        if (!Itens.Any()) erros.Add("Adicione pelo menos um item à requisição.");
        return erros;
    }

    public override void Atualizar(RequisicaoSaida entidade)
    {
        this.Data = entidade.Data;
        this.PacienteId = entidade.PacienteId;
        this.Itens = entidade.Itens;
    }
}

public class ItemRequisicaoSaida
{
    public Guid MedicamentoId { get; set; }
    public Medicamentos Medicamento { get; set; }
    public int Quantidade { get; set; }
}