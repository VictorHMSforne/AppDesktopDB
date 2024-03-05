using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;//adicionei/Biblioteca de dados
using MySqlConnector;//adicionei

namespace AppDesktopDB
{
    public class Pessoa
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string idade { get; set; }
        public string cidade { get; set; }

        MySqlConnection con = new MySqlConnection("server=sql.freedb.tech;port=3306;database=freedb_Tds10_Senai;user id=freedb_abcdeTOP;password=BzPy2MPagcF*qe&;charset=utf8");

        public List<Pessoa> listapessoa()
        {
            List<Pessoa> li = new List<Pessoa>();
            string sql = "SELECT * FROM pessoa";
            con.Open();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataReader dr = cmd.ExecuteReader(); //lê a query sql, executa ela e cria um array
            while (dr.Read())
            {
                Pessoa p = new Pessoa();
                p.id = (int)dr["id"];
                p.nome = dr["nome"].ToString();
                p.idade = dr["idade"].ToString();
                p.cidade = dr["cidade"].ToString();
                li.Add(p);
            }
            dr.Close();
            con.Close();
            return li;
        }

        public void Inserir(string nome,string idade,string cidade)
        {
            string sql = "INSERT INTO pessoa(nome,idade,cidade) VALUES ('" + nome + "','" + idade + "','" + cidade + "')";
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void Atualizar(Pessoa pessoa)
        {
            string sql = "UPDATE pessoa SET nome='" + pessoa.nome + "',idade='" + pessoa.idade + "',cidade='" + pessoa.cidade + "'WHERE id='"+pessoa.id+"'";
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void Excluir(int id)
        {
            string sql = "DELETE FROM pessoa WHERE id='" + id + "'";
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        
        public void Localizar(int id)
        {
            string sql = "SELECT * FROM pessoa WHERE id='" + id + "'";
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                nome = dr["nome"].ToString();
                idade = dr["idade"].ToString();
                cidade = dr["cidade"].ToString();
            }
            dr.Close();
            con.Close();
        }

        public bool RegistroRepetido(string nome,string idade,string cidade)
        {
            string sql = "SELECT * FROM pessoa WHERE nome='" + nome + "' AND idade='" + idade + "' AND cidade='" + cidade + "'";
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                return (int)result > 0;//aqui pode colocar como true
            }
            con.Close();
            return false;
        }
    }
}
