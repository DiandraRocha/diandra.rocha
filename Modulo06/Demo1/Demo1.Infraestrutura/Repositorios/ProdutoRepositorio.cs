﻿using Demo1.Dmonio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo1.Infraestrutura.Repositorios
{
    public class ProdutoRepositorio
    {

        string stringConexao = @"Server=13.65.101.67; User Id=diandra.rocha;Password=123456;Database=aluno24db";
        public void Criar(Produto produto)
        {
            using (var conexao = new SqlConnection(stringConexao))
            {
                conexao.Open();

                //executa insert
                using (var comando = conexao.CreateCommand())
                {
                    comando.CommandText = @"INSERT INTO Produto (Nome, Preco, Estoque) VALUES (@nome, @preco, @estoque);";
                    comando.Parameters.AddWithValue("@nome", produto.Nome);
                    comando.Parameters.AddWithValue("@preco", produto.Preco);
                    comando.Parameters.AddWithValue("@estoque", produto.Estoque);

                    comando.ExecuteNonQuery();

                }

                //trás o último id
                using (var comando = conexao.CreateCommand())
                {
                    comando.CommandText = @"SELECT @@IDENTITY";
                    var result = (decimal)comando.ExecuteScalar();
                    produto.Id = (int)result;
                }
            }

        }

        public List<Produto> Listar()
        {
            var produtos = new List<Produto>();

            using (var conexao = new SqlConnection(stringConexao))
            {
                conexao.Open();

                using(var comando = conexao.CreateCommand())
                {
                    comando.CommandText = "SELECT Id, Nome, Preco, Estoque FROM Produto";

                    
                    var dataReader = comando.ExecuteReader();
                    while (dataReader.Read())
                    {
                        var produto = new Produto();
                        produto.Id = (int)dataReader["Id"];
                        produto.Nome = (string)dataReader["Nome"];
                        produto.Preco = (decimal)dataReader["Preco"];
                        produto.Estoque = (int)dataReader["Estoque"];
                        produtos.Add(produto);
                    }

                }
            }
            return produtos;
        }
    }
}
