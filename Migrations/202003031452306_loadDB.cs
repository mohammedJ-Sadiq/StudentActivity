namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class loadDB : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Clubs", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.Students", "Club_Id", "dbo.Clubs");
            DropForeignKey("dbo.Clubs", "CoordinatorName_Id", "dbo.Students");
            DropIndex("dbo.Clubs", new[] { "Student_Id" });
            DropIndex("dbo.Clubs", new[] { "CoordinatorName_Id" });
            DropIndex("dbo.Students", new[] { "Club_Id" });
            DropColumn("dbo.Clubs", "StudentId");
            RenameColumn(table: "dbo.Clubs", name: "CoordinatorName_Id", newName: "StudentId");
            AlterColumn("dbo.Clubs", "StudentId", c => c.String(nullable: false, maxLength: 14));
            AlterColumn("dbo.Clubs", "StudentId", c => c.String(nullable: false, maxLength: 14));
            CreateIndex("dbo.Clubs", "StudentId");
            AddForeignKey("dbo.Clubs", "StudentId", "dbo.Students", "Id", cascadeDelete: true);
            DropColumn("dbo.Clubs", "Student_Id");
            DropColumn("dbo.Students", "Club_Id");
        }

        public override void Down()
        {
        }
    }
}
