namespace University_students.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFacultyAndUniversityMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Faculties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 450),
                        UniversityId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Universities", t => t.UniversityId)
                .Index(t => t.Name, unique: true)
                .Index(t => t.UniversityId);
            
            CreateTable(
                "dbo.Universities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 450),
                        City = c.String(nullable: false),
                        TypeUniversity = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Faculties", "UniversityId", "dbo.Universities");
            DropIndex("dbo.Universities", new[] { "Name" });
            DropIndex("dbo.Faculties", new[] { "UniversityId" });
            DropIndex("dbo.Faculties", new[] { "Name" });
            DropTable("dbo.Universities");
            DropTable("dbo.Faculties");
        }
    }
}
