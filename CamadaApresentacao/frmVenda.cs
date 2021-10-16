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
    public partial class frmVenda : Form
    {

        public int idfuncionario;
        private static frmVenda _Instancia;
        private bool eNovo;
        private DataTable dtDetalhe;
        private decimal totalPago = 0;

        public static frmVenda GetInstancia()
        {
            if (_Instancia == null)
            {
                _Instancia = new frmVenda();
            }
            return _Instancia;
        }


        public frmVenda()
        {
            InitializeComponent();
            this.ttMensagem.SetToolTip(this.btnBuscarCliente, "Busque o Cliente");
            this.ttMensagem.SetToolTip(this.btnBuscarProduto, "Busque o Produto");
            this.txtIdCliente.Enabled = false;
            this.txtIdProduto.Enabled = false;
            this.txtCliente.Enabled = false;
            this.txtProduto.Enabled = false;
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
            this.txtIdCliente.Text = string.Empty;
            this.txtCliente.Text = string.Empty;
            this.txtSerie.Text = string.Empty;
            this.txtCorrelativo.Text = string.Empty;
            this.txtImposto.Text = string.Empty;
            
            this.lblTotalPagar.Text = "0,0";
            this.CriarTabela();

        }

        private void LimparDetalhes()
        {
            this.txtIdProduto.Text = string.Empty;
            this.txtProduto.Text = string.Empty;
            this.txtEstoqueAtual.Text = string.Empty;
            this.txtPrecoVenda.Text = string.Empty;
            this.txtPrecoCompra.Text = string.Empty;
            this.txtDesconto.Text = "0";
            this.txtquantidade.Text = string.Empty;
        }


        //Habilitar os text box
        private void Habilitar(bool valor)
        {
            this.dtData.Enabled = valor;
            this.dtVencimento.Enabled = valor;
           
            this.txtSerie.ReadOnly = !valor;
            this.txtCorrelativo.ReadOnly = !valor;
            this.txtImposto.ReadOnly = !valor;
            this.txtDesconto.ReadOnly = !valor;
            this.txtPrecoCompra.ReadOnly = !valor;
            this.txtPrecoVenda.ReadOnly = !valor;
            this.txtEstoqueAtual.ReadOnly = !valor;
            this.txtquantidade.ReadOnly = !valor;
            this.cbComprovante.Enabled = valor;

            this.btnAdd.Enabled = valor;
            this.btnRemover.Enabled = valor;
            this.btnBuscarCliente.Enabled = valor;
            this.btnBuscarProduto.Enabled = valor;
        }


        //Habilitar os botoes
        private void botoes()
        {
            if (this.eNovo)
            {
                this.Habilitar(true);
                this.btnNovo.Enabled = false;
                this.btnSalvar.Enabled = true;
                this.btnBuscar.Enabled = false;
                this.btnCancelar.Enabled = true;
                this.btnBuscarCliente.Enabled = true;
                this.btnBuscarProduto.Enabled = true;

            }
            else
            {
                this.Habilitar(false);
                this.btnNovo.Enabled = true;
                this.btnSalvar.Enabled = false;
                this.btnBuscar.Enabled = true;
                this.btnCancelar.Enabled = false;
                this.btnBuscarCliente.Enabled = false;
                this.btnBuscarProduto.Enabled = false;
            }

        }



        //Ocultar as Colunas do Grid
        private void ocultarColunas()
        {
            this.dataLista.Columns[0].Visible = false;
            this.dataLista.Columns[1].Visible = false;
            this.dataLista.Columns[8].Visible = false;
        }


        //Mostrar no Data Grid
        private void Mostrar()
        {
            this.dataLista.DataSource = NVenda.Mostrar();
            this.ocultarColunas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataLista.Rows.Count);
        }



        //Buscar por Datas
        private void BuscarData()
        {
            this.dataLista.DataSource = NVenda.BuscarData(this.dtInicial.Value.ToString("yyyy/MM/dd"), this.dtFinal.Value.ToString("yyyy/MM/dd"));

            this.ocultarColunas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataLista.Rows.Count);
        }


        //Mostrar Detalhe Entrada
        private void MostrarDetalheEntrada()
        {
            this.dataListaDetalhes.DataSource = NVenda.MostrarDetalhes(this.txtId.Text);

        }


        //BUSCAR O CLIENTE
        public void setCliente(string idcliente, string nome)
        {
            this.txtIdCliente.Text = idcliente;
            this.txtCliente.Text = nome;
        }

        //BUSCAR O PRODUTO
        public void setProduto(string iddetalhe_entrada, string nome,
            decimal preco_compra, decimal preco_venda, int estoque, 
            DateTime data_vencimento)
        {
            this.txtIdProduto.Text = iddetalhe_entrada;
            this.txtProduto.Text = nome;
            this.txtPrecoCompra.Text = Convert.ToString(preco_compra);
            this.txtPrecoVenda.Text = Convert.ToString(preco_venda);
            this.txtEstoqueAtual.Text = Convert.ToString(estoque);
            this.dtVencimento.Value = data_vencimento;

        }



        private void frmVenda_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.Mostrar();
            this.Habilitar(false);
            this.botoes();
            this.CriarTabela();
            this.cbComprovante.SelectedIndex = 0;
        }

        private void frmVenda_FormClosed(object sender, FormClosedEventArgs e)
        {
            _Instancia = null;
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            frmBuscarCliente frm = new frmBuscarCliente();
            frm.Show();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            this.eNovo = true;
            this.botoes();
            this.Limpar();
            this.Habilitar(true);
            this.txtSerie.Focus();
            this.LimparDetalhes();
            this.txtImposto.Text = "0";
            this.txtDesconto.Text = "0";
            this.txtquantidade.Text = "1";
        }

        private void btnBuscarProduto_Click(object sender, EventArgs e)
        {
            frmBuscarProdutoVenda frm = new frmBuscarProdutoVenda();
            frm.Show();
        }


        private void CriarTabela()
        {
            this.dtDetalhe = new DataTable("Detalhe");
            this.dtDetalhe.Columns.Add("iddetalhe_entrada", System.Type.GetType("System.Int32"));
            this.dtDetalhe.Columns.Add("produto", System.Type.GetType("System.String"));
            this.dtDetalhe.Columns.Add("quantidade", System.Type.GetType("System.Decimal"));
            this.dtDetalhe.Columns.Add("preco_venda", System.Type.GetType("System.Decimal"));
            this.dtDetalhe.Columns.Add("desconto", System.Type.GetType("System.Int32"));

            
            this.dtDetalhe.Columns.Add("subtotal", System.Type.GetType("System.Decimal"));
           // this.dtDetalhe.Columns.Add("imposto", System.Type.GetType("System.Decimal"));

            this.dataListaDetalhes.DataSource = this.dtDetalhe;

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.eNovo = false;

            this.botoes();
            this.Habilitar(false);
            this.Limpar();
            this.LimparDetalhes();
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
                Opcao = MessageBox.Show("Realmente Deseja deletar os Registros", "Sistema Comércio", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcao == DialogResult.OK)
                {
                    string Codigo;
                    string Resp = "";

                    foreach (DataGridViewRow row in dataLista.Rows)
                    {

                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {

                            Codigo = Convert.ToString(row.Cells[1].Value);
                            Resp = NVenda.Deletar(Convert.ToInt32(Codigo));

                            if (Resp.Equals("OK"))
                            {
                                this.MensagemOk("Registro deletado com sucesso");

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

        private void dataLista_DoubleClick(object sender, EventArgs e)
        {
            this.txtId.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["idvenda"].Value);
            this.txtCliente.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["cliente"].Value);
            this.dtData.Value = Convert.ToDateTime(this.dataLista.CurrentRow.Cells["data"].Value);
            this.cbComprovante.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["tipo_comprovante"].Value);
            this.txtSerie.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["serie"].Value);
            this.txtCorrelativo.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["correlativo"].Value);
            this.txtImposto.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["imposto"].Value);
            this.lblTotalPagar.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["total"].Value);

            this.MostrarDetalheEntrada();
            this.tabControl1.SelectedIndex = 1;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                string resp = "";
                if (this.txtIdCliente.Text == string.Empty || 
                    this.txtImposto.Text == string.Empty || 
                    this.txtSerie.Text == string.Empty)
                {
                    MensagemErro("Preencha todos os campos");
                    errorIcone.SetError(txtImposto, "Insira o Imposto");
                    errorIcone.SetError(txtIdCliente, "Insira o Fornecedor");
                    errorIcone.SetError(txtSerie, "Insira a série");
                    

                }
                else
                {


                    if (this.eNovo)
                    {
                        resp = NVenda.Inserir(Convert.ToInt32(this.txtIdCliente.Text), 
                            idfuncionario, dtData.Value, cbComprovante.Text,
                            txtSerie.Text, txtCorrelativo.Text,
                            Convert.ToDecimal(txtImposto.Text),
                            dtDetalhe);
                    }


                    if (resp.Equals("OK"))
                    {
                        if (this.eNovo)
                        {
                            this.MensagemOk("Registro salvo com sucesso");
                        }

                    }
                    else
                    {
                        this.MensagemErro(resp);
                    }

                    this.eNovo = false;

                    this.botoes();
                    this.Limpar();
                    this.Mostrar();
                    this.LimparDetalhes();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string resp = "";
                if (this.txtIdProduto.Text == string.Empty || this.txtEstoqueAtual.Text == string.Empty || this.txtPrecoCompra.Text == string.Empty || this.txtDesconto.Text == string.Empty)
                {
                    MensagemErro("Preencha todos os campos");
                    errorIcone.SetError(txtIdProduto, "Insira o Produto");
                    errorIcone.SetError(txtEstoqueAtual, "Insira o Estoque Inicial");
                    errorIcone.SetError(txtPrecoCompra, "Insira o preço de compra");
                    errorIcone.SetError(txtPrecoCompra, "Insira o valor do desconto");

                }
                else
                {
                    bool salvar = true;
                    foreach (DataRow row in dtDetalhe.Rows)
                    {
                        if (Convert.ToInt32(row["iddetalhe_entrada"]) == Convert.ToInt32(this.txtIdProduto.Text))
                        {
                            salvar = false;
                            this.MensagemErro("O Produto já está nos detalhes");
                        }

                    }
                    if (salvar && Convert.ToInt32(txtquantidade.Text) <= Convert.ToInt32(txtEstoqueAtual.Text))
                    {
                        //codigo para adicionar ao data list
                        decimal subTotal = Convert.ToDecimal(this.txtquantidade.Text) * Convert.ToDecimal(this.txtPrecoVenda.Text)-Convert.ToDecimal(this.txtDesconto.Text);
                        totalPago = totalPago + subTotal;
                        this.lblTotalPagar.Text = totalPago.ToString("R$ #0.00#");

                        DataRow row = this.dtDetalhe.NewRow();
                        row["iddetalhe_entrada"] = Convert.ToInt32(this.txtIdProduto.Text);
                        row["produto"] = this.txtProduto.Text;
                        row["quantidade"] = Convert.ToInt32(this.txtquantidade.Text);
                        row["preco_venda"] = Convert.ToDecimal(this.txtPrecoVenda.Text);
                        row["desconto"] = this.txtDesconto.Text;

                        row["subtotal"] = subTotal;

                        this.dtDetalhe.Rows.Add(row);
                        this.LimparDetalhes();
                    }
                    else
                    {
                        MensagemErro("Não há essa quantidade de produtos no estoque!");
                        this.txtquantidade.Text = string.Empty;
                        this.txtquantidade.Focus();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            try
            {
                int indiceLinha = this.dataListaDetalhes.CurrentCell.RowIndex;
                DataRow row = this.dtDetalhe.Rows[indiceLinha];

                //abater o valor pago
                this.totalPago = this.totalPago - Convert.ToDecimal(row["subtotal"].ToString());
                this.lblTotalPagar.Text = totalPago.ToString("R$ #0.00#");

                //remover a linha
                this.dtDetalhe.Rows.Remove(row);

            }
            catch (Exception ex)
            {
                MensagemErro("Não há linha para excluir!!");
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarData();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            frmRelatorioFatura frm = new frmRelatorioFatura();
            frm.Idvenda = Convert.ToInt32(this.dataLista.CurrentRow.Cells["idvenda"].Value);
            frm.ShowDialog();
        }

        
        public void OcultarTab()
        {
            this.tabControl1.Controls.Remove(tabPage2);
            this.btnDeletar.Visible = false;
            this.chkDeletar.Visible = false;
            this.btnImprimir.Visible = false;
            this.dataLista.Enabled = true;
        }

    }
}
