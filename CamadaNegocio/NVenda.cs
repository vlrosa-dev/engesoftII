using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CamadaDados;

namespace CamadaNegocio
{
    public class NVenda
    {

        //Método Inserir
        public static string Inserir(int idcliente, int idfuncionario,
            DateTime data, string tipo_comprovante, string serie,
            string correlativo, decimal imposto, 
            DataTable dtDetalhes)
        {
            DVenda Obj = new DVenda();
            Obj.IdFuncionario = idfuncionario;
            Obj.IdCliente = idcliente;
            Obj.Data = data;
            Obj.Tipo_Comprovante = tipo_comprovante;
            Obj.Serie = serie;
            Obj.Correlativo = correlativo;
            Obj.Imposto = imposto;
            

            List<DDetalhe_Venda> detalhes = new List<DDetalhe_Venda>();

            foreach (DataRow row in dtDetalhes.Rows)
            {
                DDetalhe_Venda detalhe = new DDetalhe_Venda();
               
                detalhe.IdDetalhe_Entrada = Convert.ToInt32(row["iddetalhe_entrada"].ToString());
                detalhe.Quantidade = Convert.ToInt32(row["quantidade"].ToString());
                detalhe.Preco_Venda = Convert.ToDecimal(row["preco_venda"].ToString());                
                detalhe.Desconto = Convert.ToDecimal(row["desconto"].ToString());
                detalhes.Add(detalhe);
            }

            return Obj.Inserir(Obj, detalhes);
        }


        //Método Deletar
        public static string Deletar(int id)
        {
            DVenda Obj = new DVenda();
            Obj.IdVenda = id;

            return Obj.Excluir(Obj);
        }


        //Método Mostrar
        public static DataTable Mostrar()
        {
            return new DVenda().Mostrar();


        }


        //Método Buscar por Data
        public static DataTable BuscarData(string textobuscar, string textobuscar2)
        {
            DVenda Obj = new DVenda();
            return Obj.BuscarData(textobuscar, textobuscar2);
        }


        //Método Mostrar
        public static DataTable MostrarDetalhes(string textobuscar)
        {
            DVenda Obj = new DVenda();
            return Obj.MostrarDetalhes(textobuscar);
        }


        //Método Mostrar Produto Venda por Nome
        public static DataTable MostrarProduto_Venda_Nome(string textobuscar)
        {
            DVenda Obj = new DVenda();
            return Obj.MostrarProduto_Venda_Nome(textobuscar);
        }

        //Método Mostrar Produto Venda por Código
        public static DataTable MostrarProduto_Venda_Codigo(string textobuscar)
        {
            DVenda Obj = new DVenda();
            return Obj.MostrarProduto_Venda_Codigo(textobuscar);
        }

    }
}
