using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaProdutos.Migrations
{
    /// <inheritdoc />
    public partial class testecriacaoview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
    
create VIEW View_Movimentacoes_add_Criar_teste AS
SELECT
    new.produtoId AS ProdutoId,
    new.quantidade AS QuantidadeNew,
    old.quantidade AS QuantidadeOld,
    ABS(new.quantidade - old.quantidade) AS DiferencaQuantidade,
	CASE  WHEN new.quantidade - old.quantidade > 0 
        THEN CONCAT('Adição de ', ABS(new.quantidade - old.quantidade),' unidade(s) do produto ', new.produtoId, '. Total atual no estoque: ', ABS(new.quantidade),  ' unidade(s)')
        ELSE CONCAT('Remoção de ', ABS(new.quantidade - old.quantidade),' unidade(s) do produto ', new.produtoId, '. Total atual no estoque: ', ABS(new.quantidade),  ' unidade(s)')
    END AS TextoMovimento,
    	CASE  WHEN new.quantidade - old.quantidade > 0 
        THEN 'ENTRADA'
        ELSE 'SAIDA'
    END AS TipoAlteracao,
    new.dataAlteracao AS DataAlteracao
FROM
    produtos_audit AS new
INNER JOIN
    produtos_audit AS old ON new.produtoId = old.produtoId AND new.dataAlteracao = old.dataAlteracao
WHERE
    new.typeAudit = 'NEW' AND old.typeAudit = 'OLD' AND 
        new.quantidade != old.quantidade

UNION ALL

SELECT
     addo.produtoId AS ProdutoId,
     addo.quantidade AS QuantidadeNew,
     0 AS QuantidadeOld,
     addo.quantidade AS DiferencaQuantidade,
     CONCAT('Criação do produto ', addo.produtoId, ' com ', addo.quantidade,' unidade(s)', '. Total atual no estoque: ', ABS(addo.quantidade), ' unidade(s)') AS TextoMovimento,
		'ENTRADA'AS TipoAlteracao,
     addo.dataAlteracao AS DataAlteracao
 FROM
     produtos_audit AS addo
 WHERE
     addo.typeAudit = 'ADD' AND addo.quantidade > 0
     ORDER BY DataAlteracao DESC;
    ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS View_Movimentacoes_add_Criar_teste;");
        }
    }
}
