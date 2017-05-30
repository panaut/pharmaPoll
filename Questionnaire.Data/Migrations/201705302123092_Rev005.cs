namespace Questionnaire.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rev005 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VotingSessions", "LastPageVisited", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.VotingSessions", "LastPageVisited");
        }
    }
}
