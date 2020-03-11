namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingProgramDetailsToDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Programs", "Description", c => c.String(maxLength: 1000));
            DropColumn("dbo.Programs", "Details");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Programs", "Details", c => c.String(maxLength: 1000));
            DropColumn("dbo.Programs", "Description");
        }
    }
}
