namespace University_students.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPulpitTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pulpits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        FacultyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Faculties", t => t.FacultyId, cascadeDelete: true)
                .Index(t => t.FacultyId);
            
            AddColumn("dbo.Users", "Pulpit_Id", c => c.Int());
            CreateIndex("dbo.Users", "Pulpit_Id");
            AddForeignKey("dbo.Users", "Pulpit_Id", "dbo.Pulpits", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Pulpit_Id", "dbo.Pulpits");
            DropForeignKey("dbo.Pulpits", "FacultyId", "dbo.Faculties");
            DropIndex("dbo.Users", new[] { "Pulpit_Id" });
            DropIndex("dbo.Pulpits", new[] { "FacultyId" });
            DropColumn("dbo.Users", "Pulpit_Id");
            DropTable("dbo.Pulpits");
        }
    }
}
