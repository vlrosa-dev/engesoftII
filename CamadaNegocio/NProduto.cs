using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CamadaDados;

namespace CamadaNegocio
{
   public class NProduto
    {

        //Método Inserir
        public static string Inserir(string codigo, string nome, string descricao, byte[] imagem, int idcategoria, int idapresentacao, int estoque_minimo)
        {
            Console.WriteLine("NPRODUTO!!!!!!!!");
            DProduto Obj = new CamadaDados.DProduto();
            Obj.Codigo = codigo;
            Obj.Nome = nome;
            Obj.Descricao = descricao;
            Obj.Imagem = imagem;
            Obj.IdCategoria = idcategoria;
            Obj.IdApresentacao = idapresentacao;
            Obj.Estoque_minimo = estoque_minimo;

            return Obj.Inserir(Obj);
        }


        //Método Editar
        public static string Editar(int id, string codigo, string nome, string descricao, byte[] imagem, int idcategoria, int idapresentacao, int estoque_minimo)
        {
            DProduto Obj = new CamadaDados.DProduto();
            Obj.Id = id;
            Obj.Codigo = codigo;
            Obj.Nome = nome;
            Obj.Descricao = descricao;
            Obj.Imagem = imagem;
            Obj.IdCategoria = idcategoria;
            Obj.IdApresentacao = idapresentacao;
            Obj.Estoque_minimo = estoque_minimo;

            return Obj.Editar(Obj);
        }



        //Método Deletar
        public static string Excluir(int id)
        {
            DProduto Obj = new CamadaDados.DProduto();
            Obj.Id = id;

            return Obj.Excluir(Obj);
        }


        //Método Mostrar
        public static DataTable Mostrar()
        {
            return new DProduto().Mostrar();


        }


        //Método Buscar Nome
        public static DataTable BuscarNome(string textobuscar)
        {
            DProduto Obj = new DProduto();
            Obj.TextoBuscar = textobuscar;
            return Obj.BuscarNome(Obj);
        }


        //Método Estoque Produto
        public static DataTable EstoqueProduto()
        {
            return new DProduto().Estoque_Produto();


        }

    }
}
