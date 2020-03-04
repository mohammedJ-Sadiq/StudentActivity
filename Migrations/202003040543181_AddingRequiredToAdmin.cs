namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingRequiredToAdmin : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Admins", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Admins", "Name", c => c.String());
        }
    }
}
