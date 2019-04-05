namespace University_students.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableSpeciality : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Specialities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 450),
                        Code = c.String(nullable: false),
                        Qualification = c.String(nullable: false),
                        FacultyId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Faculties", t => t.FacultyId)
                .Index(t => t.Name, unique: true)
                .Index(t => t.FacultyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Specialities", "FacultyId", "dbo.Faculties");
            DropIndex("dbo.Specialities", new[] { "FacultyId" });
            DropIndex("dbo.Specialities", new[] { "Name" });
            DropTable("dbo.Specialities");
        }
    }
}
