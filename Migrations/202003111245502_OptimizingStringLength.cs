namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OptimizingStringLength : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Admin_Program", "AdminId", "dbo.Admins");
            DropIndex("dbo.Admin_Program", new[] { "AdminId" });
            DropPrimaryKey("dbo.Admin_Program");
            DropPrimaryKey("dbo.Admins");
            AlterColumn("dbo.Admin_Program", "AdminId", c => c.String(nullable: false, maxLength: 14));
            AlterColumn("dbo.Admins", "Id", c => c.String(nullable: false, maxLength: 14));
            AlterColumn("dbo.Admins", "Name", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.Programs", "Title", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.Programs", "Time", c => c.String(nullable: false, maxLength: 8));
            AlterColumn("dbo.Programs", "StartDate", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Programs", "EndDate", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Programs", "Details", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Clubs", "Name", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.Students", "Name", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.Students", "MobileNo", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Students", "Email", c => c.String(nullable: false, maxLength: 40));
            AddPrimaryKey("dbo.Admin_Program", new[] { "AdminId", "ProgramId" });
            AddPrimaryKey("dbo.Admins", "Id");
            CreateIndex("dbo.Admin_Program", "AdminId");
            AddForeignKey("dbo.Admin_Program", "AdminId", "dbo.Admins", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Admin_Program", "AdminId", "dbo.Admins");
            DropIndex("dbo.Admin_Program", new[] { "AdminId" });
            DropPrimaryKey("dbo.Admins");
            DropPrimaryKey("dbo.Admin_Program");
            AlterColumn("dbo.Students", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "MobileNo", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Clubs", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Programs", "Details", c => c.String());
            AlterColumn("dbo.Programs", "EndDate", c => c.String(nullable: false));
            AlterColumn("dbo.Programs", "StartDate", c => c.String(nullable: false));
            AlterColumn("dbo.Programs", "Time", c => c.String(nullable: false));
            AlterColumn("dbo.Programs", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Admins", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Admins", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Admin_Program", "AdminId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Admins", "Id");
            AddPrimaryKey("dbo.Admin_Program", new[] { "AdminId", "ProgramId" });
            CreateIndex("dbo.Admin_Program", "AdminId");
            AddForeignKey("dbo.Admin_Program", "AdminId", "dbo.Admins", "Id", cascadeDelete: true);
        }
    }
}
