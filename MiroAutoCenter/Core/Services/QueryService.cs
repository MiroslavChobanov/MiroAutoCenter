using MiroAutoCenter.Core.Contracts;
using MiroAutoCenter.Data;
using MiroAutoCenter.Data.Models;

namespace MiroAutoCenter.Core.Services
{
    public class QueryService : IQueryService
    {
        private readonly MiroAutoCenterDbContext data;

        public QueryService(MiroAutoCenterDbContext data)
        {
            this.data = data;
        }
        public Guid AddNewQueryLog(
            string queryName,
            TimeSpan queryTime,
            string queryType
            )
        {
            var newQueryLog = new QueryLog
            {
                QueryName = queryName,
                ExecutionTime = queryTime,
                QueryType = queryType
            };

            this.data.QueryLogs.Add(newQueryLog);
            this.data.SaveChanges();

            return newQueryLog.Id;
        }
    }
}
