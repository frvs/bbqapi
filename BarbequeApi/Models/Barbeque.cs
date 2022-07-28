using System;
using System.Collections.Generic;
using System.Linq;

namespace BarbequeApi.Models
{
    public class Barbeque
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public ICollection<Person> Persons { get; set; }
        public string? Notes { get; set; }
    }
}
