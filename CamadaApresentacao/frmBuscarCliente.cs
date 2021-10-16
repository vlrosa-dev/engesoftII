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
    public partial class frmBuscarCliente : Form
    {
        public frmBuscarCliente()
        {
            InitializeComponent();
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
            this.dataLista.DataSource = NCliente.Mostrar();
            this.ocultarColunas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataLista.Rows.Count);
        }



        //Buscar pelo Nome da Empresa
        private void BuscarNome()
        {
            this.dataLista.DataSource = NCliente.BuscarNome(this.txtBuscar.Text);

            this.ocultarColunas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataLista.Rows.Count);
        }


        //Buscar pelo Num Doc
        private void BuscarDocumento()
        {
            this.dataLista.DataSource = NCliente.BuscarDocumento(this.txtBuscar.Text);

            this.ocultarColunas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataLista.Rows.Count);
        }




        private void frmBuscarCliente_Load(object sender, EventArgs e)
        {
            
            this.Mostrar();
            this.cbBusca.SelectedIndex = 0;

        }

        private void cbBusca_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtBuscar.Text = string.Empty;
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

        private void dataLista_DoubleClick(object sender, EventArgs e)
        {
            frmVenda form = frmVenda.GetInstancia();
            string par1, par2;
            par1 = Convert.ToString(dataLista.CurrentRow.Cells["idcliente"].Value);
            par2 = Convert.ToString(dataLista.CurrentRow.Cells["nome"].Value);
            form.setCliente(par1, par2);
            this.Hide();
        }
    }
}
