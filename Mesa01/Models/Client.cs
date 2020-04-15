

using System;
using System.Collections.Generic; //para usar um ICollection, que é uma coleção de Lista etc...
using System.ComponentModel.DataAnnotations; //para usar o [Display] ou [DataType] ou [DisplayFormat], que é uma tag (anotação) de anotação personalizada, usada para difinir como aparecerá na tela do usuario
using System.Linq;                //para usar os codigos linq e as expressões lambda
using Mesa01.Models.Enums;

namespace Mesa01.Models
{
    public class Client
    {
        public int Id { get; set; }




        [Required(ErrorMessage = "Name required")]                                                    //anotation que define que o campo é obrigatorio, e mensagem de erro se quiser
        //[Required(ErrorMessage = "{0} required")]                                                   //como opção podemos automatizar alguns strings da mensagem de erro
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Name size shold be between 3 and 60")]   //anotation que define tamanho minimo e maximo dessa string
        //[[StringLength(60, MinimumLength =3, ErrorMessage = "{0} size shold be between {2} and {1}")]  //como opção podemos automatizar alguns strings da mensagem de erro
        public string Nome { get; set; }

        [Required(ErrorMessage = "{0} required")]              //anotation de verificação de campo obrigatorio
        [EmailAddress(ErrorMessage = "Enter a valid email")]  //anotation de verificação de email
        [DataType(DataType.EmailAddress)]       // anotation usada para transformar o email escrito em link na tela do usuario
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} required")] //anotation de verificação de campo obrigatorio
        [Display(Name = "Birth Date")]            //tag usada para personalizar como o atributo aparecerá no display, que é a tela do site
        [DataType(DataType.Date)]                // anotation usada para personalizar como aparecerá os dados na tela
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]  //anotation usada para configurar a data como dia, mes e ano.
        public DateTime? BirthDate { get; set; }


        public TipoRegistroNacional TipoRegistroNacional { get; set; } //associação de 1 cliente com apenas 1 RegistroNacional


        public int RegistroNacional { get; set; }


        //associação de 1 Operador com varios Fechamentos, já instanciando a coleção, para garantir que a lista seja criada
        public ICollection<Fechamento> Fechamentos { get; set; } = new List<Fechamento>();

        //construtor sem argumento
        public Client()
        {
        }

        //construtor com argumento
        public Client(int id, string nome, string email, DateTime? birthDate, TipoRegistroNacional tipoRegistroNacional, int registroNacional)//Tipo tipo//
        {
            Id = id;
            Nome = nome;
            Email = email;
            BirthDate = birthDate;
            TipoRegistroNacional = tipoRegistroNacional;
            RegistroNacional = registroNacional;
            //Tipo = tipo;
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
