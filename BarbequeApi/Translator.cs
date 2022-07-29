using BarbequeApi.Models;
using BarbequeApi.Models.Dtos;
using System.Collections.Generic;

namespace BarbequeApi
{
    public static class Translator
    {
        public static Barbeque ToBarbeque(BarbequeDto barbequeDto)
        {
            var barbeque = new Barbeque
            {
                Id = barbequeDto.Id,
                Title = barbequeDto.Title,
                Date = barbequeDto.Date.GetValueOrDefault(),
                Persons = ToPersons(barbequeDto.Persons),
                Notes = barbequeDto.Notes
            };

            return barbeque;
        }

        private static List<Person> ToPersons(List<PersonDto> personsDto)
        {
            var persons = new List<Person>();

            if (personsDto == null)
            {
                return persons;
            }

            foreach (var personDto in personsDto)
            {
                persons.Add(ToPerson(personDto));
            }

            return persons;
        }

        public static Person ToPerson(PersonDto personDto)
        {
            var person = new Person
            {
                Id = personDto.Id,
                Name = personDto.Name,
                FoodMoneyShare = personDto.FoodMoneyShare,
                BeverageMoneyShare = personDto.BeverageMoneyShare,
                BarbequeId = personDto.BarbequeId
            };

            return person;

        }

        public static BarbequeDto ToBarbequeDto(Barbeque barbeque)
        {
            var barbequeDto = new BarbequeDto
            {
                Id = barbeque.Id,
                Title = barbeque.Title,
                Date = barbeque.Date,
                Persons = ToPersonsDto(barbeque.Persons),
                Notes = barbeque.Notes
            };

            return barbequeDto;
        }

        private static List<PersonDto> ToPersonsDto(ICollection<Person> persons)
        {
            var personsDto = new List<PersonDto>();

            foreach (var person in persons)
            {
                personsDto.Add(ToPersonDto(person));
            }

            return personsDto;
        }

        private static PersonDto ToPersonDto(Person person)
        {
            var personDto = new PersonDto
            {
                Id = person.Id,
                Name = person.Name,
                FoodMoneyShare = person.FoodMoneyShare,
                BeverageMoneyShare = person.BeverageMoneyShare,
                BarbequeId = person.BarbequeId
            };

            return personDto;
        }
    }
}
