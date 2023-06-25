using MiroAutoCenter.Core.Contracts;
using MiroAutoCenter.Core.Models.Questions;
using MiroAutoCenter.Core.Models.Services;
using MiroAutoCenter.Data;
using MiroAutoCenter.Data.Models;

namespace MiroAutoCenter.Core.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly MiroAutoCenterDbContext data;

        public QuestionService(MiroAutoCenterDbContext data)
        {
            this.data = data;
        }
        public Guid AddQuestion(string content, string userId)
        {
            var newQuestion = new Question
            {
                Content = content,
                CreatedAt= DateTime.Now,
                IsDeleted= false,
                UserId = userId
            };

            this.data.Questions.Add(newQuestion);
            this.data.SaveChanges();

            return newQuestion.Id;
        }

        public bool DeleteQuestion(Guid id, bool isDeleted)
        {
            var questionData = this.data.Questions.Find(id);

            if (questionData == null)
            {
                return false;
            }

            questionData.IsDeleted = isDeleted;

            this.data.SaveChanges();

            return true;
        }

        public List<QuestionsListingViewModel> All()
        {
            return this.data
                 .Questions
                 .Where(x => !x.IsDeleted)
                 .Select(x => new QuestionsListingViewModel
                 {
                     Id = x.Id,
                      Content = x.Content,
                      CreatedAt = x.CreatedAt,
                      Reply = x.Reply,
                      ReplyId = x.ReplyId,
                 })
                 .OrderBy(x => x.CreatedAt)
                 .ToList();
        }
    }
}
