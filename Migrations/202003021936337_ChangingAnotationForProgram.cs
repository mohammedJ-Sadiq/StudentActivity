namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingAnotationForProgram : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Programs", "ClubId", c => c.Int(nullable: false));
            AlterColumn("dbo.Programs", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Programs", "Time", c => c.String(nullable: false));
            AlterColumn("dbo.Programs", "StartDate", c => c.String(nullable: false));
            AlterColumn("dbo.Programs", "EndDate", c => c.String(nullable: false));
            AlterColumn("dbo.Programs", "MaximumStudentNumber", c => c.String(nullable: false));
            CreateIndex("dbo.Programs", "ClubId");
            AddForeignKey("dbo.Programs", "ClubId", "dbo.Clubs", "Id", cascadeDelete: true);
            DropColumn("dbo.Programs", "ClubName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Programs", "ClubName", c => c.String());
            DropForeignKey("dbo.Programs", "ClubId", "dbo.Clubs");
            DropIndex("dbo.Programs", new[] { "ClubId" });
            AlterColumn("dbo.Programs", "MaximumStudentNumber", c => c.String());
            AlterColumn("dbo.Programs", "EndDate", c => c.String());
            AlterColumn("dbo.Programs", "StartDate", c => c.String());
            AlterColumn("dbo.Programs", "Time", c => c.String());
            AlterColumn("dbo.Programs", "Title", c => c.String());
            DropColumn("dbo.Programs", "ClubId");
        }
    }
}
