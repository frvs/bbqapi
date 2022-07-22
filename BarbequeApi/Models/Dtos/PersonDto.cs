namespace BarbequeApi.Models.Dtos
{
    public class PersonDto
    {
        public string FullName { get; set; } = "Wait";
        public string NameFormatted => FormatName(FullName);
        public decimal TotalMoney => FoodsMoney + DrinksMoney;
        public decimal FoodsMoney { get; set; } = 20;
        public decimal DrinksMoney { get; set; } = 0;

        private string FormatName(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                return string.Empty;
            }

            if(!name.Contains(" "))
            {
                return name.Substring(0, 1).ToUpper() + name.Substring(1, name.Length);
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
