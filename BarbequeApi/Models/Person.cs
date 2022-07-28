namespace BarbequeApi.Models
{
    public class Person
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal FoodMoneyShare { get; set; }
        public decimal BeverageMoneyShare { get; set; }

        public long BarbequeId { get; set; }
        public Barbeque Barbeque { get; set; }
    }
}
