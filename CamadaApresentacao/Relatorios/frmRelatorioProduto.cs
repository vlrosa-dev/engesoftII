using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CamadaApresentacao
{
    public partial class frmRelatorioProduto : Form
    {
        public frmRelatorioProduto()
        {
            InitializeComponent();
        }

        private void frmRelatorioProduto_Load(object sender, EventArgs e)
        {
            // TODO: esta linha de código carrega dados na tabela 'dsPrincipal.spmostrar_produto'. Você pode movê-la ou removê-la conforme necessário.
            this.spmostrar_produtoTableAdapter.Fill(this.dsPrincipal.spmostrar_produto);

            this.reportViewer1.RefreshReport();
        }
    }
}
