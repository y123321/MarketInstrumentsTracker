using MarketsTracker.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketsTracker.Common
{
    public interface IInstrumentsService
    {
        Task<ICollection<Instrument>> GetUserInstruments(int userId);
        Task<Instrument> GetInstrument(int id);
        Task<ICollection<Instrument>> GetAllInstruments();
        Task UpdateUserInstruments(int userId, ICollection<int> instruments);
    }
}