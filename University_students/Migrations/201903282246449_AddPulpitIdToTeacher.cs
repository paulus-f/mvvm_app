namespace University_students.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPulpitIdToTeacher : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Users", name: "Pulpit_Id", newName: "PulpitId");
            RenameIndex(table: "dbo.Users", name: "IX_Pulpit_Id", newName: "IX_PulpitId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Users", name: "IX_PulpitId", newName: "IX_Pulpit_Id");
            RenameColumn(table: "dbo.Users", name: "PulpitId", newName: "Pulpit_Id");
        }
    }
}
