namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingBoolIsDeletedToProgram : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Programs", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Programs", "IsDeleted");
        }
    }
}
