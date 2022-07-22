namespace BarbequeApi.Models.Dtos
{
    public class PersonDto
    {
        public string FullName { get; set; }
        public string ShortName => FullName[..(FullName.IndexOf(' ') + 1)] + ".";
        public decimal MoneyInvested { get; set; }
        public bool HasPaid { get; set; }
    }
}
