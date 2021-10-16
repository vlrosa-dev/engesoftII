using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CamadaDados
{
    public class DDetalhe_Entrada
    {
        private int _IdDetalhe_Entrada;
        private int _IdProduto;
        private int _IdEntrada;
        private decimal _PrecoCompra;
        private decimal _PrecoVenda;
        private int _EstoqueInicial;
        private int _EstoqueAtual;
        private DateTime _DataProducao;
        private DateTime _DataVencimento;
        private string _TextoBuscar;
        private string _TextoBuscar2;

        public int IdDetalhe_Entrada
        {
            get
            {
                return _IdDetalhe_Entrada;
            }

            set
            {
                _IdDetalhe_Entrada = value;
            }
        }

        public int IdProduto
        {
            get
            {
                return _IdProduto;
            }

            set
            {
                _IdProduto = value;
            }
        }

        public int IdEntrada
        {
            get
            {
                return _IdEntrada;
            }

            set
            {
                _IdEntrada = value;
            }
        }

        public decimal PrecoCompra
        {
            get
            {
                return _PrecoCompra;
            }

            set
            {
                _PrecoCompra = value;
            }
        }

        public decimal PrecoVenda
        {
            get
            {
                return _PrecoVenda;
            }

            set
            {
                _PrecoVenda = value;
            }
        }

        public int EstoqueInicial
        {
            get
            {
                return _EstoqueInicial;
            }

            set
            {
                _EstoqueInicial = value;
            }
        }

        public int EstoqueAtual
        {
            get
            {
                return _EstoqueAtual;
            }

            set
            {
                _EstoqueAtual = value;
            }
        }

        public DateTime DataProducao
        {
            get
            {
                return _DataProducao;
            }

            set
            {
                _DataProducao = value;
            }
        }

        public DateTime DataVencimento
        {
            get
            {
                return _DataVencimento;
            }

            set
            {
                _DataVencimento = value;
            }
        }

        public string TextoBuscar
        {
            get
            {
                return _TextoBuscar;
            }

            set
            {
                _TextoBuscar = value;
            }
        }

        public string TextoBuscar2
        {
            get
            {
                return _TextoBuscar2;
            }

            set
            {
                _TextoBuscar2 = value;
            }
        }

        public DDetalhe_Entrada()
        {

        }

        public DDetalhe_Entrada(int iddetalhe_entrada, int identrada, 
            int idproduto, decimal preco_compra, decimal preco_venda,
          int estoque_inicial, int estoque_atual, DateTime data_producao,
          DateTime data_vencimento, string textobuscar, 
          string textobuscar2)
        {
            this.IdDetalhe_Entrada = iddetalhe_entrada;
            this.IdEntrada = identrada;
            this.IdProduto = idproduto;
            this.PrecoCompra = preco_compra;
            this.PrecoVenda = preco_venda;
            this.EstoqueInicial = estoque_inicial;
            this.EstoqueAtual = estoque_atual;
            this.DataProducao = data_producao;
            this.DataVencimento = data_vencimento;
            this.TextoBuscar = textobuscar;
            this.TextoBuscar2 = textobuscar2;



        }

        //Método Inserir
        public string Inserir(DDetalhe_Entrada Detalhe_Entrada,
            ref SqlConnection SqlCon, ref SqlTransaction SqlTra)
        {
            string resp = "";
            
            try
            {
                //codigo
               // SqlCon.ConnectionString = Conexao.Cn;
              //  SqlCon.Open();

                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.Transaction = SqlTra;
                SqlCmd.CommandText = "spinserir_detalhe_entrada";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParId = new SqlParameter();
                ParId.ParameterName = "@iddetalhe_entrada";
                ParId.SqlDbType = SqlDbType.Int;
                ParId.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(ParId);


                SqlParameter ParIDEntrada = new SqlParameter();
                ParIDEntrada.ParameterName = "@identrada";
                ParIDEntrada.SqlDbType = SqlDbType.Int;                
                ParIDEntrada.Value = Detalhe_Entrada.IdEntrada;
                SqlCmd.Parameters.Add(ParIDEntrada);

                SqlParameter ParIdProduto = new SqlParameter();
                ParIdProduto.ParameterName = "@idproduto";
                ParIdProduto.SqlDbType = SqlDbType.Int;
                ParIdProduto.Value = Detalhe_Entrada.IdProduto;
                SqlCmd.Parameters.Add(ParIdProduto);

                SqlParameter ParPrecoCompra = new SqlParameter();
                ParPrecoCompra.ParameterName = "@preco_compra";
                ParPrecoCompra.SqlDbType = SqlDbType.Money;
                ParPrecoCompra.Size = 40;
                ParPrecoCompra.Value = Detalhe_Entrada.PrecoCompra;
                SqlCmd.Parameters.Add(ParPrecoCompra);

                SqlParameter ParPrecoVenda = new SqlParameter();
                ParPrecoVenda.ParameterName = "@preco_venda";
                ParPrecoVenda.SqlDbType = SqlDbType.Money;
                ParPrecoVenda.Size = 40;
                ParPrecoVenda.Value = Detalhe_Entrada.PrecoVenda;
                SqlCmd.Parameters.Add(ParPrecoVenda);

                SqlParameter ParEstoqueInicial = new SqlParameter();
                ParEstoqueInicial.ParameterName = "@estoque_inicial";
                ParEstoqueInicial.SqlDbType = SqlDbType.Int;
                ParEstoqueInicial.Value = Detalhe_Entrada.EstoqueInicial;
                SqlCmd.Parameters.Add(ParEstoqueInicial);

                SqlParameter ParEstoqueAtual = new SqlParameter();
                ParEstoqueAtual.ParameterName = "@estoque_atual";
                ParEstoqueAtual.SqlDbType = SqlDbType.Int;
                ParEstoqueAtual.Value = Detalhe_Entrada.EstoqueAtual;
                SqlCmd.Parameters.Add(ParEstoqueAtual);


                SqlParameter ParDataProducao = new SqlParameter();
                ParDataProducao.ParameterName = "@data_producao";
                ParDataProducao.SqlDbType = SqlDbType.Date;
                ParDataProducao.Value = Detalhe_Entrada.DataProducao;
                SqlCmd.Parameters.Add(ParDataProducao);

                SqlParameter ParDataVencimento = new SqlParameter();
                ParDataVencimento.ParameterName = "@data_vencimento";
                ParDataVencimento.SqlDbType = SqlDbType.Date;
                ParDataVencimento.Value = Detalhe_Entrada.DataVencimento;
                SqlCmd.Parameters.Add(ParDataVencimento);


                //Executar o comando

                resp = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "Registro não foi Inserido";


            }
            catch (Exception ex)
            {
                resp = ex.Message;
            }

            
            return resp;

        }





    }
}
