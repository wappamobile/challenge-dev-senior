﻿
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ENDERECO_MOTORISTA]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1) 
ALTER TABLE [ENDERECO] DROP CONSTRAINT [FK_ENDERECO_MOTORISTA]
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_MODELO_MARCA]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1) 
ALTER TABLE [MODELO] DROP CONSTRAINT [FK_MODELO_MARCA]
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CARRO_MODELO]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1) 
ALTER TABLE [CARRO] DROP CONSTRAINT [FK_CARRO_MODELO]
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CARRO_MOTORISTA]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1) 
ALTER TABLE [CARRO] DROP CONSTRAINT [FK_CARRO_MOTORISTA]
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[ENDERECO]') AND OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [ENDERECO]
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[MODELO]') AND OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [MODELO]
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[MARCA]') AND OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [MARCA]
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[CARRO]') AND OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [CARRO]
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[MOTORISTA]') AND OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [MOTORISTA]
;

CREATE TABLE [ENDERECO]
(
[id] int NOT NULL IDENTITY (1, 1),
[idMotorista] int NOT NULL,
[logradouro] varchar(200),
[numero] varchar(10),
[complemento] varchar(100),
[bairro] varchar(100),
[cidade] varchar(100),
[estado] varchar(2),
[cep] int,
[latitude] decimal(18,12),
[longitude] decimal(18,12)
)
;

CREATE TABLE [MODELO]
(
[id] int NOT NULL,
[descricao] varchar(50),
[idMarca] int NOT NULL
)
;

CREATE TABLE [MARCA]
(
[id] int NOT NULL,
[descricao] varchar(50)
)
;

CREATE TABLE [CARRO]
(
[id] int NOT NULL IDENTITY (1, 1),
[idMotorista] int NOT NULL,
[idModelo] int NOT NULL,
[placa] varchar(7)
)
;
CREATE TABLE [MOTORISTA]
(
[id] int NOT NULL IDENTITY (1, 1),
[nome] varchar(100),
[sobrenome] varchar(100)
)
;

CREATE INDEX [IXFK_ENDERECO_MOTORISTA] 
ON [ENDERECO] ([idMotorista] ASC)
;

ALTER TABLE [ENDERECO] 
ADD CONSTRAINT [PK_ENDERECO]
PRIMARY KEY CLUSTERED ([id])
;

CREATE INDEX [IXFK_MODELO_MARCA] 
ON [MODELO] ([idMarca] ASC)
;

ALTER TABLE [MODELO] 
ADD CONSTRAINT [PK_MODELO]
PRIMARY KEY CLUSTERED ([id])
;

ALTER TABLE [MARCA] 
ADD CONSTRAINT [PK_MARCA]
PRIMARY KEY CLUSTERED ([id])
;

CREATE INDEX [IXFK_CARRO_MODELO] 
ON [CARRO] ([idModelo] ASC)
;

CREATE INDEX [IXFK_CARRO_MOTORISTA] 
ON [CARRO] ([idMotorista] ASC)
;

ALTER TABLE [CARRO] 
ADD CONSTRAINT [PK_CARRO]
PRIMARY KEY CLUSTERED ([id])
;

ALTER TABLE [MOTORISTA] 
ADD CONSTRAINT [PK_MOTORISTA]
PRIMARY KEY CLUSTERED ([id])
;

ALTER TABLE [ENDERECO] ADD CONSTRAINT [FK_ENDERECO_MOTORISTA]
FOREIGN KEY ([idMotorista]) REFERENCES [MOTORISTA] ([id]) ON DELETE No Action ON UPDATE No Action
;

ALTER TABLE [MODELO] ADD CONSTRAINT [FK_MODELO_MARCA]
FOREIGN KEY ([idMarca]) REFERENCES [MARCA] ([id]) ON DELETE No Action ON UPDATE No Action
;

ALTER TABLE [CARRO] ADD CONSTRAINT [FK_CARRO_MODELO]
FOREIGN KEY ([idModelo]) REFERENCES [MODELO] ([id]) ON DELETE No Action ON UPDATE No Action
;

ALTER TABLE [CARRO] ADD CONSTRAINT [FK_CARRO_MOTORISTA]
FOREIGN KEY ([idMotorista]) REFERENCES [MOTORISTA] ([id]) ON DELETE No Action ON UPDATE No Action
;
