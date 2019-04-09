namespace University_students.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSubjectProgress : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SubjectProgresses", "IsExamPassed", c => c.Int(nullable: false));
            AlterColumn("dbo.SubjectProgresses", "IsStartCertifiationPassed", c => c.Int(nullable: false));
            AlterColumn("dbo.SubjectProgresses", "IsFinishCertifiationPassed", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SubjectProgresses", "IsFinishCertifiationPassed", c => c.Boolean(nullable: false));
            AlterColumn("dbo.SubjectProgresses", "IsStartCertifiationPassed", c => c.Boolean(nullable: false));
            AlterColumn("dbo.SubjectProgresses", "IsExamPassed", c => c.Boolean(nullable: false));
        }
    }
}
