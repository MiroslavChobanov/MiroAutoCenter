using MiroAutoCenter.Data.Models;

namespace MiroAutoCenter.Core.Contracts
{
    public interface IReplyService
    {
        public Guid AddQuestion(Guid questionId, string content);
    }
}
