using BarbequeApi.Models;
using BarbequeApi.Models.Dtos;
using BarbequeApi.Repositories;
using BarbequeApi.Validators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BarbequeApi.Services
{
  public interface IPersonService
  {
    (bool, List<string>) Create(string barbequeId, PersonDto personDto);
    (bool, List<string>) Delete(string barbequeId, string personId);
  }

  public class PersonService : IPersonService
  {
    private readonly IBarbequeRepository barbequeRepository;
    private readonly IPersonRepository repository;

    public PersonService(IBarbequeRepository barbequeRepository, IPersonRepository repository)
    {
      this.barbequeRepository = barbequeRepository;
      this.repository = repository;
    }

    public (bool, List<string>) Create(string barbequeIdString, PersonDto personDto)
    {
      var successful = true;
      List<string> errorMessages = new();

      var successfullyParsed = long.TryParse(barbequeIdString, out var barbequeId);

      if(!successfullyParsed || barbequeId <= 0)
      {
        successful &= false;
        errorMessages.Add("400: BarbequeId should be a integer greater than zero.");
      }
      var personValidator = new PersonValidator();

      var (isValidationSuccessful, validationMessages) = personValidator.Validate(personDto);

      if(isValidationSuccessful)
      {
        successful &= isValidationSuccessful;
        errorMessages.AddRange(validationMessages);
      }
      
      if (!successful)
      {
        return (successful, errorMessages);
      }

      var barbeque = barbequeRepository.Get(barbequeId);

      if (barbeque == null)
      {
        successful &= false;
        errorMessages.Add($"404: Error in barbequeRepository.Get({barbequeId})");

        return (successful, errorMessages);
      }

      if (personDto.Id != 0 && barbeque.Persons.Select(p => p.Id).Contains(personDto.Id))
      {
        successful &= false;
        errorMessages.Add($"400: PersonDto.Id {personDto.Id} is already in Barbeque.Id {barbequeId}.");

        return (successful, errorMessages);
      }

      var person = Translator.ToPerson(personDto);
      person.BarbequeId = barbequeId;
      FillDefaultValues(person, barbeque);

      var successfulDatabaseChanges = repository.Save(person);

      if(!successfulDatabaseChanges)
      {
        successful &= successfulDatabaseChanges;
        errorMessages.Add("404: Error in PersonRepository.Save()");
      }

      return (successful, errorMessages);
    }

    public (bool, List<string>) Delete(string barbequeIdString, string personIdString)
    {
      var successful = true;
      List<string> errorMessages = new();

      var personIdSuccessfullyParsed = long.TryParse(personIdString, out var personId);
      var barbequeIdSuccessfullyParsed = long.TryParse(personIdString, out var barbequeId);

      if (!personIdSuccessfullyParsed || personId <= 0)
      {
        successful &= false;
        errorMessages.Add("400: PersonId should be a integer greater than zero.");
      }

      if (!barbequeIdSuccessfullyParsed || barbequeId <= 0)
      {
        successful &= false;
        errorMessages.Add("400: BarbequeId should be a integer greater than zero.");
      }

      if(!successful)
      {
        return (successful, errorMessages);
      }

      var barbeque = barbequeRepository.Get(barbequeId);

      if (barbeque == null)
      {
        successful &= false;
        errorMessages.Add($"404: Cannot found Barbeque with Id {barbequeId}");

        return (successful, errorMessages);
      }

      if (barbeque.Persons.FirstOrDefault(p => p.Id == personId) == null)
      {
        successful &= false;
        errorMessages.Add($"404: Error: cannot found Person {personId} in Barbeque.Id {barbequeId}");

        return (successful, errorMessages);
      }

      var succesful = repository.Delete(barbeque, personId);

      if (!succesful)
      {
        successful &= false;
        errorMessages.Add("404: Error in PersonRepository.Delete()");
      }

      return (successful, errorMessages);
    }

    private void FillDefaultValues(Person person, Barbeque barbequeDto)
    {
      person.Name = string.IsNullOrWhiteSpace(person.Name) ? "Jane Doe" : person.Name;

      if (person.FoodMoneyShare == 0 && person.BeverageMoneyShare == 0)
      {
        person.FoodMoneyShare = 20;
        person.BeverageMoneyShare = 10;
      }
    }
  }
}
