using BarbequeApi.Models;

namespace BarbequeApi.Repositories
{
    public interface IPersonRepository
    {
        bool Save(Person person);
        bool Delete(Barbeque barbeque, long personId);
    }

    public class PersonRepository : IPersonRepository
    {
        private readonly DataContext context;

        public PersonRepository(DataContext context)
        {
            this.context = context;
        }

        public bool Delete(Barbeque barbeque, long personId)
        {
            var person = context.Persons.Find(personId);
            if (person == null)
            {
                return false;
            }

            context.Persons.Remove(person);

            return context.SaveChanges() > 0;
        }

        public bool Save(Person person)
        {
            context.Persons.Add(person);
            return context.SaveChanges() > 0;

        }
    }
}
