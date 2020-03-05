namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class justTrying : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Student_Program",
                c => new
                    {
                        StudentId = c.String(nullable: false, maxLength: 14),
                        ProgramId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentId, t.ProgramId })
                .ForeignKey("dbo.Programs", t => t.ProgramId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.ProgramId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Student_Program", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Student_Program", "ProgramId", "dbo.Programs");
            DropIndex("dbo.Student_Program", new[] { "ProgramId" });
            DropIndex("dbo.Student_Program", new[] { "StudentId" });
            DropTable("dbo.Student_Program");
        }
    }
}
