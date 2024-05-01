using System.Globalization;

namespace SistemaProdutos.Models
{
    public class ProdutoAuditModel
    {
        public int AuditId { get; set; }
        public int? ProdutoId { get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal Valor { get; set; }
        public decimal Quantidade { get; set; } // pendente saber se irá trabalhar apenas com inteiros ou terá números com virgula
        public decimal Peso { get; set; }
        public bool Inativo { get; set; }
        public string TypeAudit { get; set; }
        public DateTime DataAlteracao { get; set; }
        public virtual ProdutoModel? Produto { get; set; }
        public ProdutoAuditModel()
        {
            
        }
        public ProdutoAuditModel(ProdutoModel produto, bool existe)
        {
            ProdutoId = produto.ProdutoId;
            Nome = produto.Nome;
            Descricao = produto.Descricao;
            Valor = produto.Valor;
            Quantidade = produto.Quantidade;
            Peso = produto.Peso;
            Inativo = produto.Inativo;
            if (existe)
            {
                TypeAudit = "OLD";
            } else
            {
                TypeAudit = "ADD";
            }
        }
    }
}
