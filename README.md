# Show me the code - Vortex QR Tokenizadora

Este projeto é referente ao backend do teste de André Odon Pereira para a empresa Vortex QR Tokenizadora.

## Sobre o desenvolvimento

Para o desenvolvimento deste projeto, foi utilizado .Net 6 com arquitetura hexagonal.

Para banco de dados, o projeto prevê a utilização de SQL Server (preferencialmente na versão 2019).

## Como rodar o aplicativo

O aplicativo já está preparado para rodar em diversos tipos de ambiente, IIS, docker, AWS Code Build.

Antes de colocar o aplicativo em funcionamento, deve-se verificar se o ambiente selecionado possui a variável de ambiente __*'ASPNETCORE_ENVIRONMENT'*__ setada, os valores suportados são: __'Development'__ e  __'Production'__.

Após isso, adicionar corretamente os dados de conexão com seu servidor de banco de dados no arquivo de configuração referente ao seu ambiente ('appsettings.Development.json' para ambiente de desenvolvimento, 'appsettings.Production.json' para ambiente de produção). Esses arquivos de configuração podem ser encontrados na raiz do projeto 'API'.

Após colocar o aplicativo para rodar, o mesmo irá criar a estrutura de banco de dados, após a criação desta estrutura, você deverá rodar no banco criado o script *'Inicial.sql'* dentro da pasta *'scripts'* na raíz do projeto, para que tenha dados a serem utilizados.