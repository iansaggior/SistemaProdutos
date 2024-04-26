API de produtos



para atualizar seu banco, após ter mudado o login no arquivo 'appsettings.json', ir na opção 'Controle do Gerenciador de Pacotes' e rodar o comando:

Update-Database -Context SistemaProdutosDBContext

****obs****
caso haja alguma alteração no projeto, e seja necessario criar uma nova Migration, o comando seria:

  Add-Migration "nome_daMigration" -Context SistemaProdutosDBContext

após a criação da nova migration, certifique-se de rodar o update para atualizar as tabelas
