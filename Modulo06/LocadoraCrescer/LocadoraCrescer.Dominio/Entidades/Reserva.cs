﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LocadoraCrescer.Dominio.Entidades
{
    public class Reserva : EntidadeBasica
    {
        public int Id { get; private set; }
        public DateTime DataReserva { get; private set; }
        public DateTime DataDevolucaoPrevista { get; private set; }
        public DateTime? DataDevolucaoReal { get; private set; }
        public decimal ValorPrevisto { get; private set; }
        public decimal? ValorFinal { get; private set; }
        public int DiasDeReserva { get; private set; }
        [DefaultValue(Status.Em_Andamento)]
        public Status Status { get; private set; }
        public Cliente Cliente { get; private set; }
        public Pacote Pacote { get; private set; }
        public Produto Produto { get; private set; }
        public List<Opcional> Opcionais { get; private set; }

        public Reserva()
        {

        }

        public void AtribuirDataReserva(DateTime dataReserva)
        {
            DataReserva = dataReserva;
        }

        public void AtribuirDataDevolucaoPrevista(DateTime devolucao)
        {
            int resultado = devolucao.CompareTo(DateTime.UtcNow);

            if (resultado < 0 || resultado == 0)
            {
                DataDevolucaoPrevista = DateTime.UtcNow;
                Mensagens.Add("Data inválida");
                return;
            }
            
            DataDevolucaoPrevista = devolucao;
        }

        public void AtribuirCliente(Cliente cliente)
        {
            Cliente = cliente;
        }

        public void AtribuirProduto(Produto produto)
        {
            if (produto.QuantidadeDisponivel <= 0)
            {
                Mensagens.Add("Produto sem unidades disponíveis");
                return;
            }

            produto.DiminuirEstoque();
            Produto = produto;
        }

        public void AtribuirPacote(Pacote pacote)
        {
            Pacote = pacote;
        }

        public void AtribuirOpcionais(List<Opcional> opcionais)
        {
            var lista = new List<Opcional>();
            foreach (Opcional op in opcionais)
            {
                if (op.Nome.Equals("Reboque") && Produto.Nome.Equals("Fiat Mobi"))
                {
                    Mensagens.Add("Reboque não está disponível para Fiat Mobi");
                    return;
                }
                if (op.Nome.Equals("Rack") && Produto.Nome.Equals("Toyota Hilux"))
                {
                    Mensagens.Add("Rack não está disponível para Toyota Hilux");
                    return;
                }
                if (op.Nome.Equals("Cabo Bateria") && Produto.Nome != ("Volkswagem Kombi"))
                {
                    Mensagens.Add("Cabo de Bateria está disponível apenas para Volkswagem Kombi");
                    return;
                }

                if (op.QuantidadeDisponivel <= 0)
                {
                    Mensagens.Add("Opcional sem unidades disponíveis");
                    return;
                }

                op.DiminuirEstoque();
                lista.Add(op);

            }
            Opcionais = lista;
        }

        private void AtribuirStatus(Status status)
        {
            Status = status;
        }

        public void RealizarDevolucao()
        {
            DataDevolucaoReal = DateTime.UtcNow;
            CalcularValorFinal();     
        }

        public void CalcularValorPrevisto()
        {
            ValorPrevisto = 0;

            ValorPrevisto = (Produto.ValorDiaria * DiasDeReserva);

            if (Pacote != null)
            {
                ValorPrevisto = ValorPrevisto + (Pacote.ValorDiaria * DiasDeReserva);
            }

            if (Opcionais != null)
            {
                foreach (Opcional op in Opcionais)
                {
                    ValorPrevisto = (op.ValorDiaria * DiasDeReserva) + ValorPrevisto;
                }
            }
        }

        public void CalcularValorFinal()
        {
             ValorFinal = ValorPrevisto;

            var resultado = Nullable.Compare(DataDevolucaoReal, DataDevolucaoPrevista);
            double dias = DataDevolucaoReal.Value.Subtract(DataDevolucaoPrevista).TotalDays;

            if (resultado > 0)
            {
                AtribuirStatus(Status.Em_Atraso);
                ValorFinal = ValorPrevisto * (decimal)dias;
            }

            AtribuirStatus(Status.Finalizado);
        }

        public void CalcularDiasDeLocacao()
        {
            DiasDeReserva = (int)(DataDevolucaoPrevista - DataReserva).TotalDays;
        }

        public override bool Validar()
        {
            if (Cliente==null){
                Mensagens.Add("Cliente não pode ser nulo!");
                return false;
            }
            return true;
        }
    }
}
