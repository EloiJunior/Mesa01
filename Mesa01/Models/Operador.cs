using System;
using System.Collections.Generic; //para usar um ICollection, que é uma coleção de Lista etc...
using System.Linq;                //para usar os codigos linq e as expressões lambda
namespace Mesa01.Models
{
    public class Operador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public double BaseSalary { get; set; }
        public Departamento Departamento { get; set; } //associação de 1 Operador com apenas 1 Departamento
        public int DepartamentoId { get; set; } //foreign key

        //associação de 1 Operador com varios Fechamentos, já instanciando a coleção, para garantir que a lista seja criada
        public ICollection<Fechamento> Fechamentos { get; set; } = new List<Fechamento>(); 

        //construtor sem argumento
        public Operador()
        {
        }

        //construtor com argumento
        public Operador(int id, string nome, string email, DateTime birthDate, double baseSalary, Departamento departamento)
        {
            Id = id;
            Nome = nome;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Departamento = departamento;
        }

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
