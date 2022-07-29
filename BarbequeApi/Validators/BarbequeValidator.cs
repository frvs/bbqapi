using BarbequeApi.Models.Dtos;
using System;
using System.Collections.Generic;

namespace BarbequeApi.Validators
{
  public class BarbequeValidator : AbstractValidator<BarbequeDto>
  {
    public override (bool, List<string>) Validate(BarbequeDto barbequeDto)
    {
      bool isValid = true;
      List<string> errors = new List<string>();
      var sqlServerMinimumDateTime = new DateTime(1753, 1, 1);

      if (barbequeDto == null)
      {
        isValid &= false;
        errors.Add("BarbequeDto should not be null.");

        return (isValid, errors);
      }


      if (string.IsNullOrWhiteSpace(barbequeDto.Title))
      {
        isValid &= false;
        errors.Add("BarbequeDto.Title should not be null, empty string or white spaces.");
      }

      var personValidator = new PersonValidator();
      if (barbequeDto.Persons != null && barbequeDto.Persons.Count > 0)
      {
        for (int index = 0; index < barbequeDto.Persons.Count; index++)
        {
          var personDto = barbequeDto.Persons[index];
          var (isPersonValid, errorMessages) = personValidator.Validate(personDto);
          isValid &= isPersonValid;

          if (!isPersonValid)
          {
            errors.Add($"BarbequeDto.Person[{index}] is not valid: {string.Join(',', errorMessages)}");
          }
        }
      }

      if (barbequeDto.Date < sqlServerMinimumDateTime) // sqlserver minimum date
      {
        isValid &= false;
        errors.Add("BarbequeDto.Date should be equals or after January 1, 1753.");
      }

      if (barbequeDto.Id != 0) // trying to send an existing barbeque
      {
        // this should not throw an error (right now).
        barbequeDto.Id = 0;
      }

      return (isValid, errors);
    }
  }
}
