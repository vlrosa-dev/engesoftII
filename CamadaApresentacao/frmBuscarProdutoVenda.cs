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
    public partial class frmBuscarProdutoVenda : Form
    {
        public frmBuscarProdutoVenda()
        {
            InitializeComponent();
        }




        //Ocultar as Colunas do Grid
        private void ocultarColunas()
        {
            this.dataLista.Columns[0].Visible = false;
            this.dataLista.Columns[1].Visible = false;
           
        }


        //Mostrar no Data Grid produtos pelo nome
        private void MostrarProduto_Venda_Nome()
        {
            this.dataLista.DataSource = NVenda.MostrarProduto_Venda_Nome(this.txtBuscar.Text);
            this.ocultarColunas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataLista.Rows.Count);
        }

        //Mostrar no Data Grid produtos pelo código
        private void MostrarProduto_Venda_Codigo()
        {
            this.dataLista.DataSource = NVenda.MostrarProduto_Venda_Codigo(this.txtBuscar.Text);
            this.ocultarColunas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataLista.Rows.Count);
        }



       


        private void frmBuscarProdutoVenda_Load(object sender, EventArgs e)
        {
            this.MostrarProduto_Venda_Nome();
            cbBuscar.SelectedIndex = 0;
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (cbBuscar.Text.Equals("Nome"))
            {
                this.MostrarProduto_Venda_Nome();
            }
            else if (cbBuscar.Text.Equals("Codigo"))
            {
                this.MostrarProduto_Venda_Codigo();
            }
        }

        private void dataLista_DoubleClick(object sender, EventArgs e)
        {
            frmVenda form = frmVenda.GetInstancia();
            string par1, par2;
            decimal par3, par4;
            int par5;
            DateTime par6;

            par1 = Convert.ToString(this.dataLista.CurrentRow.Cells["iddetalhe_entrada"].Value);
            par2 = Convert.ToString(this.dataLista.CurrentRow.Cells["nome"].Value);
            par3 = Convert.ToDecimal(this.dataLista.CurrentRow.Cells["preco_compra"].Value);
            par4 = Convert.ToDecimal(this.dataLista.CurrentRow.Cells["preco_venda"].Value);
            par5 = Convert.ToInt32(this.dataLista.CurrentRow.Cells["estoque_atual"].Value);
            par6 = Convert.ToDateTime(this.dataLista.CurrentRow.Cells["data_vencimento"].Value);

            form.setProduto(par1, par2, par3, par4, par5, par6);
            this.Hide();
        }
    }
}
