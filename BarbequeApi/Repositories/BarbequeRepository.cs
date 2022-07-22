using BarbequeApi.Models;

namespace BarbequeApi.Repositories
{
    public interface IBarbequeRepository
    {
        bool Save(Barbeque barbeque);
        Barbeque Get(long barbequeId);
    }

    public class BarbequeRepository : IBarbequeRepository
    {
        public Barbeque Get(long barbequeId)
        {
            throw new NotImplementedException();
        }

        public bool Save(Barbeque barbeque)
        {
            throw new NotImplementedException();
        }
    }
}
