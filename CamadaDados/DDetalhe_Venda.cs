using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CamadaDados
{
    public class DDetalhe_Venda
    {
        private int _IdDetalhe_Venda;
        private int _IdVenda;
        private int _IdDetalhe_Entrada;
        private int _Quantidade;
        private decimal _Preco_Venda;
        private decimal _Desconto;

        public int IdDetalhe_Venda
        {
            get
            {
                return _IdDetalhe_Venda;
            }

            set
            {
                _IdDetalhe_Venda = value;
            }
        }

        public int IdVenda
        {
            get
            {
                return _IdVenda;
            }

            set
            {
                _IdVenda = value;
            }
        }

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

        public int Quantidade
        {
            get
            {
                return _Quantidade;
            }

            set
            {
                _Quantidade = value;
            }
        }

        public decimal Preco_Venda
        {
            get
            {
                return _Preco_Venda;
            }

            set
            {
                _Preco_Venda = value;
            }
        }

        public decimal Desconto
        {
            get
            {
                return _Desconto;
            }

            set
            {
                _Desconto = value;
            }
        }

        public DDetalhe_Venda()
        {

        }

        public DDetalhe_Venda(int iddetalhe_venda, int idvenda, 
            int iddetalhe_entrada, int quantidade, decimal preco_venda, 
            decimal desconto)
        {

            this.IdDetalhe_Venda = iddetalhe_venda;
            this.IdVenda = idvenda;
            this.IdDetalhe_Entrada = iddetalhe_entrada;
            this.Quantidade = quantidade;
            this.Preco_Venda = preco_venda;
            this.Desconto = desconto;
           
        }


        //Método Inserir
        public string Inserir(DDetalhe_Venda Detalhe_Venda,
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
                SqlCmd.CommandText = "spinserir_detalhes_venda";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParId = new SqlParameter();
                ParId.ParameterName = "@iddetalhe_venda";
                ParId.SqlDbType = SqlDbType.Int;
                ParId.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(ParId);


                SqlParameter ParIDVenda = new SqlParameter();
                ParIDVenda.ParameterName = "@idvenda";
                ParIDVenda.SqlDbType = SqlDbType.Int;
                ParIDVenda.Value = Detalhe_Venda.IdVenda;
                SqlCmd.Parameters.Add(ParIDVenda);

                SqlParameter ParIdDetalheEntrada = new SqlParameter();
                ParIdDetalheEntrada.ParameterName = "@iddetalhe_entrada";
                ParIdDetalheEntrada.SqlDbType = SqlDbType.Int;
                ParIdDetalheEntrada.Value = Detalhe_Venda.IdDetalhe_Entrada;
                SqlCmd.Parameters.Add(ParIdDetalheEntrada);


                SqlParameter ParQuantidade = new SqlParameter();
                ParQuantidade.ParameterName = "@quantidade";
                ParQuantidade.SqlDbType = SqlDbType.Int;
                ParQuantidade.Value = Detalhe_Venda.Quantidade;
                SqlCmd.Parameters.Add(ParQuantidade);

                

                SqlParameter ParPrecoVenda = new SqlParameter();
                ParPrecoVenda.ParameterName = "@preco_venda";
                ParPrecoVenda.SqlDbType = SqlDbType.Money;
                ParPrecoVenda.Value = Detalhe_Venda.Preco_Venda;
                SqlCmd.Parameters.Add(ParPrecoVenda);



                SqlParameter ParDesconto = new SqlParameter();
                ParDesconto.ParameterName = "@desconto";
                ParDesconto.SqlDbType = SqlDbType.Money;
                ParDesconto.Value = Detalhe_Venda.Desconto;
                SqlCmd.Parameters.Add(ParDesconto);



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
