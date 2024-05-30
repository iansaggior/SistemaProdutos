namespace SistemaProdutos.Models
{
    public class ProdutoModel
    {
        public int ProdutoId{ get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal Valor { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Peso { get; set; }
        public bool Inativo { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
