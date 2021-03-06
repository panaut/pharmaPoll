﻿using Questionnaire.Data.Model;
using System.Collections.Generic;

namespace Questionnaire.Data
{
    public interface ISurveyManagement
    {
        IEnumerable<Survey> GetAllSurveys();

        Survey Find(int id);

        Survey Find(string id);

        Survey CreateSurvey(Survey survey);

        void UpdateSurvey(Survey survey);

        void ActivatePoll(int pollId);

        void DeactivatePoll(int pollId);

        void DeleteSurvey(int pollId);
    }
}
