using MarketsTracker.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace MarketsTracker.DAL
{
    public class DatabaseUtils
    {
        public static List<SqlParameter> GetPagingParams(int page, int amount)
        {
            int from, to;
            Utils.GetPagingData(page, amount, out from, out to);
            var prams = new List<SqlParameter>
            {
                new SqlParameter("@From",from),
                new SqlParameter("@To", to)
            };
            return prams;
        }
    }
}
