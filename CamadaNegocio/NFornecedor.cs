using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CamadaDados;

namespace CamadaNegocio
{
    public class NFornecedor
    {
        //Método Inserir
        public static string Inserir(string empresa, string setor_comercial, string tipo_documento, string num_documento, string endereco, string telefone, string email, string url)
        {
            DFornecedor Obj = new CamadaDados.DFornecedor();
            Obj.Empresa = empresa;
            Obj.Setor_Comercial = setor_comercial;
            Obj.Tipo_Documento = tipo_documento;
            Obj.Num_Documento = num_documento;
            Obj.Endereco = endereco;
            Obj.Telefone = telefone;
            Obj.Email = email;
            Obj.Url = url;

            return Obj.Inserir(Obj);
        }


        //Método Editar
        public static string Editar(int id, string empresa, string setor_comercial, string tipo_documento, string num_documento, string endereco, string telefone, string email, string url)
        {
            DFornecedor Obj = new CamadaDados.DFornecedor();
            Obj.Id = id;
            Obj.Empresa = empresa;
            Obj.Setor_Comercial = setor_comercial;
            Obj.Tipo_Documento = tipo_documento;
            Obj.Num_Documento = num_documento;
            Obj.Endereco = endereco;
            Obj.Telefone = telefone;
            Obj.Email = email;
            Obj.Url = url;

            return Obj.Editar(Obj);
        }



        //Método Deletar
        public static string Excluir(int id)
        {
            DFornecedor Obj = new CamadaDados.DFornecedor();
            Obj.Id = id;

            return Obj.Excluir(Obj);
        }


        //Método Mostrar
        public static DataTable Mostrar()
        {
            return new DFornecedor().Mostrar();


        }


        //Método Buscar Nome Empresa
        public static DataTable BuscarNome(string textobuscar)
        {
            DFornecedor Obj = new DFornecedor();
            Obj.TextoBuscar = textobuscar;
            return Obj.BuscarNome(Obj);
        }

        //Método Buscar Num Doc
        public static DataTable BuscarDocumento(string textobuscar)
        {
            DFornecedor Obj = new DFornecedor();
            Obj.TextoBuscar = textobuscar;
            return Obj.BuscarDocumento(Obj);
        }

    }
}
