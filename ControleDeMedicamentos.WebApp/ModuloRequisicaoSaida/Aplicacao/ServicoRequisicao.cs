using System;
using System.Linq;
using ControleDeMedicamentos.WebApp.ModuloRequisicao.Dominio;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos.Dominio; 
using ControleDeMedicamentos.WebApp.ModuloMedicamentos.Aplicacao; // Verifique se a interface está aqui
using ControleDeMedicamentos.WebApp.Compartilhado.Dominio; // Verifique onde está IContextoPersistencia
using ControleDeMedicamentos.WebApp.ModuloRequisicaoSaida.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloRequisicaoSaida.Aplicacao;

public class ServicoRequisicao
{
    private readonly IRepositorioRequisicao _repositorioRequisicao;
    private readonly IRepositorioMedicamento _repositorioMedicamento;
    private readonly IContextoPersistencia _contexto; // Ajuste conforme o seu contexto

    public ServicoRequisicao(IRepositorioRequisicao repoReq, 
                             IRepositorioMedicamento repoMed, 
                             IContextoPersistencia contexto)
    {
        _repositorioRequisicao = repoReq;
        _repositorioMedicamento = repoMed;
        _contexto = contexto;
    }

    public void Cadastrar(Requisicao requisicao)
    {
        // 1. Validações da Requisição (Data, Paciente, etc.)
        var erros = requisicao.Validar();
        if (erros.Count > 0)
            throw new Exception(string.Join(" | ", erros));

        // 2. Processamento do estoque
        foreach (var item in requisicao.Medicamentos)
        {
            var medicamento = _repositorioMedicamento.SelecionarPorId(item.MedicamentoId);
            
            if (medicamento == null)
                throw new Exception("Medicamento não encontrado.");

            // Usa o método que criamos no domínio para subtrair e validar saldo
            medicamento.SubtrairQuantidade(item.Quantidade);
            
            _repositorioMedicamento.Editar(medicamento.Id, medicamento);
        }

        // 3. Persistência da requisição
        requisicao.Id = Guid.NewGuid();
        _repositorioRequisicao.Cadastrar(requisicao);
        _contexto.Salvar();
    }
}
