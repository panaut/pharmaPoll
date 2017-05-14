using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questionnaire.Data.Model.Results
{
    public class VotingSession
    {
        public int Id { get; set; }

        public int SurveyId { get; set; }

        public Survey Survey { get; set; }

        public bool IsCompleted { get; set; }

        [MaxLength(15)]
        public string ClientIP { get; set; }

        [MaxLength(128)]
        [Index(IsUnique = true)]
        public string Code { get; set; }

        public DateTime InitializationTime { get; set; }

        public DateTime LastSubmissionTime { get; set; }

        public int NumberOfTimesSubmitted { get; set; }

        public string JsonData { get; set; }
    }
}
