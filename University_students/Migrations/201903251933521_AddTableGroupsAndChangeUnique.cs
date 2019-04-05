namespace University_students.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableGroupsAndChangeUnique : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Faculties", new[] { "Name" });
            DropIndex("dbo.Specialities", new[] { "Name" });
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumberGroup = c.Int(nullable: false),
                        FirstYear = c.Int(nullable: false),
                        SpecialityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specialities", t => t.SpecialityId, cascadeDelete: true)
                .Index(t => t.SpecialityId);
            
            AlterColumn("dbo.Faculties", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Specialities", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Groups", "SpecialityId", "dbo.Specialities");
            DropIndex("dbo.Groups", new[] { "SpecialityId" });
            AlterColumn("dbo.Specialities", "Name", c => c.String(maxLength: 450));
            AlterColumn("dbo.Faculties", "Name", c => c.String(maxLength: 450));
            DropTable("dbo.Groups");
            CreateIndex("dbo.Specialities", "Name", unique: true);
            CreateIndex("dbo.Faculties", "Name", unique: true);
        }
    }
}
