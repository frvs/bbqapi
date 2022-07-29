namespace BarbequeApi.Models.Dtos
{
  public class PersonDto : BaseDto
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public decimal FoodMoneyShare { get; set; }
    public decimal BeverageMoneyShare { get; set; }

    public string NameFormatted => FormatName(Name);
    public decimal BudgetAmount => FoodMoneyShare + BeverageMoneyShare;

    public long BarbequeId { get; set; }

    private string FormatName(string name)
    {
      if (string.IsNullOrEmpty(name))
      {
        return string.Empty;
      }

      if (!name.Contains(' '))
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
