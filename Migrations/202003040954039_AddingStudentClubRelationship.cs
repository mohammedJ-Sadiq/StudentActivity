namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingStudentClubRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Clubs", "StudentId", "dbo.Students");
            DropIndex("dbo.Clubs", new[] { "StudentId" });
            AddColumn("dbo.Clubs", "Student_Id", c => c.String(maxLength: 14));
            AddColumn("dbo.Clubs", "CoordinatorName_Id", c => c.String(maxLength: 14));
            AddColumn("dbo.Students", "Club_Id", c => c.Int());
            AlterColumn("dbo.Clubs", "StudentId", c => c.String(nullable: false));
            CreateIndex("dbo.Clubs", "Student_Id");
            CreateIndex("dbo.Clubs", "CoordinatorName_Id");
            CreateIndex("dbo.Students", "Club_Id");
            AddForeignKey("dbo.Clubs", "Student_Id", "dbo.Students", "Id");
            AddForeignKey("dbo.Students", "Club_Id", "dbo.Clubs", "Id");
            AddForeignKey("dbo.Clubs", "CoordinatorName_Id", "dbo.Students", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clubs", "CoordinatorName_Id", "dbo.Students");
            DropForeignKey("dbo.Students", "Club_Id", "dbo.Clubs");
            DropForeignKey("dbo.Clubs", "Student_Id", "dbo.Students");
            DropIndex("dbo.Students", new[] { "Club_Id" });
            DropIndex("dbo.Clubs", new[] { "CoordinatorName_Id" });
            DropIndex("dbo.Clubs", new[] { "Student_Id" });
            AlterColumn("dbo.Clubs", "StudentId", c => c.String(nullable: false, maxLength: 14));
            DropColumn("dbo.Students", "Club_Id");
            DropColumn("dbo.Clubs", "CoordinatorName_Id");
            DropColumn("dbo.Clubs", "Student_Id");
            CreateIndex("dbo.Clubs", "StudentId");
            AddForeignKey("dbo.Clubs", "StudentId", "dbo.Students", "Id", cascadeDelete: true);
        }
    }
}
