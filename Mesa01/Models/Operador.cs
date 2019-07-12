using System;
using System.Collections.Generic; //para usar um ICollection, que é uma coleção de Lista etc...
using System.ComponentModel.DataAnnotations; //para usar o [Display] ou [DataType] ou [DisplayFormat], que é uma tag (anotação) de anotação personalizada, usada para difinir como aparecerá na tela do usuario
using System.Linq;                //para usar os codigos linq e as expressões lambda
namespace Mesa01.Models
{
    public class Operador
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        [DataType(DataType.EmailAddress)]       // anotation usada para transformar o email escrito em link na tela do usuario
        public string Email { get; set; }

        [Display(Name ="Birth Date")]            //tag usada para personalizar como o atributo aparecerá no display, que é a tela do site
        [DataType(DataType.Date)]                // anotation usada para personalizar como aparecerá os dados na tela
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]  //anotation usada para configurar a data como dia, mes e ano.
        public DateTime BirthDate { get; set; }

        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString ="{0:F2}")]    // anotation usada para configurar o formato para mostrar os dados, o 0 indica o valor do atributo, F2 indica 2 casas decimais
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
