using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BarbequeApi.Models.Dtos
{
    public class BarbequeDto
    {
        public long Id { get; set; }
        public string Title { get; set; } = "Sem motivo";
        public DateTime Date { get; set; } = DateTime.Now;
        public List<PersonDto> Persons { get; set; } = new();
        public string DateFormatted => Date.ToString("dd/MM", new CultureInfo("pt-BR"));
        public int NumberOfParticipants => Persons.Count;
        public decimal MoneyInvested => Persons.Select(p => p.TotalMoney).Sum(n => Convert.ToDecimal(n));

    }
}
