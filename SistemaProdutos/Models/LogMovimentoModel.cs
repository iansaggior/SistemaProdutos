namespace SistemaProdutos.Models
{
    public class LogMovimentoModel
    {
        public int MovId { get; set; }
        public string? TextoMovimento{ get; set; }
        public int? ProdutoId { get; set; }
        public virtual ProdutoModel? Produto { get; set; }


    }
}
