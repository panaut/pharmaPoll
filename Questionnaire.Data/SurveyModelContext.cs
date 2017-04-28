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

        public SurveyModelContext(bool autoChangesDetect = false,
            bool lazyLoading = false,
            bool proxyEnabled = false)
            : base(System.Configuration.ConfigurationManager.ConnectionStrings["SurveyModelContainer"].ConnectionString)
        {
            this.Configuration.AutoDetectChangesEnabled = autoChangesDetect;
            this.Configuration.LazyLoadingEnabled = lazyLoading;
            this.Configuration.ProxyCreationEnabled = proxyEnabled;
        }

        public DbSet<Survey> Surveys { get; set; }

        public DbSet<Page> Pages { get; set; }

        public DbSet<ElementBase> Elements { get; set; }

        public DbSet<SurveyTrigger> SurveyTriggers { get; set; }

        public DbSet<SurveyValidator> SurveyValidators { get; set; }

        public DbSet<MatrixRow> MatrixRows { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
