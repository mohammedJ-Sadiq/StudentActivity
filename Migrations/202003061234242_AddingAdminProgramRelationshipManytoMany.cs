namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingAdminProgramRelationshipManytoMany : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admin_Program",
                c => new
                    {
                        AdminId = c.String(nullable: false, maxLength: 128),
                        ProgramId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AdminId, t.ProgramId })
                .ForeignKey("dbo.Admins", t => t.AdminId, cascadeDelete: false)
                .ForeignKey("dbo.Programs", t => t.ProgramId, cascadeDelete: false)
                .Index(t => t.AdminId)
                .Index(t => t.ProgramId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Admin_Program", "ProgramId", "dbo.Programs");
            DropForeignKey("dbo.Admin_Program", "AdminId", "dbo.Admins");
            DropIndex("dbo.Admin_Program", new[] { "ProgramId" });
            DropIndex("dbo.Admin_Program", new[] { "AdminId" });
            DropTable("dbo.Admin_Program");
        }
    }
}
