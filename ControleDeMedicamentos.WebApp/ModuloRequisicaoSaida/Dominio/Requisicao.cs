using ControleDeMedicamentos.WebApp.Compartilhado.Dominio;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos.Dominio;
using ControleDeMedicamentos.WebApp.ModuloPacientes.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloRequisicao.Dominio;

public class Requisicao : EntidadeBase<Requisicao>
{
    public DateTime Data { get; set; }
    public Guid PacienteId { get; set; }
    public Paciente Paciente { get; set; }
    public List<ItemRequisicao> Medicamentos { get; set; } = new();

    public override List<string> Validar()
    {
        var erros = new List<string>();

        if (Data == default)
            erros.Add("A data da requisição é obrigatória.");

        if (PacienteId == Guid.Empty)
            erros.Add("O paciente é obrigatório.");

        if (Medicamentos == null || Medicamentos.Count == 0)
            erros.Add("Você deve selecionar pelo menos um medicamento.");
        else
        {
            // Validação extra: verificar se nenhum item tem quantidade zero ou negativa
            if (Medicamentos.Any(x => x.Quantidade <= 0))
                erros.Add("A quantidade de todos os medicamentos deve ser maior que zero.");
        }

        return erros;
    }

    public override void Atualizar(Requisicao entidadeAtualizada)
    {
       throw new NotImplementedException("Requisições de saída não podem ser editadas. Cancele e crie uma nova.");
}

public class ItemRequisicao
{
    public Guid MedicamentoId { get; set; }
    public Medicamentos Medicamento { get; set; }
    public int Quantidade { get; set; }
}
}