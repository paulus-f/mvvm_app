namespace University_students.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWorkOut : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkOuts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsWorkOut = c.Boolean(nullable: false),
                        SubjectProgressId = c.Int(),
                        Reason = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubjectProgresses", t => t.SubjectProgressId)
                .Index(t => t.SubjectProgressId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkOuts", "SubjectProgressId", "dbo.SubjectProgresses");
            DropIndex("dbo.WorkOuts", new[] { "SubjectProgressId" });
            DropTable("dbo.WorkOuts");
        }
    }
}
