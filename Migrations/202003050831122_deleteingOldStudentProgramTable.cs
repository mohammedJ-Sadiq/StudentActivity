namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteingOldStudentProgramTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StudentPrograms", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.StudentPrograms", "Program_Id", "dbo.Programs");
            DropIndex("dbo.StudentPrograms", new[] { "Student_Id" });
            DropIndex("dbo.StudentPrograms", new[] { "Program_Id" });
            DropTable("dbo.StudentPrograms");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StudentPrograms",
                c => new
                    {
                        Student_Id = c.String(nullable: false, maxLength: 14),
                        Program_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Student_Id, t.Program_Id });
            
            CreateIndex("dbo.StudentPrograms", "Program_Id");
            CreateIndex("dbo.StudentPrograms", "Student_Id");
            AddForeignKey("dbo.StudentPrograms", "Program_Id", "dbo.Programs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StudentPrograms", "Student_Id", "dbo.Students", "Id", cascadeDelete: true);
        }
    }
}
