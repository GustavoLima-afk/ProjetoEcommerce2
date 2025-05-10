using MySql.Data.MySqlClient;
using ProjetoEcommerce2.Models;
using System.Data;

namespace ProjetoEcommerce2.Repositorio
{
    public class ProdutoRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");

        public void Cadastrar(Produto produto)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into produto (Nome,Descricao,Quantidade,Preco) values (@nome, @descricao, @quantidade, @preco)", conexao); 
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = produto.Nome;
                cmd.Parameters.Add("@descricao", MySqlDbType.VarChar).Value = produto.Descricao;
                cmd.Parameters.Add("@quantidade", MySqlDbType.Int32).Value = produto.Quantidade;
                cmd.Parameters.Add("@preco", MySqlDbType.Decimal).Value = produto.Preco;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public bool Atualizar(Produto produto)
        {
            try
            {
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    conexao.Open();
                    MySqlCommand cmd = new MySqlCommand("Update produto set Nome=@nome, Descricao=@descricao, Quantidade=@quantidade, Preco=@preco " + " where IdProd=@codigo ", conexao);
                    cmd.Parameters.Add("@codigo", MySqlDbType.Int32).Value = produto.IdProd;
                    cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = produto.Nome;
                    cmd.Parameters.Add("@Descricao", MySqlDbType.VarChar).Value = produto.Descricao;
                    cmd.Parameters.Add("@quantidade", MySqlDbType.Int32).Value = produto.Quantidade;
                    cmd.Parameters.Add("@preco", MySqlDbType.Decimal).Value = produto.Preco;
                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    return linhasAfetadas > 0;

                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro ao atualizar produto: {ex.Message}");
                return false; 

            }
        }
        public IEnumerable<Produto> TodosProdutos()
        {
            List<Produto> Produtolist = new List<Produto>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * from produto", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    Produtolist.Add(
                                new Produto
                                {
                                    IdProd = Convert.ToInt32(dr["IdProd"]),
                                    Nome = ((string)dr["Nome"]),
                                    Descricao = ((string)dr["Descricao"]),
                                    Quantidade = Convert.ToInt32(dr["Quantidade"]),
                                    Preco = ((decimal)dr["Preco"])
                                });
                }
                return Produtolist;
            }

        }
        public Produto ObterProduto(int Codigo)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * from produto where IdProd=@codigo ", conexao);

                cmd.Parameters.AddWithValue("@codigo", Codigo);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                MySqlDataReader dr;
                Produto produto = new Produto();


                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    produto.IdProd = Convert.ToInt32(dr["IdProd"]);
                    produto.Nome = (string)(dr["Nome"]);
                    produto.Quantidade = Convert.ToInt32(dr["Quantidade"]);
                    produto.Descricao = (string)(dr["Descricao"]);
                    produto.Preco = (decimal)(dr["Preco"]);
                }
                return produto;
            }
        }

        public void Excluir(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("delete from produto where IdProd=@codigo", conexao);

                cmd.Parameters.AddWithValue("@codigo", Id);

                int i = cmd.ExecuteNonQuery();

                conexao.Close();
            }
        }

    }
}