using System.Collections.Generic; //para usar o ICollection

namespace Mesa01.Models.ViewModels
{
    public class FechamentoFormViewModel
    {
        public Fechamento Fechamento { get; set; }
        public ICollection<Operador> Operadores { get; set; }
    }
}
