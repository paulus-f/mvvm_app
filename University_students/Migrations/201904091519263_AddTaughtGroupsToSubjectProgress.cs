namespace University_students.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTaughtGroupsToSubjectProgress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubjectProgresses", "TaughtGroupsId", c => c.Int(nullable: false));
            CreateIndex("dbo.SubjectProgresses", "TaughtGroupsId");
            AddForeignKey("dbo.SubjectProgresses", "TaughtGroupsId", "dbo.TaughtGroups", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubjectProgresses", "TaughtGroupsId", "dbo.TaughtGroups");
            DropIndex("dbo.SubjectProgresses", new[] { "TaughtGroupsId" });
            DropColumn("dbo.SubjectProgresses", "TaughtGroupsId");
        }
    }
}
