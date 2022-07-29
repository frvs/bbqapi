using BarbequeApi.Models.Dtos;
using System.Collections.Generic;

namespace BarbequeApi.Validators
{
    public class PersonValidator : AbstractValidator<PersonDto>
    {
        public override (bool, List<string>) Validate(PersonDto personDto)
        {
            bool isValid = true;
            List<string> errors = new List<string>();

            if (personDto == null)
            {
                isValid &= false;
                errors.Add("PersonDto should not be null.");

                return (isValid, errors);
            }

            if (string.IsNullOrWhiteSpace(personDto.Name))
            {
                isValid &= false;
                errors.Add("PersonDto.Name should not be null, empty string or white spaces.");
            }

            if(personDto.BeverageMoneyShare < 0)
            {
                isValid &= false;
                errors.Add("PersonDto.BeverageMoneyShare should not be negative.");
            }

            if (personDto.FoodMoneyShare < 0)
            {
                isValid &= false;
                errors.Add("PersonDto.FoodMoneyShare should not be negative.");
            }

            if (personDto.Id != 0) // trying to send an existing person
            {
                // this should not throw an error (right now).
                personDto.Id = 0;
            }


            return (isValid, errors);
        }
    }
}
