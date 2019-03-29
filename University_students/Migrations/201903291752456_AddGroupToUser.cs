namespace University_students.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGroupToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "GroupId", c => c.Int());
            CreateIndex("dbo.Users", "GroupId");
            AddForeignKey("dbo.Users", "GroupId", "dbo.Groups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "GroupId", "dbo.Groups");
            DropIndex("dbo.Users", new[] { "GroupId" });
            DropColumn("dbo.Users", "GroupId");
        }
    }
}
