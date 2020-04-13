using MarketsTracker.Common;
using MarketsTracker.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketsTracker.Services
{
    public class InstrumentsService : IInstrumentsService
    {
        private readonly IInstrumentsRepository _instrumentsRepository;

        public InstrumentsService(IInstrumentsRepository instrumentsRepository)
        {
            _instrumentsRepository = instrumentsRepository;
        }

        public async Task<ICollection<Instrument>> GetAllInstruments(int page, int amount) => await _instrumentsRepository.GetAllInstruments(page, amount);

        public async Task<Instrument> GetInstrument(int id, int page, int amount) => await _instrumentsRepository.GetInstrument(id, page, amount);

        public async Task<ICollection<Instrument>> GetUserInstruments(int userId, int page, int amount) => await _instrumentsRepository.GetUserInstruments(userId, page, amount);

        public async Task UpdateUserInstruments(int userId, ICollection<int> instruments) => await _instrumentsRepository.UpdateUserInstruments(userId, instruments);
    }
}
