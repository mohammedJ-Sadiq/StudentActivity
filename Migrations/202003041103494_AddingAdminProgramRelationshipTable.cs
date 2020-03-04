namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingAdminProgramRelationshipTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProgramStudents", newName: "StudentPrograms");
            DropPrimaryKey("dbo.StudentPrograms");
            CreateTable(
                "dbo.ProgramAdmins",
                c => new
                    {
                        Program_Id = c.Int(nullable: false),
                        Admin_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Program_Id, t.Admin_Id })
                .ForeignKey("dbo.Programs", t => t.Program_Id, cascadeDelete: true)
                .ForeignKey("dbo.Admins", t => t.Admin_Id, cascadeDelete: true)
                .Index(t => t.Program_Id)
                .Index(t => t.Admin_Id);
            
            AddPrimaryKey("dbo.StudentPrograms", new[] { "Student_Id", "Program_Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProgramAdmins", "Admin_Id", "dbo.Admins");
            DropForeignKey("dbo.ProgramAdmins", "Program_Id", "dbo.Programs");
            DropIndex("dbo.ProgramAdmins", new[] { "Admin_Id" });
            DropIndex("dbo.ProgramAdmins", new[] { "Program_Id" });
            DropPrimaryKey("dbo.StudentPrograms");
            DropTable("dbo.ProgramAdmins");
            AddPrimaryKey("dbo.StudentPrograms", new[] { "Program_Id", "Student_Id" });
            RenameTable(name: "dbo.StudentPrograms", newName: "ProgramStudents");
        }
    }
}
