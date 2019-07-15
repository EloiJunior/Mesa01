using System;
using System.ComponentModel.DataAnnotations;  //para usar as anotações de Formatos
using Mesa01.Models.Enums; // para usar o SaleStatus que é um Enum


namespace Mesa01.Models
{
    public class Fechamento
    {
        public int Id { get; set; }            //atributo basico
        public string Tipo { get; set; }       //atributo basico

        public Operador Operador { get; set; } //associação de 1 Fechamento para 1 Operador
        public int OperadorId { get; set; }  //foreign Key

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Data { get; set; }     //atributo basico

        public string Empresa { get; set; }    //atributo basico

        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Valor { get; set; }      //atributo basico

        public double Taxa { get; set; }       //atributo basico
        public double Despesa { get; set; }    //atributo basico
        public int Fluxo { get; set; }         //atributo basico
        public string Banco { get; set; }      //atributo basico
        public SaleStatus Status { get; set; } //associação de 1 Fechamento com 1 SaleStatus

        //Construtor sem argumento, precisamos criar pois o framework precisa dele
        public Fechamento()
        {
        }

        //Construtor com argumento
        public Fechamento(int id, string tipo, Operador operador, DateTime data, string empresa, double valor, double taxa, double despesa, int fluxo, string banco, SaleStatus status)
        {
            Id = id;
            Tipo = tipo;
            Operador = operador;
            Data = data;
            Empresa = empresa;
            Valor = valor;
            Taxa = taxa;
            Despesa = despesa;
            Fluxo = fluxo;
            Banco = banco;
            Status = status;
        }

    }
}
