using MarketsTracker.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketsTracker.Common
{
    public interface IInstrumentsRepository
    {
        Task<ICollection<Instrument>> GetAllInstruments(int page, int amount);
        Task<Instrument> GetInstrument(int id, int page, int amount);
        Task<ICollection<Instrument>> GetUserInstruments(int userId, int page, int amount);
        Task UpdateUserInstruments(int userId,ICollection<int> instrumentIds);
    }
}