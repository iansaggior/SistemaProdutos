using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaProdutos.Migrations
{
    /// <inheritdoc />
    public partial class view_movimentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW View_Movimentacoes AS
                SELECT 
                    `new`.`AuditId` AS `AuditId`,
                    `new`.`ProdutoId` AS `ProdutoId`,
                    `new`.`Quantidade` AS `QuantidadeNew`,
                    `old`.`Quantidade` AS `QuantidadeOld`,
                    ABS((`new`.`Quantidade` - `old`.`Quantidade`)) AS `DiferencaQuantidade`,
                    (CASE
                        WHEN
                            ((`new`.`Quantidade` - `old`.`Quantidade`) > 0)
                        THEN
                            CONCAT('Adição de ',
                                    ABS((new.Quantidade - old.Quantidade)),
                                    ' unidade(s) do produto ',
                                    new.ProdutoId,
                                    '. Total atual no estoque: ',
                                    ABS(new.Quantidade),
                                    ' unidade(s)')
                        ELSE CONCAT('Remoção de ',
                                ABS((new.Quantidade - old.Quantidade)),
                                ' unidade(s) do produto ',
                                new.ProdutoId,
                                '. Total atual no estoque: ',
                                ABS(new.Quantidade),
                                ' unidade(s)')
                    END) AS TextoMovimento,
                    (CASE
                        WHEN ((new.Quantidade - old.Quantidade) > 0) THEN 'ENTRADA'
                        ELSE 'SAIDA'
                    END) AS TipoAlteracao,
                    new.DataAlteracao AS DataAlteracao
                    FROM
                    (db_sistemaprodutos.produtos_audit new
                    JOIN db_sistemaprodutos.produtos_audit old ON (((new.ProdutoId = old.ProdutoId)
                        AND (new.DataAlteracao = old.DataAlteracao))))
                WHERE
                    ((new.TypeAudit = 'NEW')
                        AND (old.TypeAudit = 'OLD')
                        AND (new.Quantidade <> old.Quantidade)) 
                UNION ALL SELECT 
                    addo.AuditId AS AuditId,
                    addo.ProdutoId AS ProdutoId,
                    addo.Quantidade AS QuantidadeNew,
                    0 AS QuantidadeOld,
                    addo.Quantidade AS DiferencaQuantidade,
                    CONCAT('Criação do produto ',
                            addo.ProdutoId,
                            ' com ',
                            addo.Quantidade,
                            ' unidade(s)',
                            '. Total atual no estoque: ',
                            ABS(addo.Quantidade),
                            ' unidade(s)') AS TextoMovimento,
                    'ENTRADA' AS TipoAlteracao,
                    addo.DataAlteracao AS DataAlteracao
                FROM
                    db_sistemaprodutos.produtos_audit addo
                WHERE
                    ((addo.TypeAudit = 'ADD')
                        AND (addo.Quantidade > 0))
                ORDER BY AuditId DESC;
    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW View_Movimentacoes;");
        }



    }

}
