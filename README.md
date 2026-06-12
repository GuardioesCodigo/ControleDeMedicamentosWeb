# Sistema de Controle de Medicamentos Web

O Sistema de Controle de Medicamentos é uma aplicação desenvolvida para auxiliar na gestão completa de uma unidade de saúde, permitindo o controle de fornecedores, pacientes, medicamentos, funcionários e movimentações de estoque. O sistema garante organização, integridade dos dados e automação de processos essenciais como entradas e saídas de medicamentos.

O projeto foi desenvolvido com foco em boas práticas de arquitetura, validação de regras de negócio e facilidade de uso.

Desenvolvido por **Iago** e **Dayuã** durante o curso Fullstack da [Academia do Programador](https://www.academiadoprogramador.net) 2026.

---

## 1. Módulo de Fornecedores

O módulo de fornecedores permite o gerenciamento dos parceiros responsáveis pelo fornecimento de medicamentos e insumos do sistema.

### Funcionalidades

* Cadastro de novos fornecedores
* Visualização de todos os fornecedores cadastrados
* Edição de fornecedores existentes
* Exclusão de fornecedores cadastrados

---

## 2. Módulo de Pacientes

O módulo de pacientes é responsável pelo cadastro e manutenção dos dados dos pacientes atendidos no sistema.

### Funcionalidades

* Cadastro de novos pacientes
* Visualização de todos os pacientes cadastrados
* Edição de pacientes existentes
* Exclusão de pacientes cadastrados

---

## 3. Módulo de Medicamentos

O módulo de medicamentos centraliza o controle de todos os itens disponíveis no estoque da unidade.

### Funcionalidades

* Cadastro de novos medicamentos
* Visualização de todos os medicamentos cadastrados
* Edição de medicamentos existentes
* Exclusão de medicamentos cadastrados
* Destaque de medicamentos com menos de 20 unidades ("em falta")
* Atualização automática de estoque ao cadastrar medicamentos já existentes

---

## 4. Módulo de Funcionários

O módulo de funcionários gerencia os colaboradores responsáveis pelas operações do sistema.

### Funcionalidades

* Cadastro de novos funcionários
* Visualização de todos os funcionários cadastrados
* Edição de funcionários existentes
* Exclusão de funcionários cadastrados

---

## 5. Módulo de Estoque

O módulo de estoque controla as movimentações de entrada e saída de medicamentos, garantindo atualização automática das quantidades disponíveis.

### 5.1 Requisições de Entrada

Este submódulo registra a entrada de medicamentos no estoque.

#### Funcionalidades

* Registro de novas requisições de entrada
* Visualização de todas as requisições de entrada

---

### 5.2 Requisições de Saída

Este submódulo registra a saída de medicamentos para pacientes.

#### Funcionalidades

* Registro de novas requisições de saída
* Visualização de todas as requisições de saída

---

## Sistema

O sistema oferece operações completas de cadastro, edição, visualização e exclusão em todos os módulos, garantindo consistência dos dados e automação no controle de estoque.

---

## ▶️ Como utilizar

1. Clone o repositório ou baixe o código fonte.
2. Abra o terminal e navegue até a pasta raiz do projeto.
3. Restaure as dependências:

```bash

dotnet run --project ControleDeMedicamentos.WebApp



##  Requisitos

* .NET 10.0 SDK
* Visual Studio 2022 ou VS Code