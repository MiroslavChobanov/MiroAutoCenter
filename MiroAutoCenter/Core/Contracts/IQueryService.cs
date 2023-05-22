namespace MiroAutoCenter.Core.Contracts
{
    public interface IQueryService
    {
        public Guid AddNewQueryLog(
            string queryName,
            TimeSpan queryTime,
            string queryType
            );
    }
}
