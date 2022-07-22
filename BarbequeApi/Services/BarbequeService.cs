using BarbequeApi.Models;
using BarbequeApi.Models.Dtos;
using BarbequeApi.Repositories;

namespace BarbequeApi.Services
{
    public interface IBarbequeService
    {
        void Create(BarbequeDto barbequeDto);
        BarbequeDto Get(long barbequeId);
    }

    public class BarbequeService : IBarbequeService
    {
        private readonly IBarbequeRepository barbequeRepository;

        public BarbequeService(IBarbequeRepository barbequeRepository)
        {
            this.barbequeRepository = barbequeRepository;
        }

        public void Create(BarbequeDto barbequeDto)
        {
            // Validator.Validate(barbequeDto);? // should I call/create it?

            var barbeque = Translator.ToBarbeque(barbequeDto);

            bool successful = barbequeRepository.Save(barbeque);

            if (!successful)
            {
                // error handler. should I throw?
            }
        }

        public BarbequeDto Get(long barbequeId)
        {
            if(barbequeId <= 0)
            {
                // error handler. should I throw?
            }

            var barbeque = barbequeRepository.Get(barbequeId);

            if(barbeque == null)
            {
                return null;
            }

            var barbequeDto = Translator.ToBarbequeDto(barbeque);

            return barbequeDto;

        }
    }
}
