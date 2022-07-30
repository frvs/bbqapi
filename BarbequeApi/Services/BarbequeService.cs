using BarbequeApi.Models;
using BarbequeApi.Models.Dtos;
using BarbequeApi.Repositories;
using BarbequeApi.Validators;
using System;
using System.Collections.Generic;

namespace BarbequeApi.Services
{
  public interface IBarbequeService
  {
    (bool, List<string>) Create(BarbequeDto barbequeDto);
    (BarbequeDto?, List<string>) Get(string barbequeId);
  }

  public class BarbequeService : IBarbequeService
  {
    private readonly IBarbequeRepository barbequeRepository;
    private readonly BarbequeValidator validator;

    public BarbequeService(IBarbequeRepository barbequeRepository)
    {
      this.barbequeRepository = barbequeRepository;
    }

    public (bool, List<string>) Create(BarbequeDto barbequeDto)
    {
      var validator = new BarbequeValidator();
      var (isDtoValid, errorMessages) = validator.Validate(barbequeDto);

      if (!isDtoValid)
      {
        return (isDtoValid, errorMessages);
      }

      var barbeque = Translator.ToBarbeque(barbequeDto);
      FillDefaultValues(barbeque); 

      var successful = barbequeRepository.Save(barbeque);

      if (!successful)
      {
        errorMessages.Add("500: Error in barbequeRepository.Save()");
      }

      return (successful, errorMessages);
    }

    public (BarbequeDto?, List<string>) Get(string barbequeIdString)
    {
      List<string> errorMessages = new();

      var successfullyParsed = long.TryParse(barbequeIdString, out var barbequeId);
      if (!successfullyParsed || barbequeId <= 0) 
      {
        errorMessages.Add("400: BarbequeId should be a integer greater than zero.");
        return (null, errorMessages);
      }

      var barbeque = barbequeRepository.Get(barbequeId);

      if (barbeque == null)
      {
        errorMessages.Add($"404: Cannot found a Barbeque with Id equals {barbequeIdString}.");
        return (null, errorMessages);
      }

      var barbequeDto = Translator.ToBarbequeDto(barbeque);

      return (barbequeDto, errorMessages);
    }

    private void FillDefaultValues(Barbeque barbeque)
    {
      barbeque.Title = string.IsNullOrWhiteSpace(barbeque.Title) ? "Sem motivo" : barbeque.Title;
      barbeque.Date = barbeque.Date == default ? DateTime.Now : barbeque.Date;
    }
  }
}
