namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingStudentsVariablesValues : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Students");
            AlterColumn("dbo.Students", "Id", c => c.String(nullable: false, maxLength: 14));
            AlterColumn("dbo.Students", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "MobileNo", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "Email", c => c.String(nullable: false));
            AddPrimaryKey("dbo.Students", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Students");
            AlterColumn("dbo.Students", "Email", c => c.String());
            AlterColumn("dbo.Students", "MobileNo", c => c.String());
            AlterColumn("dbo.Students", "Name", c => c.String());
            AlterColumn("dbo.Students", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Students", "Id");
        }
    }
}
