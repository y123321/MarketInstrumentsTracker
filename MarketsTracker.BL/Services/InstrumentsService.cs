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

        public async Task<ICollection<Instrument>> GetAllInstruments() => await _instrumentsRepository.GetAllInstruments();

        public async Task<Instrument> GetInstrument(int id) => await _instrumentsRepository.GetInstrument(id);

        public async Task<ICollection<Instrument>> GetUserInstruments(int userId) => await _instrumentsRepository.GetUserInstruments(userId);

        public async Task UpdateUserInstruments(int userId,ICollection<int> instruments) => await _instrumentsRepository.UpdateUserInstruments(userId, instruments);
    }
}
