namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingStudentClubRelationshipManytoMany : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Student_Club",
                c => new
                    {
                        StudentId = c.String(nullable: false, maxLength: 14),
                        ClubId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentId, t.ClubId })
                .ForeignKey("dbo.Clubs", t => t.ClubId, cascadeDelete: false)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: false)
                .Index(t => t.StudentId)
                .Index(t => t.ClubId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Student_Club", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Student_Club", "ClubId", "dbo.Clubs");
            DropIndex("dbo.Student_Club", new[] { "ClubId" });
            DropIndex("dbo.Student_Club", new[] { "StudentId" });
            DropTable("dbo.Student_Club");
        }
    }
}
