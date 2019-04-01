namespace University_students.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeFix : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.TeacherSubjects", "SubjectId", "dbo.Subjects");
            //DropForeignKey("dbo.TeacherSubjects", "UserId", "dbo.Users");
            //DropIndex("dbo.TeacherSubjects", new[] { "UserId" });
            //DropIndex("dbo.TeacherSubjects", new[] { "SubjectId" });
            //DropTable("dbo.TeacherSubjects");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TeacherSubjects",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.SubjectId });
            
            CreateIndex("dbo.TeacherSubjects", "SubjectId");
            CreateIndex("dbo.TeacherSubjects", "UserId");
            AddForeignKey("dbo.TeacherSubjects", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TeacherSubjects", "SubjectId", "dbo.Subjects", "Id", cascadeDelete: true);
        }
    }
}
