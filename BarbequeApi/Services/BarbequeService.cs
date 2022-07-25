using BarbequeApi.Models;
using BarbequeApi.Models.Dtos;
using BarbequeApi.Repositories;
using System;

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
            FillDefaultValuesIfNecessary(barbeque);

            bool successful = barbequeRepository.Save(barbeque);

            if (!successful)
            {
                throw new Exception("Error in create.");
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
                throw new Exception($"Error in get barbequeId: {barbequeId}");
            }

            var barbequeDto = Translator.ToBarbequeDto(barbeque);

            return barbequeDto;
        }

        private void FillDefaultValuesIfNecessary(Barbeque barbeque)
        {
            barbeque.Title = string.IsNullOrWhiteSpace(barbeque.Title) ? "Sem motivo" : barbeque.Title;
            barbeque.Date = barbeque.Date == null || barbeque.Date == default ? DateTime.Now : barbeque.Date;
        }
    }
}
