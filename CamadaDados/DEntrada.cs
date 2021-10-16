using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CamadaDados
{
    public class DEntrada
    {
        private int _IdEntrada;
        private int _IdFuncionario;
        private int _IdFornecedor;
        private DateTime _Data;
        private string _TipoComprovante;
        private string _Serie;
        private string _Correlativo;
        private decimal _Imposto;
        private string _Estado;
        

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

        public int IdFuncionario
        {
            get
            {
                return _IdFuncionario;
            }

            set
            {
                _IdFuncionario = value;
            }
        }

        public int IdFornecedor
        {
            get
            {
                return _IdFornecedor;
            }

            set
            {
                _IdFornecedor = value;
            }
        }

        public DateTime Data
        {
            get
            {
                return _Data;
            }

            set
            {
                _Data = value;
            }
        }

        public string TipoComprovante
        {
            get
            {
                return _TipoComprovante;
            }

            set
            {
                _TipoComprovante = value;
            }
        }

        public string Serie
        {
            get
            {
                return _Serie;
            }

            set
            {
                _Serie = value;
            }
        }

        public string Correlativo
        {
            get
            {
                return _Correlativo;
            }

            set
            {
                _Correlativo = value;
            }
        }

       

        public string Estado
        {
            get
            {
                return _Estado;
            }

            set
            {
                _Estado = value;
            }
        }

       
        public decimal Imposto
        {
            get
            {
                return _Imposto;
            }

            set
            {
                _Imposto = value;
            }
        }

        public DEntrada()
        {

        }


        public DEntrada(int identrada, int idfuncionario, int idfornecedor,
            DateTime data, string tipo_comprovante, string serie,
            string correlativo, decimal imposto, string estado)
        {
            this.IdEntrada = identrada;
            this.IdFuncionario = idfuncionario;
            this.IdFornecedor = idfornecedor;
            this.Data = data;
            this.TipoComprovante = tipo_comprovante;
            this.Serie = serie;
            this.Correlativo = correlativo;
            this.Imposto = imposto;
            this.Estado = estado;


            
        }


        //Método Inserir
        public string Inserir(DEntrada Entrada, List<DDetalhe_Entrada> Detalhe)
        {
            string resp = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                //codigo
                SqlCon.ConnectionString = Conexao.Cn;
                SqlCon.Open();

                SqlTransaction SqlTra = SqlCon.BeginTransaction();


                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.Transaction = SqlTra;
                SqlCmd.CommandText = "spinserir_entrada";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParId = new SqlParameter();
                ParId.ParameterName = "@identrada";
                ParId.SqlDbType = SqlDbType.Int;
                ParId.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(ParId);


                SqlParameter ParIdFuncionario = new SqlParameter();
                ParIdFuncionario.ParameterName = "@idfuncionario";
                ParIdFuncionario.SqlDbType = SqlDbType.Int;
                ParIdFuncionario.Value = Entrada.IdFuncionario;
                SqlCmd.Parameters.Add(ParIdFuncionario);

                SqlParameter ParIdFornecedor = new SqlParameter();
                ParIdFornecedor.ParameterName = "@idfornecedor";
                ParIdFornecedor.SqlDbType = SqlDbType.Int;
                ParIdFornecedor.Value = Entrada.IdFornecedor;
                SqlCmd.Parameters.Add(ParIdFornecedor);


                SqlParameter ParData = new SqlParameter();
                ParData.ParameterName = "@data";
                ParData.SqlDbType = SqlDbType.Date;
                ParData.Value = Entrada.Data;
                SqlCmd.Parameters.Add(ParData);

                SqlParameter ParTipoComprovante = new SqlParameter();
                ParTipoComprovante.ParameterName = "@tipo_comprovante";
                ParTipoComprovante.SqlDbType = SqlDbType.VarChar;
                ParTipoComprovante.Size = 20;
                ParTipoComprovante.Value = Entrada.TipoComprovante;
                SqlCmd.Parameters.Add(ParTipoComprovante);

                SqlParameter ParSerie = new SqlParameter();
                ParSerie.ParameterName = "@serie";
                ParSerie.SqlDbType = SqlDbType.VarChar;
                ParSerie.Size = 4;
                ParSerie.Value = Entrada.Serie;
                SqlCmd.Parameters.Add(ParSerie);


                SqlParameter ParCorrelativo = new SqlParameter();
                ParCorrelativo.ParameterName = "@correlativo";
                ParCorrelativo.SqlDbType = SqlDbType.VarChar;
                ParCorrelativo.Size = 7;
                ParCorrelativo.Value = Entrada.Correlativo;
                SqlCmd.Parameters.Add(ParCorrelativo);


                SqlParameter ParImposto = new SqlParameter();
                ParImposto.ParameterName = "@imposto";
                ParImposto.SqlDbType = SqlDbType.Decimal;
                ParImposto.Precision= 4;
                ParImposto.Scale = 2;
                ParImposto.Value = Entrada.Imposto;
                SqlCmd.Parameters.Add(ParImposto);

                SqlParameter ParEstado = new SqlParameter();
                ParEstado.ParameterName = "@estado";
                ParEstado.SqlDbType = SqlDbType.VarChar;
                ParEstado.Size = 7;
                ParEstado.Value = Entrada.Estado;
                SqlCmd.Parameters.Add(ParEstado);

                //Executar o comando

                resp = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "Registro não foi Inserido";

                if (resp.Equals("OK"))
                {
                    //Obter o código id de entrada gerado
                    this.IdEntrada = Convert.ToInt32(SqlCmd.Parameters["@identrada"].Value);

                    foreach (DDetalhe_Entrada det in Detalhe)
                    {
                        det.IdEntrada = this.IdEntrada;

                        //Chamar método inserir no detalhes entrada
                        resp = det.Inserir(det, ref SqlCon, ref SqlTra);

                        if (!resp.Equals("OK"))
                        {
                            break;
                        }
                    }

                }
                if (resp.Equals("OK"))
                {
                    SqlTra.Commit();     //Só a partir do comit os dados são salvos com uma transação
                }else
                {
                    SqlTra.Rollback();   //Roolback cancela toda a transação, nada é salvo!!
                }

            }
            catch (Exception ex)
            {
                resp = ex.Message;
            }

            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return resp;

        }






        //Método Anular
        public string Anular(DEntrada Entrada)
        {
            string resp = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                //codigo
                SqlCon.ConnectionString = Conexao.Cn;
                SqlCon.Open();

                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spanular_entrada";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParId = new SqlParameter();
                ParId.ParameterName = "@identrada";  //identrada
                ParId.SqlDbType = SqlDbType.Int;
                ParId.Value = Entrada.IdEntrada;
                SqlCmd.Parameters.Add(ParId);



                //Executar o comando

                resp = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "A Anulação não foi feita";


            }
            catch (Exception ex)
            {
                resp = ex.Message;
            }

            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return resp;
        }


        //Método Mostrar
        public DataTable Mostrar()
        {
            DataTable DtResultado = new DataTable("entrada");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexao.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spmostrar_entrada";
                SqlCmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sqlDat = new SqlDataAdapter(SqlCmd);
                sqlDat.Fill(DtResultado);

            }
            catch (Exception ex)
            {
                DtResultado = null;
            }
            return DtResultado;

        }


        //Método Buscar por Datas
        public DataTable BuscarData(string TextoBuscar, string TextoBuscar2)
        {
            DataTable DtResultado = new DataTable("entrada");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexao.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spbuscar_entrada_data";
                SqlCmd.CommandType = CommandType.StoredProcedure;


                SqlParameter ParTextoBuscar = new SqlParameter();
                ParTextoBuscar.ParameterName = "@textobuscar";
                ParTextoBuscar.SqlDbType = SqlDbType.VarChar;
                ParTextoBuscar.Size = 150;
                ParTextoBuscar.Value = TextoBuscar;
                SqlCmd.Parameters.Add(ParTextoBuscar);

                SqlParameter ParTextoBuscar2 = new SqlParameter();
                ParTextoBuscar2.ParameterName = "@textobuscar2";
                ParTextoBuscar2.SqlDbType = SqlDbType.VarChar;
                ParTextoBuscar2.Size = 150;
                ParTextoBuscar2.Value = TextoBuscar2;
                SqlCmd.Parameters.Add(ParTextoBuscar2);

                SqlDataAdapter sqlDat = new SqlDataAdapter(SqlCmd);
                sqlDat.Fill(DtResultado);

            }
            catch (Exception ex)
            {
                DtResultado = null;
            }
            return DtResultado;

        }




        //Método Mostrar Detalhes
        public DataTable MostrarDetalhes(string TextoBuscar)
        {
            DataTable DtResultado = new DataTable("detalhe_entrada");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexao.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spmostrar_detalhes_entrada";
                SqlCmd.CommandType = CommandType.StoredProcedure;


                SqlParameter ParTextoBuscar = new SqlParameter();
                ParTextoBuscar.ParameterName = "@textobuscar";
                ParTextoBuscar.SqlDbType = SqlDbType.VarChar;
                ParTextoBuscar.Size = 150;
                ParTextoBuscar.Value = TextoBuscar;
                SqlCmd.Parameters.Add(ParTextoBuscar);

                

                SqlDataAdapter sqlDat = new SqlDataAdapter(SqlCmd);
                sqlDat.Fill(DtResultado);

            }
            catch (Exception ex)
            {
                DtResultado = null;
            }
            return DtResultado;

        }


    }
}
