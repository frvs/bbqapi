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

    private bool NoCharAfterSpace(string str) => str.IndexOf(' ') + 1 > str.Length - 1;
    private string FormatName(string name)
    {
      if (string.IsNullOrWhiteSpace(name))
      {
        return string.Empty;
      }

      if (!name.Contains(' ') || NoCharAfterSpace(name))
      {
        return name.Substring(0, 1).ToUpper() + name.Substring(1).Trim();
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
