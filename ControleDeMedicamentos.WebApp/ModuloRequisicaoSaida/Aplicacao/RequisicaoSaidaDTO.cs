using System;
using ControleDeMedicamentos.WebApp.ModuloFornecedores.Dominio;
using ControleDeMedicamentos.WebApp.ModuloPacientes.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloRequisicaoSaidae.Aplicacao;

public record CadastrarRequisicaoSaidaDto(
    DateTime Data,
    string PacienteId,
    List<ItemRequisicaoDto> Itens
);

public record ListarRequisicaoSaidaDto(
    Guid Id,
    DateTime Data,
    string PacienteNome,
    string ResumoMedicamentos,
    int QuantidadeTotal
);

public record ItemRequisicaoDto(
    Guid MedicamentoId, 
    int Quantidade
);

public record DetalheRequisicaoSaidaDto(
    Guid Id,
    DateTime Data,
    string MedicamentoNome,
    string PacienteNome
);

public record OpcaoPacientesDto(
    Guid Id,
    string Nome,
    string CartaoSus

);
