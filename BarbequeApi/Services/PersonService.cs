using BarbequeApi.Models;
using BarbequeApi.Models.Dtos;
using BarbequeApi.Repositories;
using System;
using System.Linq;

namespace BarbequeApi.Services
{
    public interface IPersonService
    {
        void Create(long barbequeId, PersonDto personDto);
        void Delete(long barbequeId, long personId);
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

        public void Create(long barbequeId, PersonDto personDto)
        {
            if(barbequeId <= 0)
            {
                return;
            }

            var barbeque = barbequeRepository.Get(barbequeId);

            if(barbeque == null)
            {
                return;
            }

            if(personDto.Id != 0 && barbeque.Persons.Select(p => p.Id).Contains(personDto.Id))
            {
                // in this case, the person already exists in the bbq. 
                // it should be a put/update. what should I do now?
                // err;
                return;
            }

            var person = Translator.ToPerson(personDto);
            person.BarbequeId = barbequeId;
            FillDefaultValuesIfNecessary(person, barbeque);

            bool succesful = repository.Save(person);

            if (!succesful)
            {
                throw new Exception("Error in Create");
            }
        }

        public void Delete(long barbequeId, long personId)
        {
            if (barbequeId <= 0 || personId <= 0)
            {
                // err
                return;
            }

            var barbeque = barbequeRepository.Get(barbequeId);

            if (barbeque == null)
            {
                // err
                return;
            }

            if (barbeque.Persons.FirstOrDefault(p => p.Id == personId) == null)
            {
                // err
                return;
            }

            bool succesful = repository.Delete(barbeque, personId);

            if(!succesful)
            {
                throw new Exception("Error in Delete");
            }
        }

        private void FillDefaultValuesIfNecessary(Person person, Barbeque barbequeDto)
        {
            person.Name = string.IsNullOrWhiteSpace(person.Name) ? "Joao Doe" : person.Name;

            if (person.FoodsMoney == 0 && person.DrinksMoney == 0)
            {
                if (barbequeDto.Persons.Count == 0)
                {
                    person.FoodsMoney = 20;
                    person.DrinksMoney = 10;
                }
                else
                {
                    person.FoodsMoney = Math.Floor(barbequeDto.Persons.Select(p => p.FoodsMoney).Sum() / barbequeDto.Persons.Count);
                    person.DrinksMoney = Math.Floor(barbequeDto.Persons.Select(p => p.DrinksMoney).Sum() / barbequeDto.Persons.Count);
                }
            }
        }
    }
}
