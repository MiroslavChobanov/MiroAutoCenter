using MiroAutoCenter.Core.Contracts;
using MiroAutoCenter.Data;
using MiroAutoCenter.Data.Models;

namespace MiroAutoCenter.Core.Services
{
    public class ReplyService : IReplyService
    {
        private readonly MiroAutoCenterDbContext data;

        public ReplyService(MiroAutoCenterDbContext data)
        {
            this.data = data;
        }
        public Guid AddQuestion(Guid questionId, string content)
        {
            var newReply = new Reply
            {
                Content = content,
                CreatedAt = DateTime.Now,
                QuestionId= questionId,
            };

            this.data.Replies.Add(newReply);
            this.data.SaveChanges();

            return newReply.Id;
        }
    }
}
