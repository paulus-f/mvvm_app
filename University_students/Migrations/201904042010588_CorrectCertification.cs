namespace University_students.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectCertification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Сertification", "FirstAutumnStartDate", c => c.DateTime(nullable: false, storeType: "date"));
            AddColumn("dbo.Сertification", "FirstAutumnEndDate", c => c.DateTime(nullable: false, storeType: "date"));
            AddColumn("dbo.Сertification", "LastAutumnStartDate", c => c.DateTime(nullable: false, storeType: "date"));
            AddColumn("dbo.Сertification", "LastAutumnEndDate", c => c.DateTime(nullable: false, storeType: "date"));
            AddColumn("dbo.Сertification", "FirstSpringStartDate", c => c.DateTime(nullable: false, storeType: "date"));
            AddColumn("dbo.Сertification", "FirstSpringEndDate", c => c.DateTime(nullable: false, storeType: "date"));
            AddColumn("dbo.Сertification", "LastSpringStartDate", c => c.DateTime(nullable: false, storeType: "date"));
            AddColumn("dbo.Сertification", "LastSpringEndDate", c => c.DateTime(nullable: false, storeType: "date"));
            DropColumn("dbo.Сertification", "StartDate");
            DropColumn("dbo.Сertification", "EndDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Сertification", "EndDate", c => c.DateTime(nullable: false, storeType: "date"));
            AddColumn("dbo.Сertification", "StartDate", c => c.DateTime(nullable: false, storeType: "date"));
            DropColumn("dbo.Сertification", "LastSpringEndDate");
            DropColumn("dbo.Сertification", "LastSpringStartDate");
            DropColumn("dbo.Сertification", "FirstSpringEndDate");
            DropColumn("dbo.Сertification", "FirstSpringStartDate");
            DropColumn("dbo.Сertification", "LastAutumnEndDate");
            DropColumn("dbo.Сertification", "LastAutumnStartDate");
            DropColumn("dbo.Сertification", "FirstAutumnEndDate");
            DropColumn("dbo.Сertification", "FirstAutumnStartDate");
        }
    }
}
