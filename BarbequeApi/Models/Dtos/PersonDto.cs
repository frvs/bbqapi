namespace BarbequeApi.Models.Dtos
{
    public class PersonDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal FoodsMoney { get; set; }
        public decimal DrinksMoney { get; set; }
        
        public string NameFormatted => FormatName(Name);
        public decimal TotalMoney => FoodsMoney + DrinksMoney;

        public long BarbequeId { get; set; }

        private string FormatName(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                return string.Empty;
            }

            if(!name.Contains(" "))
            {
                return name.Substring(0, 1).ToUpper() + name.Substring(1, name.Length - 1);
            }

            var indexOfFirstSpaceSeparator = name.IndexOf(" ");

            return (
                name.Substring(0, 1).ToUpper() 
                + name.Substring(1, indexOfFirstSpaceSeparator - 1)
                + " "
                + name.Substring(indexOfFirstSpaceSeparator + 1, 1).ToUpper() + ".");
        }
    }
}
