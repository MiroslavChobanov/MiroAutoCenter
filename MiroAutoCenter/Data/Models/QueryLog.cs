using System.ComponentModel.DataAnnotations;

namespace MiroAutoCenter.Data.Models
{
    public class QueryLog
    {
        public QueryLog()
        {
            this.Id = Guid.NewGuid();
        }
        [Key]
        [Required]
        public Guid Id { get; set; }
        public string QueryName { get; set; }
        public TimeSpan ExecutionTime { get; set; }
        public string QueryType { get; set; }
    }
}
