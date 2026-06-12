// using System;
// using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations;
// using Microsoft.AspNetCore.Mvc.Rendering;

// namespace ControleDeMedicamentos.WebApp.ModuloEstoque.RequisicoesSaida.Apresentacao;

// public class CadastrarRequisicaoSaidaViewModel
// {
//     [Required(ErrorMessage = "O campo Data é obrigatório.")]
//     public DateTime Data { get; set; } = DateTime.Today;

//     [Required(ErrorMessage = "O campo Medicamento é obrigatório.")]
//     public string MedicamentoId { get; set; } = string.Empty;

//     [Required(ErrorMessage = "O campo Paciente é obrigatório.")]
//     public string PacienteId { get; set; } = string.Empty;

//     [Required(ErrorMessage = "O campo Quantidade é obrigatório.")]
//     [Range(1, int.MaxValue, ErrorMessage = "O campo Quantidade deve ser um número positivo.")]
//     public int Quantidade { get; set; }

//     public List<SelectListItem> Medicamentos { get; set; } = new List<SelectListItem>();

//     public List<SelectListItem> Pacientes { get; set; } = new List<SelectListItem>();
// }

// public class ListarRequisicaoSaidaViewModel
// {
//     public string Id { get; set; } = string.Empty;
//     public DateTime Data { get; set; }
//     public string MedicamentoNome { get; set; } = string.Empty;
//     public string PacienteNome { get; set; } = string.Empty;
//     public int Quantidade { get; set; }
// }



// public class EditarRequisicaoSaidaViewModel
// {
//     [Required]
//     public string Id { get; set; } = string.Empty;

//     [Required(ErrorMessage = "O campo Data é obrigatório.")]
//     public DateTime Data { get; set; } = DateTime.Today;

//     [Required(ErrorMessage = "O campo Medicamento é obrigatório.")]
//     public string MedicamentoId { get; set; } = string.Empty;

//     [Required(ErrorMessage = "O campo Paciente é obrigatório.")]
//     public string PacienteId { get; set; } = string.Empty;

//     [Required(ErrorMessage = "O campo Quantidade é obrigatório.")]
//     [Range(1, int.MaxValue, ErrorMessage = "O campo Quantidade deve ser um número positivo.")]
//     public int Quantidade { get; set; }

//     public List<SelectListItem> Medicamentos { get; set; } = new List<SelectListItem>();

//     public List<SelectListItem> Pacientes { get; set; } = new List<SelectListItem>();
// }