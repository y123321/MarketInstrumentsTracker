using MarketsTracker.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketsTracker.Common
{
    public interface IInstrumentsRepository
    {
        Task<ICollection<Instrument>> GetAllInstruments();
        Task<Instrument> GetInstrument(int id);
        Task<ICollection<Instrument>> GetUserInstruments(int userId);
        Task UpdateUserInstruments(int userId,ICollection<int> instrumentIds);
    }
}