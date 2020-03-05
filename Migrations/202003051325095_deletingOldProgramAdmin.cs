namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletingOldProgramAdmin : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProgramAdmins", "Program_Id", "dbo.Programs");
            DropForeignKey("dbo.ProgramAdmins", "Admin_Id", "dbo.Admins");
            DropIndex("dbo.ProgramAdmins", new[] { "Program_Id" });
            DropIndex("dbo.ProgramAdmins", new[] { "Admin_Id" });
            DropTable("dbo.ProgramAdmins");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProgramAdmins",
                c => new
                    {
                        Program_Id = c.Int(nullable: false),
                        Admin_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Program_Id, t.Admin_Id });
            
            CreateIndex("dbo.ProgramAdmins", "Admin_Id");
            CreateIndex("dbo.ProgramAdmins", "Program_Id");
            AddForeignKey("dbo.ProgramAdmins", "Admin_Id", "dbo.Admins", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProgramAdmins", "Program_Id", "dbo.Programs", "Id", cascadeDelete: true);
        }
    }
}
