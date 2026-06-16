CREATE TABLE [dbo].[TBFornecedor] (
[Id] uniqueidentifier NOT NULL IDENTITY(1,1),
[Nome] nvarchar(100) NOT NULL,
[Telefone] nvarchar(18) NOT NULL,
[Cnpj] nvarchar(18) NOT NULL,
PRIMARY KEY ([Id])
);

CREATE TABLE [dbo].[TBPaciente] (
[Id] uniqueidentifier NOT NULL IDENTITY(1,1),
[Nome] nvarchar(100) NOT NULL,
[Telefone] nvarchar(15) NOT NULL,
[Cpf] nvarchar(14) NOT NULL,
[CartaoSus] nvarchar(15) NOT NULL,
PRIMARY KEY ([Id])
);

CREATE TABLE [dbo].[TBFuncionario] (
[Id] uniqueidentifier NOT NULL IDENTITY(1,1),
[Nome] nvarchar(100) NOT NULL,
[Telefone] nvarchar(15) NOT NULL,
[Cpf] nvarchar(14) NOT NULL,
PRIMARY KEY ([Id])
);

CREATE TABLE [dbo].[TBMedicamentos] (
[Id] uniqueidentifier NOT NULL IDENTITY(1,1),
[Nome] nvarchar(100) NOT NULL,
[Descricao] nvarchar(255) NOT NULL,
[FornecedorId] uniqueidentifier NOT NULL,
PRIMARY KEY ([Id])
);


ALTER TABLE [dbo].[TBMedicamentos]
ADD CONSTRAINT [FK_TBMedicamentos_TBFornecedor]
FOREIGN KEY ([FornecedorId]) 
REFERENCES [dbo].[TBFornecedor]([Id])
ON DELETE NO ACTION
ON UPDATE NO ACTION;



