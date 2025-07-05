namespace TechStoreApi.models
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
        public int estoque { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}