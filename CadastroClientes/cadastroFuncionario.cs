using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CadastroClientes
{
    public partial class cadastroFuncionario : Form
    {
        public cadastroFuncionario()
        {
            InitializeComponent();
        }

        SqlConnection sqlCon = null;
        private string strCon = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=db_cadastro_clientes;Data Source=DESKTOP-BDMJAFC";
        private string strSql = string.Empty;

        private void Limpar()
        {
            txtNome.Clear();
            txtBairro.Clear();
            mtbCpf.Clear();
            txtEmail.Clear();
            txtEndereco.Clear();
            txtNumero.Clear();
            mtbRg.Clear();
            mtbCelular.Clear();
            mtbTelefone.Clear();
        }

        private void Adicionar()
        {
            strSql = "insert into tblFuncionarios(nome, telefone, celular, email,endereco,numero,bairro,rg,cpf) values(@nome,@telefone,@celular,@email,@endereco,@numero,@bairro,@rg,@cpf)";

            sqlCon = new SqlConnection(strCon);
            SqlCommand comando = new SqlCommand(strSql, sqlCon);

            comando.Parameters.Add("@nome", SqlDbType.VarChar).Value = txtNome.Text;
            comando.Parameters.Add("@telefone", SqlDbType.VarChar).Value = mtbTelefone.Text;
            comando.Parameters.Add("@celular", SqlDbType.VarChar).Value = mtbCelular.Text;
            comando.Parameters.Add("@email", SqlDbType.VarChar).Value = txtEmail.Text;
            comando.Parameters.Add("@endereco", SqlDbType.VarChar).Value = txtEndereco.Text;
            comando.Parameters.Add("@numero", SqlDbType.VarChar).Value = txtNumero.Text;
            comando.Parameters.Add("@bairro", SqlDbType.VarChar).Value = txtBairro.Text;
            comando.Parameters.Add("@rg", SqlDbType.VarChar).Value = mtbRg.Text;
            comando.Parameters.Add("@cpf", SqlDbType.VarChar).Value = mtbCpf.Text;

            if (txtNome.Text == string.Empty || mtbTelefone.Text == string.Empty || mtbCelular.Text == string.Empty || txtEmail.Text == string.Empty || txtEndereco.Text == string.Empty || txtNumero.Text == string.Empty || txtBairro.Text == string.Empty || mtbRg.Text == string.Empty || mtbCpf.Text == string.Empty)
            {
                MessageBox.Show("PREENCHA TODOS OS CAMPOS", "CAMPOS OBRIGATÓRIOS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNome.Focus();
            }

            else
            {
                try
                {
                    sqlCon.Open();
                    comando.ExecuteNonQuery();

                    MessageBox.Show("CADASTRO EFETUADO COM SUCESSO", "MENSAGEM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                finally
                {
                    sqlCon.Close();
                    Limpar();
                    txtNome.Focus();
                }
            }
        }

        private void Pesquisar()
        {
            strSql = "select * from tblFuncionarios where nome=@pesquisa";

            sqlCon = new SqlConnection(strCon);
            SqlCommand comando = new SqlCommand(strSql, sqlCon);

            comando.Parameters.Add("@pesquisa", SqlDbType.VarChar).Value = txtPesquisar.Text;

            try
            {
                if (txtPesquisar.Text == string.Empty)
                {
                    MessageBox.Show("DIGITE O NOME DESEJADO", "CAMPO VAZIO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Limpar();
                }

                sqlCon.Open();
                SqlDataReader dr = comando.ExecuteReader();

                if (dr.HasRows == false)
                {
                    throw new Exception("FUNCIONÁRIO NÃO CADASTRADO!");
                }

                dr.Read();

                txtNome.Text = Convert.ToString(dr["nome"]);
                mtbTelefone.Text = Convert.ToString(dr["telefone"]);
                mtbCelular.Text = Convert.ToString(dr["celular"]);
                txtEmail.Text = Convert.ToString(dr["email"]);
                txtEndereco.Text = Convert.ToString(dr["endereco"]);
                txtNumero.Text = Convert.ToString(dr["numero"]);
                txtBairro.Text = Convert.ToString(dr["bairro"]);
                mtbRg.Text = Convert.ToString(dr["rg"]);
                mtbCpf.Text = Convert.ToString(dr["cpf"]);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                sqlCon.Close();
                txtPesquisar.Clear();
            } 
        }

        private void Alterar()
        {
            strSql = "update tblFuncionarios set nome=@nome,telefone=@telefone,celular=@celular,email=@email,endereco=@endereco,numero=@numero,bairro=@bairro,rg=@rg, cpf=@cpf where nome=@nome";

            sqlCon = new SqlConnection(strCon);
            SqlCommand comando = new SqlCommand(strSql, sqlCon);

            comando.Parameters.Add("@nome", SqlDbType.VarChar).Value = txtNome.Text;
            comando.Parameters.Add("@telefone", SqlDbType.VarChar).Value = mtbTelefone.Text;
            comando.Parameters.Add("@celular", SqlDbType.VarChar).Value = mtbCelular.Text;
            comando.Parameters.Add("@email", SqlDbType.VarChar).Value = txtEmail.Text;
            comando.Parameters.Add("@endereco", SqlDbType.VarChar).Value = txtEndereco.Text;
            comando.Parameters.Add("@numero", SqlDbType.VarChar).Value = txtNumero.Text;
            comando.Parameters.Add("@bairro", SqlDbType.VarChar).Value = txtBairro.Text;
            comando.Parameters.Add("@rg", SqlDbType.VarChar).Value = mtbRg.Text;
            comando.Parameters.Add("@cpf", SqlDbType.VarChar).Value = mtbCpf.Text;

            if (txtNome.Text == string.Empty || mtbTelefone.Text == string.Empty || mtbCelular.Text == string.Empty || txtEmail.Text == string.Empty || txtEndereco.Text == string.Empty || txtNumero.Text == string.Empty || txtBairro.Text == string.Empty || mtbRg.Text == string.Empty || mtbCpf.Text == string.Empty)
            {
                MessageBox.Show("PREENCHA TODOS OS CAMPOS", "CAMPOS OBRIGATÓRIOS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPesquisar.Focus();
            }

            else
            {
                try
                {
                    if (MessageBox.Show("REALMENTE DESEJA ALTERAR OS DADOS", "MENSAGEM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        sqlCon.Open();
                        comando.ExecuteNonQuery();
                        MessageBox.Show("DADOS ALTERADOS COM SUCESSO", "MENSAGEM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                finally
                {
                    sqlCon.Close();
                    Limpar();
                    txtPesquisar.Focus();
                }
            }  
        }

        private void Deletar()
        {
            strSql = "delete from tblFuncionarios where nome=@nome";

            sqlCon = new SqlConnection(strCon);
            SqlCommand comando = new SqlCommand(strSql, sqlCon);

            comando.Parameters.Add("@nome", SqlDbType.VarChar).Value = txtNome.Text;

            if (txtNome.Text == string.Empty || mtbTelefone.Text == string.Empty || mtbCelular.Text == string.Empty || txtEmail.Text == string.Empty || txtEndereco.Text == string.Empty || txtNumero.Text == string.Empty || txtBairro.Text == string.Empty || mtbRg.Text == string.Empty || mtbCpf.Text == string.Empty)
            {
                MessageBox.Show("SELECIONE O CADASTRO A SER EXCLUIDO", "CAMPOS VAZIOS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPesquisar.Focus();
            }

            else
            {
                try
                {
                    if (MessageBox.Show("REALMENTE DESEJA EXCLUIR O FUNCIONÁRIO","MENSAGEM",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        sqlCon.Open();
                        comando.ExecuteNonQuery();
                        MessageBox.Show("FUNCIONÁRIO EXCLUIDO COM SUCESSO!");
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                finally
                {
                    sqlCon.Close();
                    Limpar();
                    txtPesquisar.Focus();
                }
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Adicionar();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            Pesquisar();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            Alterar();
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            Deletar();
        }
    }
}
