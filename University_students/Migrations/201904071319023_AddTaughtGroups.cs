namespace University_students.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTaughtGroups : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TeachingGroups", "Teaching_Id", "dbo.Teachings");
            DropForeignKey("dbo.TeachingGroups", "Group_Id", "dbo.Groups");
            DropIndex("dbo.TeachingGroups", new[] { "Teaching_Id" });
            DropIndex("dbo.TeachingGroups", new[] { "Group_Id" });
            CreateTable(
                "dbo.TaughtGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubjectId = c.Int(),
                        GroupId = c.Int(),
                        TeachingId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.Subjects", t => t.SubjectId)
                .ForeignKey("dbo.Teachings", t => t.TeachingId)
                .Index(t => t.SubjectId)
                .Index(t => t.GroupId)
                .Index(t => t.TeachingId);
            
            DropTable("dbo.TeachingGroups");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TeachingGroups",
                c => new
                    {
                        Teaching_Id = c.Int(nullable: false),
                        Group_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Teaching_Id, t.Group_Id });
            
            DropForeignKey("dbo.TaughtGroups", "TeachingId", "dbo.Teachings");
            DropForeignKey("dbo.TaughtGroups", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.TaughtGroups", "GroupId", "dbo.Groups");
            DropIndex("dbo.TaughtGroups", new[] { "TeachingId" });
            DropIndex("dbo.TaughtGroups", new[] { "GroupId" });
            DropIndex("dbo.TaughtGroups", new[] { "SubjectId" });
            DropTable("dbo.TaughtGroups");
            CreateIndex("dbo.TeachingGroups", "Group_Id");
            CreateIndex("dbo.TeachingGroups", "Teaching_Id");
            AddForeignKey("dbo.TeachingGroups", "Group_Id", "dbo.Groups", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TeachingGroups", "Teaching_Id", "dbo.Teachings", "Id", cascadeDelete: true);
        }
    }
}
