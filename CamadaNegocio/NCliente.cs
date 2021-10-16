using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CamadaDados;

namespace CamadaNegocio
{
    public class NCliente
    {
        //Método Inserir
        public static string Inserir(string nome, string sobrenome, string sexo, DateTime data_nasc, string tipo_documento, string num_documento, string endereco, string telefone, string email)
        {
            DCliente Obj = new CamadaDados.DCliente();
            Obj.Nome = nome;
            Obj.Sobrenome = sobrenome;
            Obj.Sexo = sexo;
            Obj.DataNascimento = data_nasc;
            Obj.TipoDocumento = tipo_documento;
            Obj.NumDocumento = num_documento;
            Obj.Endereco = endereco;
            Obj.Telefone = telefone;
            Obj.Email = email;
            

            return Obj.Inserir(Obj);
        }


        //Método Editar
        public static string Editar(int id, string nome, string sobrenome, 
            string sexo, DateTime data_nasc, string tipo_documento, 
            string num_documento, string endereco, string telefone, 
            string email)
        {
            DCliente Obj = new CamadaDados.DCliente();
            Obj.Id = id;
            Obj.Nome = nome;
            Obj.Sobrenome = sobrenome;
            Obj.Sexo = sexo;
            Obj.DataNascimento = data_nasc;
            Obj.TipoDocumento = tipo_documento;
            Obj.NumDocumento = num_documento;
            Obj.Endereco = endereco;
            Obj.Telefone = telefone;
            Obj.Email = email;
            

            return Obj.Editar(Obj);
        }



        //Método Deletar
        public static string Excluir(int id)
        {
            DCliente Obj = new CamadaDados.DCliente();
            Obj.Id = id;

            return Obj.Excluir(Obj);
        }


        //Método Mostrar
        public static DataTable Mostrar()
        {
            return new DCliente().Mostrar();


        }


        //Método Buscar Nome Cliente
        public static DataTable BuscarNome(string textobuscar)
        {
            DCliente Obj = new DCliente();
            Obj.TextoBuscar = textobuscar;
            return Obj.BuscarNome(Obj);
        }

        //Método Buscar Num Doc
        public static DataTable BuscarDocumento(string textobuscar)
        {
            DCliente Obj = new DCliente();
            Obj.TextoBuscar = textobuscar;
            return Obj.BuscarDocumento(Obj);
        }
    }
}
