using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Questionnaire.DataBroker
{
    public interface IAnswerManagement
    {
        IEnumerable<AnswerTemplate> GetAllAnswersForQuestion(int questionId);

        AnswerTemplate Find(int answerId);

        QuestionTemplate CreateAnswer(
            int questionId,
            EAnswerType type,
            string text,
            int? displayOrder = null,
            int actualType = 0,
            Regex validationExpression = null);

        void ChangeActualType(int actualType = 0, Regex validationExpression = null);

        void ChangeValue(int answerId, EAnswerType type, string text);

        void DeleteAnswer(int answerId);

        void ChangeDisplayOrder(int answerId, int newValue);
    }
}
