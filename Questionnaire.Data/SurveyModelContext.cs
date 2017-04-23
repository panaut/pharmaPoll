using Questionnaire.Data.Model;
using Questionnaire.Data.Model.QuestionDefinition;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.Data
{
    public class SurveyModelContext : DbContext
    {

        public SurveyModelContext() 
            : base(System.Configuration.ConfigurationManager.ConnectionStrings["SurveyModelContainer"].ConnectionString)
        { 

        }

        public DbSet<Survey> Surveys { get; set; }

        public DbSet<Page> Pages{ get; set; }

        public DbSet<QuestionBase> Questions { get; set; }

        public DbSet<SurveyTrigger> SurveyTriggers { get; set; }

        public DbSet<SurveyValidator> SurveyValidators { get; set; }

        public DbSet<MatrixRow> MatrixRows { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
