namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingRequiredAttriInClub : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Clubs", "StudentId", "dbo.Students");
            DropIndex("dbo.Clubs", new[] { "StudentId" });
            AlterColumn("dbo.Clubs", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Clubs", "StudentId", c => c.String(nullable: false, maxLength: 14));
            CreateIndex("dbo.Clubs", "StudentId");
            AddForeignKey("dbo.Clubs", "StudentId", "dbo.Students", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clubs", "StudentId", "dbo.Students");
            DropIndex("dbo.Clubs", new[] { "StudentId" });
            AlterColumn("dbo.Clubs", "StudentId", c => c.String(maxLength: 14));
            AlterColumn("dbo.Clubs", "Name", c => c.String());
            CreateIndex("dbo.Clubs", "StudentId");
            AddForeignKey("dbo.Clubs", "StudentId", "dbo.Students", "Id");
        }
    }
}
