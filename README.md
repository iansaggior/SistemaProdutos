API de produtos



para atualizar seu banco, após ter mudado o login no arquivo 'appsettings.json', ir na opção 'Controle do Gerenciador de Pacotes' e rodar o comando:

Update-Database -Context SistemaProdutosDBContext

****obs****
caso haja alguma alteração no projeto, e seja necessario criar uma nova Migration, o comando seria:

  Add-Migration "nome_daMigration" -Context SistemaProdutosDBContext

após a criação da nova migration, certifique-se de rodar o update para atualizar as tabelas


CAMINHO MÉTODOS HTTPS
métodos get - movimentos:
- lista todos os movimentos realizados de todos os produtos: https://localhost:7214/api/Movimentos
- lista todos os movimentos realizados de um produto (id): https://localhost:7214/api/Movimentos/{id}
- lista todos os movimentos realizados por um tipo (add/ rem): https://localhost:7214/api/Movimentos/type={type}
- lista todos os movimentos realizados por um tipo (add/ rem) e por um produto (id): https://localhost:7214/api/Movimentos/id={id}/type={type}
- lista todos os movimentos realizados entre uma data e outra: https://localhost:7214/api/Movimentos/{dataInicio}/{dataFinal}
- lista todos os movimentos realizados entre uma data e outra, de um produto em especifico: https://localhost:7214/api/Movimentos/{dataInicio}/{dataFinal}/id={id}
- lista todos os movimentos realizados entre uma data e outra, de um produto em especifico, de um determinado tipo (add/ rem): https://localhost:7214/api/Movimentos/{dataInicio}/{dataFinal}/id={id}/type={type}
- lista todos os movimentos realizados entre uma data e outra, de um determinado tipo (add/ rem): https://localhost:7214/api/Movimentos/{dataInicio}/{dataFinal}/type={type}

métodos get - produto
- lista todos os produtos: https://localhost:7214/api/Produto
- lista um produto de acordo com o ID: https://localhost:7214/api/Produto/{id}
- lista produtos de acordo com um coringa na busca: https://localhost:7214/api/Produto/busca/{coringa}
- lista produtos de acordo com a quantidade armazenada de forma ascendente: https://localhost:7214/api/Produto/busca/qtdeAsc
- lista produtos de acordo com a quantidade armazenada de forma descendente: https://localhost:7214/api/Produto/busca/qtdeDesc
- lista os ultimos produtos cadastrados (Ordena DESC pela DataCadastro): https://localhost:7214/api/Produto/buscaPorDataCadastro

métodos post - produto
- cadastrar um novo produto: https://localhost:7214/api/Produto

métodos put - produto
- alterar o cadastro de um produto, atraves do seu ID: https://localhost:7214/api/Produto/{id}
- adicionar x unidade(s) de um produto, atraves do seu ID: https://localhost:7214/api/Produto/add/id={id}&qtde={qtde}
- remover x unidade(s) de um produto, atraves do seu ID: https://localhost:7214/api/Produto/rem/id={id}&qtde={qtde}

métodos delete - produto
- inativar o cadastro de um produto, atraves do seu ID: https://localhost:7214/api/Produto/rem/{id}


