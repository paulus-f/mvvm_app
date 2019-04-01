namespace University_students.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectAssociationsInSubjectAndUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SubjectUsers",
                c => new
                    {
                        Subject_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Subject_Id, t.User_Id })
                .ForeignKey("dbo.Subjects", t => t.Subject_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Subject_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubjectUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.SubjectUsers", "Subject_Id", "dbo.Subjects");
            DropIndex("dbo.SubjectUsers", new[] { "User_Id" });
            DropIndex("dbo.SubjectUsers", new[] { "Subject_Id" });
            DropTable("dbo.SubjectUsers");
        }
    }
}
