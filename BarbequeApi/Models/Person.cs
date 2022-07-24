namespace BarbequeApi.Models
{
    public class Person
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal FoodsMoney { get; set; }
        public decimal DrinksMoney { get; set; }

        public long BarbequeId { get; set; }
        public Barbeque Barbeque { get; set; }
    }
}
