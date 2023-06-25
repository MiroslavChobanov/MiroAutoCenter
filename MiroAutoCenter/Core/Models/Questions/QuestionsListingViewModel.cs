using MiroAutoCenter.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MiroAutoCenter.Core.Models.Questions
{
    public class QuestionsListingViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public Guid? ReplyId { get; set; }
        [ForeignKey(nameof(ReplyId))]
        public Reply Reply { get; set; }
        public string Username { get; set; }
    }
}
