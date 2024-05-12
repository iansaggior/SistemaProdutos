using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaProdutos.Models
{
    public class MovimentacaoModel
    {
        [Column("QuantidadeNew")]
        public decimal QuantidadeNew { get; set; }
        [Column("QuantidadeOld")]
        public decimal QuantidadeOld { get; set; }
        [Column("DiferencaQuantidade")]
        public int DiferencaQuantidade { get; set; }
        [Column("TextoMovimento")]
        public string TextoMovimento { get; set; }
        [Column("TipoAlteracao")]
        public string TipoAlteracao { get; set; }
        [Column("DataAlteracao")]
        public DateTime DataAlteracao { get; set; }
        [Column("ProdutoId")]
        public int? ProdutoId { get; set; }
        public virtual ProdutoModel? Produto { get; set; }

    }
}
