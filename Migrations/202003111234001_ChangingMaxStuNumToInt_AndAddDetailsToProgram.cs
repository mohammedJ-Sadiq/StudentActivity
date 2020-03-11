namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingMaxStuNumToInt_AndAddDetailsToProgram : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Programs", "Details", c => c.String());
            AlterColumn("dbo.Programs", "MaximumStudentNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Programs", "MaximumStudentNumber", c => c.String(nullable: false));
            DropColumn("dbo.Programs", "Details");
        }
    }
}
