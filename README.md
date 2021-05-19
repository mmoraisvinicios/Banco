## Projeto .net Core 3.1 com Angular

### Configuração Banco de Dados SQL Server 

1. Crie o banco de dados com o nome ContaBancaria
2. Execute o comando de criação da tabela abaixo:
    
    USE [ContaBancaria]
    GO
    SET ANSI_NULLS ON
    GO

    SET QUOTED_IDENTIFIER ON
    GO

    CREATE TABLE [dbo].[Conta](
      [Id] [int] IDENTITY(1,1) NOT NULL,
      [NumeroConta] [varchar](50) NOT NULL,
      [NumeroAgencia] [varchar](50) NOT NULL,
      [CodigoBanco] [varchar](50) NOT NULL,
      [Documento] [varchar](20) NOT NULL,
      [Nome] [varchar](200) NOT NULL,
      [DataAbertura] [datetime] NOT NULL,
      [Situacao] [bit] NOT NULL,
     CONSTRAINT [PK_Conta] PRIMARY KEY CLUSTERED 
    (
      [Id] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO


### Executando o projeto
Baixe e execute o Projeto Front-end, que está no repositório
https://github.com/mmoraisvinicios/ProjectTestMega

Baixe e execute o Projeto Back-end, que está neste repositório.
https://github.com/mmoraisvinicios/Banco

OBS:
Caso o seu projeto front-end não execute na porta padrão 4200, 
<p>é necessário alterar o endereço no projeto back-end, no arquivo Startup.
    No trecho: spa.UseProxyToSpaDevelopmentServer($"INFORME_SEU_ENDERECO_AQUI"); </p>

<p>Para fazer a conexão com o banco de dados, é necessario alterar para os parametros do seu banco. </p>
<p>Arquivo para fazer a configuração appsettings.json
  "ConnectionStrings": {
    "ContextContaBancaria": "Server=localhost\\SQLEXPRESS01;Database=ContaBancaria;Trusted_Connection=True;MultipleActiveResultSets=true"
  },</p>

