using MiroAutoCenter.Core.Models.Questions;
using MiroAutoCenter.Data.Models;

namespace MiroAutoCenter.Core.Contracts
{
    public interface IQuestionService
    {
        public Guid AddQuestion(string content, string userId);
        public bool DeleteQuestion(Guid id, bool isDeleted);
        public List<QuestionsListingViewModel> All();
    }
}
