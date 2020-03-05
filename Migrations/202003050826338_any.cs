namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class any : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Student_Program", "ProgramId", "dbo.Programs");
            DropForeignKey("dbo.Student_Program", "StudentId", "dbo.Students");
            DropIndex("dbo.Student_Program", new[] { "StudentId" });
            DropIndex("dbo.Student_Program", new[] { "ProgramId" });
            DropTable("dbo.Student_Program");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Student_Program",
                c => new
                    {
                        StudentId = c.String(nullable: false, maxLength: 14),
                        ProgramId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentId, t.ProgramId });
            
            CreateIndex("dbo.Student_Program", "ProgramId");
            CreateIndex("dbo.Student_Program", "StudentId");
            AddForeignKey("dbo.Student_Program", "StudentId", "dbo.Students", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Student_Program", "ProgramId", "dbo.Programs", "Id", cascadeDelete: true);
        }
    }
}
