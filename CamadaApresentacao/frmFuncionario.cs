using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CamadaNegocio;

namespace CamadaApresentacao
{
    public partial class frmFuncionario : Form
    {

        private bool eNovo = false;
        private bool eEditar = false;

        public frmFuncionario()
        {
            InitializeComponent();
            this.ttMensagem.SetToolTip(this.txtNome, "Insira o nome do Funcionário");
        }


        //Mostrar mensagem de confirmação
        private void MensagemOk(string mensagem)
        {
            MessageBox.Show(mensagem, "Sistema Comércio", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        //Mostrar mensagem de erro
        private void MensagemErro(string mensagem)
        {
            MessageBox.Show(mensagem, "Sistema Comércio", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }



        //Limpar Campos
        private void Limpar()
        {
            this.txtNome.Text = string.Empty;
            this.txtSobrenome.Text = string.Empty;
            this.txtEndereco.Text = string.Empty;
            this.txtTelefone.Text = string.Empty;
            this.txtNumeroDoc.Text = string.Empty;
            this.txtEmail.Text = string.Empty;
            this.txtUsuario.Text = string.Empty;
            this.txtSenha.Text = string.Empty;
            
        }


        //Habilitar os text box
        private void Habilitar(bool valor)
        {
            this.txtNome.ReadOnly = !valor;
            this.txtNumeroDoc.ReadOnly = !valor;
            this.txtEndereco.ReadOnly = !valor;
            this.txtTelefone.ReadOnly = !valor;
            this.txtSenha.ReadOnly = !valor;
            this.txtEmail.ReadOnly = !valor;
            this.cbAcesso.Enabled = valor;
            
            this.cbSexo.Enabled = valor;
            this.txtUsuario.ReadOnly = !valor;
            this.dtData.Enabled = valor;
            


        }


        //Habilitar os botoes
        private void botoes()
        {
            if (this.eNovo || this.eEditar)
            {
                this.Habilitar(true);
                this.btnNovo.Enabled = false;
                this.btnSalvar.Enabled = true;
                this.btnEditar.Enabled = false;
                this.btnCancelar.Enabled = true;

            }
            else
            {
                this.Habilitar(false);
                this.btnNovo.Enabled = true;
                this.btnSalvar.Enabled = false;
                this.btnEditar.Enabled = true;
                this.btnCancelar.Enabled = false;
            }

        }



        //Ocultar as Colunas do Grid
        private void ocultarColunas()
        {
            this.dataLista.Columns[0].Visible = false;
            this.dataLista.Columns[1].Visible = false;
        }


        //Mostrar no Data Grid
        private void Mostrar()
        {
            this.dataLista.DataSource = NFuncionario.Mostrar();
            this.ocultarColunas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataLista.Rows.Count);
        }



        //Buscar pelo Nome da Empresa
        private void BuscarNome()
        {
            this.dataLista.DataSource = NFuncionario.BuscarNome(this.txtBuscar.Text);

            this.ocultarColunas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataLista.Rows.Count);
        }


        //Buscar pelo Num Doc
        private void BuscarDocumento()
        {
            this.dataLista.DataSource = NFuncionario.BuscarDocumento(this.txtBuscar.Text);

            this.ocultarColunas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataLista.Rows.Count);
        }



        private void frmFuncionario_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.Mostrar();
            this.Habilitar(false);
            this.botoes();
            this.txtId.Enabled = false;
            this.cbBusca.SelectedIndex = 0;
            this.cbSexo.SelectedIndex = 0;
            this.cbBusca.SelectedIndex = 0;
            this.cbAcesso.SelectedIndex = 0;
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (cbBusca.Text.Equals("Nome"))
            {
                this.BuscarNome();
            }
            else if (cbBusca.Text.Equals("Documento"))
            {
                this.BuscarDocumento();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cbBusca.Text.Equals("Nome"))
            {
                this.BuscarNome();
            }
            else if (cbBusca.Text.Equals("Documento"))
            {
                this.BuscarDocumento();
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            this.eNovo = true;
            this.eEditar = false;
            this.botoes();
            this.Limpar();
            this.Habilitar(true);
            this.txtNome.Focus();
            this.txtId.Enabled = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                string resp = "";
                if (this.txtNome.Text == string.Empty) 
                {
                    MensagemErro("Preencha todos os campos");
                    errorIcone.SetError(txtNome, "Insira o nome da Empresa");

                }
                else
                {
                    if (this.eNovo)
                    {
                        resp = NFuncionario.Inserir(this.txtNome.Text.Trim().ToUpper(), this.txtSobrenome.Text, this.cbSexo.Text, this.dtData.Value, this.txtNumeroDoc.Text, this.txtEndereco.Text, this.txtTelefone.Text, this.txtEmail.Text, this.cbAcesso.Text, this.txtUsuario.Text, this.txtSenha.Text);
                    }
                    else
                    {
                        resp = NFuncionario.Editar(Convert.ToInt32(this.txtId.Text),
                            this.txtNome.Text.Trim().ToUpper(), this.txtSobrenome.Text, this.cbSexo.Text, this.dtData.Value, this.txtNumeroDoc.Text, this.txtEndereco.Text, this.txtTelefone.Text, this.txtEmail.Text, this.cbAcesso.Text, this.txtUsuario.Text, this.txtSenha.Text);
                    }

                    if (resp.Equals("OK"))
                    {
                        if (this.eNovo)
                        {
                            this.MensagemOk("Registro salvo com sucesso");
                        }
                        else
                        {
                            this.MensagemOk("Registro editado com sucesso");
                        }
                    }
                    else
                    {
                        this.MensagemErro(resp);
                    }

                    this.eNovo = false;
                    this.eEditar = false;
                    this.botoes();
                    this.Limpar();
                    this.Mostrar();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void dataLista_DoubleClick(object sender, EventArgs e)
        {
            this.txtId.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["idfuncionario"].Value);
            this.txtNome.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["nome"].Value);
            this.txtSobrenome.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["sobrenome"].Value);
            this.cbSexo.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["sexo"].Value);
            this.dtData.Value = Convert.ToDateTime(this.dataLista.CurrentRow.Cells["data_nasc"].Value);
            this.txtNumeroDoc.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["num_documento"].Value);
            this.txtEndereco.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["endereco"].Value);
            this.txtTelefone.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["telefone"].Value);
            this.txtEmail.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["email"].Value);
            this.cbAcesso.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["acesso"].Value);
            this.txtUsuario.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["usuario"].Value);
            this.txtSenha.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["senha"].Value);
            this.tabControl1.SelectedIndex = 1;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (this.txtId.Text.Equals(""))
            {
                this.MensagemErro("Selecione um registro para Editar");
            }
            else
            {
                this.eEditar = true;
                this.botoes();
                this.Habilitar(true);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.eNovo = false;
            this.eEditar = false;
            this.botoes();
            this.Habilitar(false);
            this.Limpar();
        }

        private void chkDeletar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDeletar.Checked)
            {
                this.dataLista.Columns[0].Visible = true;
            }
            else
            {
                this.dataLista.Columns[0].Visible = false;
            }
        }

        private void dataLista_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataLista.Columns["Deletar"].Index)
            {
                DataGridViewCheckBoxCell ChkDeletar = (DataGridViewCheckBoxCell)dataLista.Rows[e.RowIndex].Cells["Deletar"];
                ChkDeletar.Value = !Convert.ToBoolean(ChkDeletar.Value);
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcao;
                Opcao = MessageBox.Show("Realmente Deseja apagar os Registros", "Sistema Comércio", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcao == DialogResult.OK)
                {
                    string Codigo;
                    string Resp = "";

                    foreach (DataGridViewRow row in dataLista.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToString(row.Cells[1].Value);
                            Resp = NFuncionario.Excluir(Convert.ToInt32(Codigo));

                            if (Resp.Equals("OK"))
                            {
                                this.MensagemOk("Registro excluido com sucesso");

                            }
                            else
                            {
                                this.MensagemErro(Resp);
                            }
                        }
                    }
                    this.Mostrar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }

        }

        private void cbBusca_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtBuscar.Text = string.Empty;
        }
    }
}
