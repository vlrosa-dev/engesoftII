namespace CamadaApresentacao
{
    partial class frmRelatorioFatura
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.sprelatorio_faturaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsPrincipal = new CamadaApresentacao.dsPrincipal();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.sprelatorio_faturaTableAdapter = new CamadaApresentacao.dsPrincipalTableAdapters.sprelatorio_faturaTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.sprelatorio_faturaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPrincipal)).BeginInit();
            this.SuspendLayout();
            // 
            // sprelatorio_faturaBindingSource
            // 
            this.sprelatorio_faturaBindingSource.DataMember = "sprelatorio_fatura";
            this.sprelatorio_faturaBindingSource.DataSource = this.dsPrincipal;
            // 
            // dsPrincipal
            // 
            this.dsPrincipal.DataSetName = "dsPrincipal";
            this.dsPrincipal.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.sprelatorio_faturaBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "CamadaApresentacao.Relatorios.rptComprovanteVenda.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(884, 445);
            this.reportViewer1.TabIndex = 0;
            // 
            // sprelatorio_faturaTableAdapter
            // 
            this.sprelatorio_faturaTableAdapter.ClearBeforeFill = true;
            // 
            // frmRelatorioFatura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 445);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmRelatorioFatura";
            this.Text = "Comprovante de Venda";
            this.Load += new System.EventHandler(this.frmRelatorioFatura_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sprelatorio_faturaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPrincipal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource sprelatorio_faturaBindingSource;
        private dsPrincipal dsPrincipal;
        private dsPrincipalTableAdapters.sprelatorio_faturaTableAdapter sprelatorio_faturaTableAdapter;
    }
}