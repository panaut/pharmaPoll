namespace Questionnaire.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rev003 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Triggers_Complete",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Triggers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Triggers_SetValue",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        setToName = c.String(maxLength: 512),
                        setValue = c.String(maxLength: 512),
                        isVariable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Triggers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Triggers_Visibility",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        pages = c.String(),
                        questions = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Triggers", t => t.Id)
                .Index(t => t.Id);
            
            AddColumn("dbo.Triggers", "relation", c => c.String(maxLength: 16));
            AddColumn("dbo.Triggers", "value", c => c.String(maxLength: 512));
            AddColumn("dbo.Triggers", "name", c => c.String(maxLength: 512));
            AlterColumn("dbo.Validators", "text", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Triggers_Visibility", "Id", "dbo.Triggers");
            DropForeignKey("dbo.Triggers_SetValue", "Id", "dbo.Triggers");
            DropForeignKey("dbo.Triggers_Complete", "Id", "dbo.Triggers");
            DropIndex("dbo.Triggers_Visibility", new[] { "Id" });
            DropIndex("dbo.Triggers_SetValue", new[] { "Id" });
            DropIndex("dbo.Triggers_Complete", new[] { "Id" });
            AlterColumn("dbo.Validators", "text", c => c.String());
            DropColumn("dbo.Triggers", "name");
            DropColumn("dbo.Triggers", "value");
            DropColumn("dbo.Triggers", "relation");
            DropTable("dbo.Triggers_Visibility");
            DropTable("dbo.Triggers_SetValue");
            DropTable("dbo.Triggers_Complete");
        }
    }
}
