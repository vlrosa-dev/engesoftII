using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CamadaDados;

namespace CamadaNegocio
{
    public class NFuncionario
    {

        //Método Inserir
        public static string Inserir(string nome, string sobrenome, string sexo, DateTime data_nasc, string num_documento, string endereco, string telefone, string email, string acesso, string usuario, string senha)
        {
            DFuncionario Obj = new CamadaDados.DFuncionario();
            Obj.Nome = nome;
            Obj.Sobrenome = sobrenome;
            Obj.Sexo = sexo;
            Obj.DataNascimento = data_nasc;
            Obj.NumDocumento = num_documento;
            Obj.Endereco = endereco;
            Obj.Telefone = telefone;
            Obj.Email = email;
            Obj.Acesso = acesso;
            Obj.Usuario = usuario;
            Obj.Senha = senha;

            return Obj.Inserir(Obj);
        }


        //Método Editar
        public static string Editar(int id, string nome, string sobrenome, string sexo, DateTime data_nasc, string num_documento, string endereco, string telefone, string email, string acesso, string usuario, string senha)
        {
            DFuncionario Obj = new CamadaDados.DFuncionario();
            Obj.Id = id;
            Obj.Nome = nome;
            Obj.Sobrenome = sobrenome;
            Obj.Sexo = sexo;
            Obj.DataNascimento = data_nasc;
            Obj.NumDocumento = num_documento;
            Obj.Endereco = endereco;
            Obj.Telefone = telefone;
            Obj.Email = email;
            Obj.Acesso = acesso;
            Obj.Usuario = usuario;
            Obj.Senha = senha;

            return Obj.Editar(Obj);
        }



        //Método Deletar
        public static string Excluir(int id)
        {
            DFuncionario Obj = new CamadaDados.DFuncionario();
            Obj.Id = id;

            return Obj.Excluir(Obj);
        }


        //Método Mostrar
        public static DataTable Mostrar()
        {
            return new DFuncionario().Mostrar();


        }


        //Método Buscar Nome Empresa
        public static DataTable BuscarNome(string textobuscar)
        {
            DFuncionario Obj = new DFuncionario();
            Obj.TextoBuscar = textobuscar;
            return Obj.BuscarNome(Obj);
        }

        //Método Buscar Num Doc
        public static DataTable BuscarDocumento(string textobuscar)
        {
            DFuncionario Obj = new DFuncionario();
            Obj.TextoBuscar = textobuscar;
            return Obj.BuscarDocumento(Obj);
        }



        //Método Login
        public static DataTable Login(string usuario, string senha)
        {
            DFuncionario Obj = new DFuncionario();
            Obj.Usuario = usuario;
            Obj.Senha = senha;
            return Obj.Login(Obj);
        }


    }
}
