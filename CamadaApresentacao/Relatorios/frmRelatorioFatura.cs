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
    public partial class frmRelatorioFatura : Form
    {

        private int _idvenda;


        public int Idvenda
        {
            get
            {
                return _idvenda;
            }

            set
            {
                _idvenda = value;
            }
        }


        public frmRelatorioFatura()
        {
            InitializeComponent();
        }

       

        private void frmRelatorioFatura_Load(object sender, EventArgs e)
        {
            // TODO: esta linha de código carrega dados na tabela 'dsPrincipal.sprelatorio_fatura'. Você pode movê-la ou removê-la conforme necessário.

            try { 
            this.sprelatorio_faturaTableAdapter.Fill(this.dsPrincipal.sprelatorio_fatura, Idvenda);

            this.reportViewer1.RefreshReport();
            }catch(Exception ex)
            {
                this.reportViewer1.RefreshReport();
            }
        }
    }
}
