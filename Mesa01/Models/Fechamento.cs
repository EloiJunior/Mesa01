using System;


namespace Mesa01.Models
{
    public class Fechamento
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Operador { get; set; }
        public DateTime Data { get; set; }
        public string Empresa { get; set; }
        public float Valor { get; set; }
        public float Taxa { get; set; }
        public float Despesa { get; set; }
        public int Fluxo { get; set; }
        public string Banco { get; set; }

    }
}
