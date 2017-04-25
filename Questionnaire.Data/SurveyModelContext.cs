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

        public DbSet<QuestionBase> Questions { get; set; }

        public DbSet<SurveyTrigger> SurveyTriggers { get; set; }

        public DbSet<SurveyValidator> SurveyValidators { get; set; }

        public DbSet<MatrixRow> MatrixRows { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Page>().HasMany(page => page.elements).WithOptional(el => el.Page).WillCascadeOnDelete(true);
            //modelBuilder.Entity<ComplexMatrixBase>().HasMany(matrix => matrix.columns).WithOptional(el => el.ComplexMatrixBase).WillCascadeOnDelete(true);
            //modelBuilder.Entity<PlainMatrix>().HasMany(matrix => matrix.columns).WithOptional(el => el.PlainMatrix).WillCascadeOnDelete(true);
            //modelBuilder.Entity<Rating>().HasMany(matrix => matrix.choices).WithOptional(ch => ch.Rating).WillCascadeOnDelete(true);
            //modelBuilder.Entity<SelectQuestionBase>().HasMany(matrix => matrix.choices).WithOptional(ch => ch.Question).WillCascadeOnDelete(true);
            //modelBuilder.Entity<MultipleText>().HasMany(mt => mt.items).WithRequired(mti => mti.MultipleTextQuestion).WillCascadeOnDelete(true);
        }
    }
}
