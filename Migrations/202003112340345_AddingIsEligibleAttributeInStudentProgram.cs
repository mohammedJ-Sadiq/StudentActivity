namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingIsEligibleAttributeInStudentProgram : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Student_Program", "IsEligible", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Student_Program", "IsEligible");
        }
    }
}
