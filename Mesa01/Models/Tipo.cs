using System;                       //para ter o DateTime
using System.Collections.Generic; //para usar o ICollection que é uma coleção, de lista etc..
using System.Linq;                // para usar o codigo linq e expressões lambda

namespace Mesa01.Models
{
    public class Tipo
    {
        public int Id { get; set; }                            //atributo basico da classe
        public string Nome { get; set; }                       //atributo basico da classe

        //associação de 1 Tipo com varios Fechamentos, já instanciando a coleção, para garantir que a lista seja criada
        public ICollection<Fechamento> Fechamentos { get; set; } = new List<Fechamento>();


        //tirei a associação com operadores pois não quero travar o departamento de origem com o Operador
        //associação com a classe Operador, 1 Tipo tem varios Operadores, 
        //public ICollection<Operador> Operadores { get; set; } = new List<Operador>();
        //já instanciando a coleção, só para garantir que a minha lista seja instanciada

        //construtor sem argumento, pois o framework precisa dele
        public Tipo()
        {
        }

        //Construtor com argumento, todos os atributos com exceção das coleções
        public Tipo(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        //operação (metodo customizado) para adicionar Operador na lista de Operadores do Tipo
        //public void AddSeller(Operador seller)
        //{
        //    Operadores.Add(seller);
        //}

        //operação (metodo customizado) para retornar o total de vendas do Tipo
        //public double TotalSales(DateTime inicial, DateTime final)
        //{
        //    return Operadores.Sum(operador => operador.TotalSales(inicial, final));
        //}

        //operação para adicionar um fechamento na minha lista Fechamentos
        public void AddSales(Fechamento fchto)
        {
            Fechamentos.Add(fchto);
        }

        //operação para remover um fechamento na minha lista Fechamentos
        public void RemoveSales(Fechamento fchto)
        {
            Fechamentos.Remove(fchto);
        }
        //operação de calculo do total de vendas do operador, com data inicial e final
        public double TotalSales(DateTime inicial, DateTime final)
        {
            return Fechamentos.Where(fchto => fchto.Data >= inicial && fchto.Data <= final).Sum(fchto => fchto.Valor);
        }


    }
}
