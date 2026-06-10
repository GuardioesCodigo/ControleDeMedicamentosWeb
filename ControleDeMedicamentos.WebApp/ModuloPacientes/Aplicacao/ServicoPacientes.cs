using System;

namespace ControleDeMedicamentos.WebApp.ModuloPacientes.Aplicacao;

public class ServicoPaciente
{
    public bool CPFJaExiste(string cpf) => registros.Any(x => x.CPF == cpf);
    public bool CartaoSusJaExiste(string cartaoSus) => registros.Any(x => x.CartaoSus == cartaoSus);

    
    public override void Cadastrar(Paciente novoPaciente)
    {
        if (CartaoSusJaExiste(novoPaciente.CartaoSus))
            throw new Exception("Já existe um paciente com este cartão do SUS.");

        if (CPFJaExiste(novoPaciente.CPF))
            throw new Exception("Já existe um paciente com este CPF.");

        base.Inserir(novoPaciente);
    }

    public override void Editar(Paciente pacienteAtualizado)
    {
        // Na edição, precisamos ignorar o próprio paciente que está sendo editado
        if (registros.Any(p => p.CartaoSus == pacienteAtualizado.CartaoSus && p.Id != pacienteAtualizado.Id))
            throw new Exception("Já existe outro paciente com este cartão do SUS.");

        base.Editar(pacienteAtualizado);
    }

}
