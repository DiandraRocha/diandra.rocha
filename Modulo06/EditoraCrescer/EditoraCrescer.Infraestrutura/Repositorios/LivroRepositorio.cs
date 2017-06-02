﻿using EditoraCrescer.Infraestrutura.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EditoraCrescer.Infraestrutura.Repositorios
{
    public class LivroRepositorio : IDisposable
    {
        private Contexto contexto = new Contexto();
        private static int qtdPular =0 ;

        public LivroRepositorio()
        {

        }

        public List<dynamic> Obter()
        {
            var livros = contexto.Livros.ToList();

            return contexto.Livros.Select(x =>
            new { Isbn = x.Isbn, Titulo = x.Titulo, Capa = x.Capa, NomeAutor = x.Autor.Nome, Genero = x.Genero })
            .ToList<dynamic>();
        }

        public Livro ObterPorIsbn(int isbn)
        {
            return contexto.Livros.SingleOrDefault(x => x.Isbn == isbn);
        }

        public List<dynamic> ObterPorGenero(string genero)
        {
            var lista =  contexto.Livros.Where(x => x.Genero.Contains(genero)).ToList();

            return lista.Select(x =>
            new { Isbn = x.Isbn, Titulo = x.Titulo, Capa = x.Capa, NomeAutor = x.Autor.Nome, Genero = x.Genero })
            .ToList<dynamic>();
        }

        public List<dynamic> ObterPorData()
        {
            var livros = contexto.Livros.ToList();    

            return livros.Where(x => ((DateTime.Now - x.DataPublicacao).TotalDays) >= 7).Select(x =>
            new { Isbn = x.Isbn, Titulo = x.Titulo, Capa = x.Capa, NomeAutor = x.Autor.Nome, Genero = x.Genero })
            .ToList<dynamic>();
        }

        public List<dynamic> ObterParaPaginação(int qtdTrazer)
        {
            qtdTrazer = 10;

            return contexto.Livros.Select(x =>
            new { Titulo = x.Titulo, Capa = x.Capa, Genero = x.Genero, NomeAutor = x.Autor.Nome })
            .Take(qtdTrazer)
            .Skip(qtdPular)
            .ToList<dynamic>();

            qtdPular =+ 10;

        }

        public bool VerificarLivro(Livro livro)
        {
            return contexto.Livros.Count(x => x.Isbn == livro.Isbn) != 0;
        }


        public void Adicionar(Livro livro)
        {
            contexto.Livros.Add(livro);
            contexto.SaveChanges();
        }

        public void Deletar(int Isbn)
        {
            var itemToRemove = contexto.Livros.SingleOrDefault(x => x.Isbn == Isbn);
            if (itemToRemove != null)
            {
                contexto.Livros.Remove(itemToRemove);
                contexto.SaveChanges();
            }

        }

        public Livro Atualizar(int isbn, Livro livro)
        {

            contexto.Entry(livro).State = System.Data.Entity.EntityState.Modified;

            contexto.SaveChanges();
            return livro;
        }

        public void Dispose()
        {
            contexto.Dispose();
        }
    }
}