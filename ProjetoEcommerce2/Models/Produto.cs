namespace ProjetoEcommerce2.Models
{
    public class Produto
    {
        public int IdProd { get; set; }
        public string ?Nome { get; set; }
        public string ?Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }

    }
}
