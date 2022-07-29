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
    BarbequeDto Get(long barbequeId);
  }

  public class BarbequeService : IBarbequeService
  {
    private readonly IBarbequeRepository barbequeRepository;

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

      bool successful = barbequeRepository.Save(barbeque);

      if (!successful)
      {
        errorMessages.Add("Error in barbequeRepository.Save()");
      }

      return (successful, errorMessages);
    }

    public BarbequeDto Get(long barbequeId)
    {
      if (barbequeId <= 0) // for the simplicity, i'll do this check in controller. its not the best practice but...
      {
        // error handler. should I throw?
      }

      var barbeque = barbequeRepository.Get(barbequeId);

      if (barbeque == null)
      {
        return null;
      }

      var barbequeDto = Translator.ToBarbequeDto(barbeque);

      return barbequeDto;
    }

    private void FillDefaultValues(Barbeque barbeque)
    {
      barbeque.Title = string.IsNullOrWhiteSpace(barbeque.Title) ? "Sem motivo" : barbeque.Title;
      barbeque.Date = barbeque.Date == default ? DateTime.Now : barbeque.Date;
    }
  }
}
