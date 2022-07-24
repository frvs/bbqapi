using BarbequeApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BarbequeApi.Repositories
{
    public interface IBarbequeRepository
    {
        bool Save(Barbeque barbeque);
        Barbeque Get(long barbequeId);
    }

    public class BarbequeRepository : IBarbequeRepository
    {
        private readonly DataContext context;

        public BarbequeRepository(DataContext context)
        {
            this.context = context;
        }

        public Barbeque Get(long barbequeId)
        {
            return context.Barbeques.Include(b => b.Persons).FirstOrDefault(barbeque => barbeque.Id == barbequeId);
        }

        public bool Save(Barbeque barbeque)
        {
            context.Barbeques.Add(barbeque);

            return context.SaveChanges() > 0;
        }
    }
}
