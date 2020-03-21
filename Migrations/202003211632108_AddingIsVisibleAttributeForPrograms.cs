namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingIsVisibleAttributeForPrograms : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Programs", "IsVisible", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Programs", "IsVisible");
        }
    }
}
