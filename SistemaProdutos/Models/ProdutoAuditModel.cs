namespace SistemaProdutos.Models
{
    public class ProdutoAuditModel
    {
        public int AuditId { get; set; }
        public int? ProdutoId { get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal Valor { get; set; }
        public decimal Peso { get; set; }
        public bool Inativo { get; set; }
        public string TypeAudit { get; set; }
        public virtual ProdutoModel? Produto { get; set; }
        public ProdutoAuditModel()
        {
            
        }
        public ProdutoAuditModel(ProdutoModel produto, bool existe = false)
        {
            ProdutoId = produto.ProdutoId;
            Nome = produto.Nome;
            Descricao = produto.Descricao;
            Valor = produto.Valor;
            Peso = produto.Peso;
            Inativo = produto.Inativo;
            if (!existe)
            {
                TypeAudit = "ADD";
            } else
            {
                TypeAudit = "OLD";
            }
        }
    }
}
