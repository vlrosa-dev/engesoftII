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
    public partial class frmEntrada : Form
    {
        public int idfuncionario;
        private static frmEntrada _Instancia;
        private bool eNovo;
        private DataTable dtDetalhe;
        private decimal totalPago = 0;
        
        public static frmEntrada GetInstancia()
        {
            if (_Instancia == null)
            {
                _Instancia = new frmEntrada();
            }
            return _Instancia;
        }

        public frmEntrada()
        {
            InitializeComponent();
            this.ttMensagem.SetToolTip(this.btnBuscarFornecedor, "Busque o Fornecedor");
            this.ttMensagem.SetToolTip(this.btnBuscarProduto, "Busque o Produto");
            this.txtIdFornecedor.Enabled = false;
            this.txtIdProduto.Enabled = false;
            this.txtFornecedor.Enabled = false;
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
            this.txtIdFornecedor.Text = string.Empty;
            this.txtFornecedor.Text = string.Empty;
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
            this.txtEstoqueInicial.Text = string.Empty;
            this.txtPrecoVenda.Text = string.Empty;
            this.txtPrecoCompra.Text = string.Empty;
            
        }


        //Habilitar os text box
        private void Habilitar(bool valor)
        {
            this.dtData.Enabled = valor;
            this.dtProducao.Enabled = valor;
            this.dtVencimento.Enabled = valor;
            this.txtSerie.ReadOnly = !valor;
            this.txtCorrelativo.ReadOnly = !valor;
            this.txtImposto.ReadOnly = !valor;
            this.txtPrecoCompra.ReadOnly = !valor;
            this.txtPrecoVenda.ReadOnly = !valor;
            this.txtEstoqueInicial.ReadOnly = !valor;
            this.cbComprovante.Enabled = valor;
            
            this.btnAdd.Enabled = valor;
            this.btnRemover.Enabled = valor;
            this.btnBuscarFornecedor.Enabled = valor;
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
                this.btnBuscarFornecedor.Enabled = true;
                this.btnBuscarProduto.Enabled = true;

            }
            else
            {
                this.Habilitar(false);
                this.btnNovo.Enabled = true;
                this.btnSalvar.Enabled = false;
                this.btnBuscar.Enabled = true;
                this.btnCancelar.Enabled = false;
                this.btnBuscarFornecedor.Enabled = false;
                this.btnBuscarProduto.Enabled = false;
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
            this.dataLista.DataSource = NEntrada.Mostrar();
            this.ocultarColunas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataLista.Rows.Count);
        }



        //Buscar por Datas
        private void BuscarData()
        {
            this.dataLista.DataSource = NEntrada.BuscarData(this.dtInicial.Value.ToString("yyyy/MM/dd"), this.dtFinal.Value.ToString("yyyy/MM/dd"));

            this.ocultarColunas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataLista.Rows.Count);
        }


        //Mostrar Detalhe Entrada
        private void MostrarDetalheEntrada()
        {
            this.dataListaDetalhes.DataSource = NEntrada.MostrarDetalhes(this.txtId.Text);
            
        }



        private void frmEntrada_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.Mostrar();
            this.Habilitar(false);
            this.botoes();
            this.CriarTabela();
            this.cbComprovante.SelectedIndex = 0;
        }

        private void frmEntrada_FormClosed(object sender, FormClosedEventArgs e)
        {
            _Instancia = null;
        }

        public void setFornecedor(string idfornecedor, string nome)
        {
            this.txtIdFornecedor.Text = idfornecedor;
            this.txtFornecedor.Text = nome;
        }

        public void setProduto(string idproduto, string nome)
        {
            this.txtIdProduto.Text = idproduto;
            this.txtProduto.Text = nome;
        }

        private void bnBuscarProduto_Click(object sender, EventArgs e)
        {
            frmVerProdutoEntrada frm = new frmVerProdutoEntrada();
            frm.Show();
        }

        private void btnBuscarFornecedor_Click(object sender, EventArgs e)
        {
            frmVerFornecedorEntrada frm = new frmVerFornecedorEntrada();
            frm.Show();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarData();
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcao;
                Opcao = MessageBox.Show("Realmente Deseja anular os Registros", "Sistema Comércio", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcao == DialogResult.OK)
                {
                    string Codigo;
                    string Resp = "";

                    foreach (DataGridViewRow row in dataLista.Rows)
                    {
                       
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            
                            Codigo = Convert.ToString(row.Cells[1].Value);
                            Resp = NEntrada.Anular(Convert.ToInt32(Codigo));

                            if (Resp.Equals("OK"))
                            {
                                this.MensagemOk("Registro anulado com sucesso");

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


        private void CriarTabela()
        {
            this.dtDetalhe = new DataTable("Detalhe");
            this.dtDetalhe.Columns.Add("idproduto", System.Type.GetType("System.Int32"));
            this.dtDetalhe.Columns.Add("produto", System.Type.GetType("System.String"));
            this.dtDetalhe.Columns.Add("preco_compra", System.Type.GetType("System.Decimal"));
            this.dtDetalhe.Columns.Add("preco_venda", System.Type.GetType("System.Decimal"));
            this.dtDetalhe.Columns.Add("estoque_inicial", System.Type.GetType("System.Int32"));
           
            this.dtDetalhe.Columns.Add("data_producao", System.Type.GetType("System.DateTime"));
            this.dtDetalhe.Columns.Add("data_vencimento", System.Type.GetType("System.DateTime"));
            this.dtDetalhe.Columns.Add("subtotal", System.Type.GetType("System.Decimal"));
            this.dtDetalhe.Columns.Add("imposto", System.Type.GetType("System.Decimal"));

            this.dataListaDetalhes.DataSource = this.dtDetalhe;

        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            this.eNovo = true;
            this.botoes();
            this.Limpar();
            this.Habilitar(true);
            this.txtSerie.Focus();
            this.LimparDetalhes();
            this.totalPago = 0;  
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.eNovo = false;
           
            this.botoes();
            this.Habilitar(false);
            this.Limpar();
            this.LimparDetalhes();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                string resp = "";
                if (this.txtIdFornecedor.Text == string.Empty || this.txtImposto.Text == string.Empty || this.txtSerie.Text == string.Empty)
                {
                    MensagemErro("Preencha todos os campos");
                    errorIcone.SetError(txtImposto, "Insira o Imposto");
                    errorIcone.SetError(txtIdFornecedor, "Insira o Fornecedor");
                    errorIcone.SetError(txtSerie, "Insira a série");

                }
                else
                {
                    

                    if (this.eNovo)
                    {
                        resp = NEntrada.Inserir(idfuncionario, Convert.ToInt32(this.txtIdFornecedor.Text),
                            dtData.Value, cbComprovante.Text,
                            txtSerie.Text, txtCorrelativo.Text, 
                            Convert.ToDecimal(txtImposto.Text), "EMITIDO", 
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
                if (this.txtIdProduto.Text == string.Empty || this.txtEstoqueInicial.Text == string.Empty || this.txtPrecoCompra.Text == string.Empty)
                {
                    MensagemErro("Preencha todos os campos");
                    errorIcone.SetError(txtIdProduto, "Insira o Produto");
                    errorIcone.SetError(txtEstoqueInicial, "Insira o Estoque Inicial");
                    errorIcone.SetError(txtPrecoCompra, "Insira o preço de compra");

                }
                else
                {
                    bool salvar = true;
                    foreach(DataRow row in dtDetalhe.Rows)
                    {
                        if (Convert.ToInt32(row["idproduto"]) == Convert.ToInt32(this.txtIdProduto.Text))
                        {
                            salvar = false;
                            this.MensagemErro("O Produto já está nos detalhes");
                        }

                    }
                    if (salvar)
                    {
                        //codigo para adicionar ao data list
                        decimal subTotal = Convert.ToDecimal(this.txtEstoqueInicial.Text) * Convert.ToDecimal(this.txtPrecoCompra.Text);
                        totalPago = totalPago + subTotal;
                        this.lblTotalPagar.Text = totalPago.ToString("#0.00#");

                        DataRow row = this.dtDetalhe.NewRow();
                        row["idproduto"] = Convert.ToInt32(this.txtIdProduto.Text);
                        row["produto"] = this.txtProduto.Text;
                        row["preco_compra"] = Convert.ToDecimal(this.txtPrecoCompra.Text);
                        row["preco_venda"] = Convert.ToDecimal(this.txtPrecoVenda.Text);
                        row["estoque_inicial"] = this.txtEstoqueInicial.Text;
                        row["data_producao"] = this.dtProducao.Value;
                        row["data_vencimento"] = this.dtVencimento.Value;
                        row["subtotal"] = subTotal;

                        this.dtDetalhe.Rows.Add(row);
                        this.LimparDetalhes();
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
                this.lblTotalPagar.Text = totalPago.ToString("#0.00#");

                //remover a linha
                this.dtDetalhe.Rows.Remove(row);

            }catch(Exception ex)
            {
                MensagemErro("Não há linha para excluir!!");
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

        private void dataLista_DoubleClick(object sender, EventArgs e)
        {
            this.txtId.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["identrada"].Value);
            this.txtFornecedor.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["fornecedor"].Value);
            this.dtData.Value = Convert.ToDateTime(this.dataLista.CurrentRow.Cells["data"].Value);
            this.cbComprovante.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["tipo_comprovante"].Value);
            this.txtSerie.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["serie"].Value);
            this.txtCorrelativo.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["correlativo"].Value);
            this.txtImposto.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["imposto"].Value);
            this.lblTotalPagar.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["total"].Value);

            this.MostrarDetalheEntrada();
            this.tabControl1.SelectedIndex = 1;
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
