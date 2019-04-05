namespace University_students.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTeaching : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teachings",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.TeachingGroups",
                c => new
                    {
                        Teaching_Id = c.Int(nullable: false),
                        Group_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Teaching_Id, t.Group_Id })
                .ForeignKey("dbo.Teachings", t => t.Teaching_Id, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.Group_Id, cascadeDelete: true)
                .Index(t => t.Teaching_Id)
                .Index(t => t.Group_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teachings", "Id", "dbo.Users");
            DropForeignKey("dbo.TeachingGroups", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.TeachingGroups", "Teaching_Id", "dbo.Teachings");
            DropIndex("dbo.TeachingGroups", new[] { "Group_Id" });
            DropIndex("dbo.TeachingGroups", new[] { "Teaching_Id" });
            DropIndex("dbo.Teachings", new[] { "Id" });
            DropTable("dbo.TeachingGroups");
            DropTable("dbo.Teachings");
        }
    }
}
