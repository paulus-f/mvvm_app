namespace University_students.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddСertification : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Сertification",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false, storeType: "date"),
                        EndDate = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Universities", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Сertification", "Id", "dbo.Universities");
            DropIndex("dbo.Сertification", new[] { "Id" });
            DropTable("dbo.Сertification");
        }
    }
}
