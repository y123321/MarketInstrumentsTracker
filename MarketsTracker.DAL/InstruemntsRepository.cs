using MarketsTracker.Common;
using MarketsTracker.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MarketsTracker.DAL
{
    public class InstrumentsRepository : IInstrumentsRepository
    {
        string _connectionString;
        public InstrumentsRepository(DatabaseOptions databaseOptions)
        {
            _connectionString = databaseOptions.ConnectionString;
        }

        public async Task<ICollection<Instrument>> GetAllInstruments(int page = 1, int amount = 100)
        {

            var sql = @"SELECT * 
                        FROM
                        (
                            SELECT i.instrumentId,i.name,i.symbol,i.instrumentType, ROW_NUMBER() OVER (ORDER BY i.name) AS RowNum
                            FROM instrument i
                        ) a
                        WHERE RowNum>=@From 
                            AND RowNum<@To
                        ORDER BY RowNum";
            var prams = DatabaseUtils.GetPagingParams(page, amount);
            var instruments = await ExecuteInstrumentsQuery(sql, prams.ToArray());
            return instruments;
        }



        public async Task<Instrument> GetInstrument(int id, int page = 1, int amount = 100)
        {
            var sql = @"
                        SELECT * 
                        FROM
                        (
                            SELECT i.instrumentId,i.name,i.symbol,i.instrumentType, ROW_NUMBER() OVER (ORDER BY i.name) AS RowNum
                            FROM instrument i
                            WHERE i.instrumentId = @InstrumentId
                        ) a
                        WHERE RowNum>=@From 
                            AND RowNum<@To
                        ORDER BY RowNum"; 
            var param = new SqlParameter("@InstrumentId", id);
            var pagePrams = DatabaseUtils.GetPagingParams(page, amount);
            pagePrams.Add(param);
            var instruments = await ExecuteInstrumentsQuery(sql, pagePrams.ToArray());
            return instruments.FirstOrDefault();
        }

        public async Task<ICollection<Instrument>> GetUserInstruments(int userId, int page, int amount)
        {
            var sql = @"
                        SELECT * 
                        FROM
                        (
                            SELECT i.instrumentId,i.name,i.symbol,i.instrumentType, ROW_NUMBER() OVER (ORDER BY i.name) AS RowNum
                            FROM instrument i
                            JOIN userInstrument iu
                            ON i.instrumentId = iu.instrumentId 
                            WHERE iu.userId = @UserId
                        ) a
                        WHERE RowNum>=@From 
                            AND RowNum<@To
                        ORDER BY RowNum";
            var param = new SqlParameter("@UserId", userId);
            var pagePrams = DatabaseUtils.GetPagingParams(page, amount);
            pagePrams.Add(param);
            Instrument[] instruments = await ExecuteInstrumentsQuery(sql, pagePrams.ToArray());
            return instruments;
        }


        public async Task UpdateUserInstruments(int userId, ICollection<int> instrumentIds)
        {
            var valuesPerStatementLimit = 1000;
            var groups = instrumentIds.ToArray().Split(valuesPerStatementLimit);
            var sb = new StringBuilder();
            foreach (var grp in groups)
            {
                var batch = @"
                              INSERT INTO userInstrument(userId, instrumentId) 
                              VALUES" + Environment.NewLine;
                var values = grp.Select(instrumentId => $"({userId},{instrumentId}),");
                batch += string.Join(Environment.NewLine, values).TrimEnd(',') + ";";
                sb.AppendLine(batch);
            }
            var sql = $@"
                        
                        BEGIN TRAN
                            BEGIN TRY
                                DELETE FROM userInstrument WHERE userId = @UserId;
                                {sb.ToString()}
                            END TRY
                            BEGIN CATCH
                                ROLLBACK;
                                THROW;
                            END CATCH
                        COMMIT;";
            var pram = new SqlParameter("@UserId", userId);
            await QueryExecuter.ExecuteNoQuery(sql, _connectionString, pram);
        }


        private async Task<Instrument[]> ExecuteInstrumentsQuery(string sql, params SqlParameter[] param)
        {
            var entities = await QueryExecuter.ExecuteCollection(sql, Mappers.MapToInstrumentEntity, _connectionString, param);
            var instruments = entities.Select(Mappers.MapToInstrumentDto).ToArray();
            return instruments;
        }
    }
}
