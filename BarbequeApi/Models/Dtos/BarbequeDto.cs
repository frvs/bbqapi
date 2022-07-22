using System.Globalization;

namespace BarbequeApi.Models.Dtos
{
    public class BarbequeDto
    {
        public string Title { get; set; } // default = "Sem motivo"
        public DateTime Date { get; set; }
        public List<PersonDto> Persons { get; set; } = new();
        public string DateInTitle => Date.ToString("m", new CultureInfo("pt-BR"));
        // acho que vai ser logado 10 Janeiro em vez de 10/01
        public int NumberOfParticipants => Persons.Count;
        public decimal MoneyInvested => Persons.Where(p => p.HasPaid).Sum(n => Convert.ToDecimal(n));

    }
}
