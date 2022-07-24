﻿using BarbequeApi.Models;
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
                Title = barbequeDto.Title,
                Date = barbequeDto.Date,
                Persons = ToPersons(barbequeDto.Persons), 
                Notes = barbequeDto.Notes
            };

            return barbeque;
        }

        private static List<Person> ToPersons(List<PersonDto> personsDto)
        {
            var persons = new List<Person>();

            foreach (var personDto in personsDto)
            {
                persons.Add(ToPerson(personDto));
            }

            return persons;
        }

        private static Person ToPerson(PersonDto personDto)
        {
            var person = new Person
            {
                Name = personDto.Name,
                FoodsMoney = personDto.FoodsMoney,
                DrinksMoney = personDto.DrinksMoney
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
                personsDto.Add(ToPerson(person));
            }

            return personsDto;
        }

        private static PersonDto ToPerson(Person person)
        {
            var personDto = new PersonDto
            {
                Id = person.Id,
                Name = person.Name,
                FoodsMoney = person.FoodsMoney,
                DrinksMoney = person.DrinksMoney
            };

            return personDto;
        }
    }
}
