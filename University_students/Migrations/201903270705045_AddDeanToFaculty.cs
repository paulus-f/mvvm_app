namespace University_students.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeanToFaculty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Faculties", "Dean", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Faculties", "Dean");
        }
    }
}
