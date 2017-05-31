﻿using EditoraCrescer.Infraestrutura.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace EditoraCrescer.Infraestrutura.Repositorios
{
    public class LivroRepositorio
    {
        private Contexto contexto = new Contexto();

        public LivroRepositorio()
        {

        }
        public List<Livro> Obter()
        {
            return contexto.Livros.ToList();
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
    }
}