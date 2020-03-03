namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingClubFkAgain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Programs", "Club_Id", c => c.Int());
            CreateIndex("dbo.Programs", "Club_Id");
            AddForeignKey("dbo.Programs", "Club_Id", "dbo.Clubs", "Id");
            DropColumn("dbo.Programs", "ClubId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Programs", "ClubId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Programs", "Club_Id", "dbo.Clubs");
            DropIndex("dbo.Programs", new[] { "Club_Id" });
            DropColumn("dbo.Programs", "Club_Id");
        }
    }
}
