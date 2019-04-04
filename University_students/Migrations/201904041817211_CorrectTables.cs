namespace University_students.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Faculties", "UniversityId", "dbo.Universities");
            DropIndex("dbo.Faculties", new[] { "UniversityId" });
            AlterColumn("dbo.Faculties", "UniversityId", c => c.Int(nullable: false));
            CreateIndex("dbo.Faculties", "UniversityId");
            AddForeignKey("dbo.Faculties", "UniversityId", "dbo.Universities", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Faculties", "UniversityId", "dbo.Universities");
            DropIndex("dbo.Faculties", new[] { "UniversityId" });
            AlterColumn("dbo.Faculties", "UniversityId", c => c.Int());
            CreateIndex("dbo.Faculties", "UniversityId");
            AddForeignKey("dbo.Faculties", "UniversityId", "dbo.Universities", "Id");
        }
    }
}
