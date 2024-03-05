using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppDesktopDB
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tu é Lindo","Easter Egg", MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            Pessoa pessoa = new Pessoa();
            List<Pessoa> pessoas =  pessoa.listapessoa();
            dgvPessoa.DataSource = pessoas;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                Pessoa pessoa = new Pessoa();
                if (pessoa.RegistroRepetido(txtNome.Text,txtIdade.Text,txtCidade.Text)==true)
                {
                    MessageBox.Show("Pessoa já existe em nossa base de dados!", "Registro Repetido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNome.Text = "";
                    txtIdade.Text = "";
                    txtCidade.Text = "";
                    this.txtNome.Focus();
                    return;//encerrar esse ciclo e voltando ao início
                }
                else
                {
                    pessoa.Inserir(txtNome.Text,txtIdade.Text,txtCidade.Text);
                    MessageBox.Show("Pessoa Inserida com Sucesso!","Inserção",MessageBoxButtons.OK, MessageBoxIcon.Information);
                    List<Pessoa> pessoas = pessoa.listapessoa();
                    dgvPessoa.DataSource = pessoas;
                    txtNome.Text = "";
                    txtIdade.Text = "";
                    txtCidade.Text = "";
                    this.txtNome.Focus();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
            try
            {
                int id = Convert.ToInt32(txtId.Text.Trim());
                Pessoa pessoa = new Pessoa();
                pessoa.Localizar(id);
                txtNome.Text = pessoa.nome;
                txtIdade.Text = pessoa.idade;
                txtCidade.Text = pessoa.cidade;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(txtId.Text.Trim());
                Pessoa pessoa = new Pessoa();
                pessoa.Atualizar(id, txtNome.Text, txtIdade.Text, txtCidade.Text);
                MessageBox.Show("Pessoa Atualizada com Sucesso!","Atualização",MessageBoxButtons.OK,MessageBoxIcon.Information);
                List<Pessoa> pessoas = pessoa.listapessoa();
                dgvPessoa.DataSource = pessoas;
                txtId.Text = "";
                txtNome.Text = "";
                txtIdade.Text = "";
                txtCidade.Text = "";
                this.txtNome.Focus();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(txtId.Text.Trim());
                Pessoa pessoa = new Pessoa();
                pessoa.Excluir(id);
                MessageBox.Show("Pessoa Excluída com Sucesso!", "Exclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<Pessoa> pessoas = pessoa.listapessoa();
                dgvPessoa.DataSource = pessoas;
                txtId.Text = "";
                txtNome.Text = "";
                txtIdade.Text = "";
                txtCidade.Text = "";
                this.txtNome.Focus();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPessoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>=0)
            {
                DataGridViewRow row = this.dgvPessoa.Rows[e.RowIndex];
                this.dgvPessoa.Rows[e.RowIndex].Selected = true;
                txtId.Text = row.Cells[0].Value.ToString();
                txtNome.Text = row.Cells[1].Value.ToString();
                txtIdade.Text = row.Cells[2].Value.ToString();
                txtCidade.Text = row.Cells[3].Value.ToString();
            }
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
        }
    }
}
