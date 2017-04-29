using Questionnaire.Data.Model;
using Questionnaire.Data.Model.QuestionDefinition;
using System.Data.Entity;

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

        public DbSet<Choice> Choices { get; set; }

        public DbSet<MatrixRow> MatrixRows { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
