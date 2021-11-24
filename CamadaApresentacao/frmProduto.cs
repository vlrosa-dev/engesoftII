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
    public partial class frmProduto : Form
    {
        private bool eNovo = false;
        private bool eEditar = false;
        private static frmProduto _Instancia;

        public static frmProduto GetInstancia()
        {
            if(_Instancia == null)
            {
                _Instancia = new frmProduto();
            }
            return _Instancia;
        }

        public void setCategoria(string idCategoria, string Nome)
        {
            this.txtIdCategoria.Text = idCategoria;
            this.txtCategoria.Text = Nome;
        }



        public frmProduto()
        {
            InitializeComponent();
            this.ttMensagem.SetToolTip(this.txtNome, "Insira o nome do Produto");
            this.ttMensagem.SetToolTip(this.pxImagem, "Insira uma imagem para o Produto");
            this.ttMensagem.SetToolTip(this.cbApresentacao, "Selecione a Apresentação do Produto");
            this.ttMensagem.SetToolTip(this.txtCategoria, "Selecione uma Categoria para o Produto");
            this.txtIdCategoria.Enabled = false;
            this.txtCategoria.Enabled = false;
            this.ComboApresentacao();
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
            this.txtId.Text = string.Empty;
            this.txtDescricao.Text = string.Empty;
            this.txtCodigo.Text = string.Empty;
            this.txtIdCategoria.Text = string.Empty;
            this.txtCategoria.Text = string.Empty;
            this.lblEstoqueMinimo.Text = string.Empty;
            this.pxImagem.Image = global::CamadaApresentacao.Properties.Resources.semImagem;
        }


        //Habilitar os text box
        private void Habilitar(bool valor)
        {
            this.txtNome.ReadOnly = !valor;
            this.txtDescricao.ReadOnly = !valor;
            this.txtId.ReadOnly = !valor;
            this.txtCodigo.ReadOnly = !valor;
            this.cbApresentacao.Enabled = valor;
            this.btnBuscar.Enabled = valor;
            this.btnLimpar.Enabled = valor;
            this.btnCarregar.Enabled = valor;
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
                this.btnBuscarCategoria.Enabled = true;

            }
            else
            {
                this.Habilitar(false);
                this.btnNovo.Enabled = true;
                this.btnSalvar.Enabled = false;
                this.btnEditar.Enabled = true;
                this.btnCancelar.Enabled = false;
                this.btnBuscarCategoria.Enabled = false;
            }

        }



        //Ocultar as Colunas do Grid
        private void ocultarColunas()
        {
            this.dataLista.Columns[0].Visible = false;
            this.dataLista.Columns[1].Visible = false;
            this.dataLista.Columns[5].Visible = false;
            this.dataLista.Columns[6].Visible = false;
            this.dataLista.Columns[8].Visible = false;
        }


        //Mostrar no Data Grid
        private void Mostrar()
        {
            this.dataLista.DataSource = NProduto.Mostrar();
            this.ocultarColunas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataLista.Rows.Count);
        }



        //Buscar pelo Nome
        private void BuscarNome()
        {
            this.dataLista.DataSource = NProduto.BuscarNome(this.txtBuscar.Text);

            this.ocultarColunas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataLista.Rows.Count);
        }

        private void ComboApresentacao()
        {
            cbApresentacao.DataSource = NApresentacao.Mostrar();
            cbApresentacao.ValueMember = "IdApresentacao";
            cbApresentacao.DisplayMember = "nome";
        }



        private void frmProduto_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.Mostrar();
            this.Habilitar(false);
            this.botoes();
        }

        private void btnCarregar_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult result = dialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                this.pxImagem.SizeMode = PictureBoxSizeMode.StretchImage;
                this.pxImagem.Image = Image.FromFile(dialog.FileName);

            }

        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            this.pxImagem.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pxImagem.Image = global::CamadaApresentacao.Properties.Resources.semImagem;
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            this.BuscarNome();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarNome();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            this.eNovo = true;
            this.eEditar = false;
            this.botoes();
            this.Limpar();
            this.Habilitar(true);
            this.txtNome.Focus();
            
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                string resp = "";
                if (this.txtNome.Text == string.Empty || this.txtIdCategoria.Text == string.Empty || this.txtCodigo.Text == string.Empty)
                {
                    MensagemErro("Preencha todos os campos");
                    errorIcone.SetError(txtNome, "Insira o nome");
                    errorIcone.SetError(txtIdCategoria, "Insira o nome");
                    errorIcone.SetError(txtCodigo, "Insira o nome");

                }
                else
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    this.pxImagem.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] imagem = ms.GetBuffer();

                    if (this.eNovo)
                    {
                        resp = NProduto.Inserir(this.txtCodigo.Text, this.txtNome.Text.Trim().ToUpper(), this.txtDescricao.Text.Trim(), imagem, Convert.ToInt32(this.txtIdCategoria.Text), Convert.ToInt32(this.cbApresentacao.SelectedValue), Convert.ToInt32(this.lblEstoqueMinimo.Text));
                    }
                    else
                    {
                        resp = NProduto.Editar(Convert.ToInt32(this.txtId.Text), this.txtCodigo.Text, this.txtNome.Text.Trim().ToUpper(), this.txtDescricao.Text.Trim(), imagem, Convert.ToInt32(this.txtIdCategoria.Text), Convert.ToInt32(this.cbApresentacao.SelectedValue), Convert.ToInt32(this.lblEstoqueMinimo.Text));
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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (this.txtId.Text.Equals(""))
            {
                this.MensagemErro("Selecione um registro para editar");
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

        private void dataLista_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.txtId.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["idproduto"].Value);
            this.txtCodigo.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["codigo"].Value);
            this.txtNome.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["nome"].Value);
            this.txtDescricao.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["descricao"].Value);

            
            byte[] imagenBuffer = (byte[])this.dataLista.CurrentRow.Cells["imagem"].Value;
            System.IO.MemoryStream ms = new System.IO.MemoryStream(imagenBuffer);
            this.pxImagem.Image = Image.FromStream(ms);
            this.pxImagem.SizeMode = PictureBoxSizeMode.StretchImage;

            this.txtIdCategoria.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["idcategoria"].Value);
            this.txtCategoria.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["Categoria"].Value);
            this.cbApresentacao.SelectedValue = Convert.ToString(this.dataLista.CurrentRow.Cells["idapresentacao"].Value);
            this.lblEstoqueMinimo.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["estoque_minimo"].Value);
           

            this.tabControl1.SelectedIndex = 1;
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
                            Resp = NProduto.Excluir(Convert.ToInt32(Codigo));

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

        private void btnBuscarCategoria_Click(object sender, EventArgs e)
        {
            frmBuscarCategoria form = new frmBuscarCategoria();
            form.ShowDialog();
        }

        private void frmProduto_FormClosed(object sender, FormClosedEventArgs e)
        {
            _Instancia = null;
        }

        private void cbApresentacao_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            frmRelatorioProduto frm = new frmRelatorioProduto();
            frm.ShowDialog();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
