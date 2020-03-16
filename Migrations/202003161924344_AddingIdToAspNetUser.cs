namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingIdToAspNetUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "studentId", c => c.String(nullable: false, maxLength: 14));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "studentId");
        }
    }
}
