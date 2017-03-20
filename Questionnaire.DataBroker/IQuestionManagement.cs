using Questionnaire.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.DataBroker
{
    public interface IQuestionManagement
    {
        IEnumerable<QuestionTemplate> GetAllQuestionsForPoll(int pollId, bool includeAnswers = true);

        IEnumerable<QuestionTemplate> GetAllQuestionsForSection(int sectionId, bool includeAnswers = true);

        QuestionTemplate Find(int questionId, bool includeAnswers = true);

        QuestionTemplate CreateQuestion(
            int sectionId, 
            string nativeText,
            EQuestionType questionType, 
            EAppearanceType appearanceType, 
            int? displayOrder = null);

        void ChangeText(int questionId, string nativeText);

        void ChangeQuestionType(int questionId, EQuestionType type);

        void ChangeAppearanceType(int questionId, EAppearanceType type);

        void ChangeDisplayOrder(int questionId, int newValue);

        void DeleteQuestion(int questionId);
    }
}
