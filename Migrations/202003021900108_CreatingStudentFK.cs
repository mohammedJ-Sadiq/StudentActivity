namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatingStudentFK : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clubs", "StudentId", c => c.String(maxLength: 14));
            CreateIndex("dbo.Clubs", "StudentId");
            AddForeignKey("dbo.Clubs", "StudentId", "dbo.Students", "Id");
            DropColumn("dbo.Clubs", "CoordinatorName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clubs", "CoordinatorName", c => c.String());
            DropForeignKey("dbo.Clubs", "StudentId", "dbo.Students");
            DropIndex("dbo.Clubs", new[] { "StudentId" });
            DropColumn("dbo.Clubs", "StudentId");
        }
    }
}
