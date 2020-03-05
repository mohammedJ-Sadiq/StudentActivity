namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixingClubProgramRelationship : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Programs", new[] { "Club_Id" });
            RenameColumn(table: "dbo.Programs", name: "Club_Id", newName: "ClubId");
            AlterColumn("dbo.Programs", "ClubId", c => c.Int(nullable: false));
            CreateIndex("dbo.Programs", "ClubId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Programs", new[] { "ClubId" });
            AlterColumn("dbo.Programs", "ClubId", c => c.Int());
            RenameColumn(table: "dbo.Programs", name: "ClubId", newName: "Club_Id");
            CreateIndex("dbo.Programs", "Club_Id");
        }
    }
}
