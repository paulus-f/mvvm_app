namespace University_students.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGroupsToTeachers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "GroupId", "dbo.Groups");
            AddColumn("dbo.Users", "Group_Id", c => c.Int());
            AddColumn("dbo.Users", "Group_Id1", c => c.Int());
            AddColumn("dbo.Groups", "User_Id", c => c.Int());
            CreateIndex("dbo.Users", "Group_Id");
            CreateIndex("dbo.Users", "Group_Id1");
            CreateIndex("dbo.Groups", "User_Id");
            AddForeignKey("dbo.Users", "Group_Id1", "dbo.Groups", "Id");
            AddForeignKey("dbo.Groups", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Users", "Group_Id", "dbo.Groups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.Groups", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Group_Id1", "dbo.Groups");
            DropIndex("dbo.Groups", new[] { "User_Id" });
            DropIndex("dbo.Users", new[] { "Group_Id1" });
            DropIndex("dbo.Users", new[] { "Group_Id" });
            DropColumn("dbo.Groups", "User_Id");
            DropColumn("dbo.Users", "Group_Id1");
            DropColumn("dbo.Users", "Group_Id");
            AddForeignKey("dbo.Users", "GroupId", "dbo.Groups", "Id");
        }
    }
}
