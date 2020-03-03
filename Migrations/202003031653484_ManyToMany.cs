namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManyToMany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Programs", "ClubId", "dbo.Clubs");
            DropIndex("dbo.Programs", new[] { "ClubId" });
            CreateTable(
                "dbo.ProgramStudents",
                c => new
                    {
                        Program_Id = c.Int(nullable: false),
                        Student_Id = c.String(nullable: false, maxLength: 14),
                    })
                .PrimaryKey(t => new { t.Program_Id, t.Student_Id })
                .ForeignKey("dbo.Programs", t => t.Program_Id, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.Student_Id, cascadeDelete: true)
                .Index(t => t.Program_Id)
                .Index(t => t.Student_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProgramStudents", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.ProgramStudents", "Program_Id", "dbo.Programs");
            DropIndex("dbo.ProgramStudents", new[] { "Student_Id" });
            DropIndex("dbo.ProgramStudents", new[] { "Program_Id" });
            DropTable("dbo.ProgramStudents");
            CreateIndex("dbo.Programs", "ClubId");
            AddForeignKey("dbo.Programs", "ClubId", "dbo.Clubs", "Id", cascadeDelete: true);
        }
    }
}
