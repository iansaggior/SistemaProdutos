namespace SistemaProdutos.Models
{
    public class ProdutoModel
    {
        public int IdProduto { get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal Valor { get; set; }
        public decimal Peso { get; set; }
        public bool Status { get; set; }
    }
}
