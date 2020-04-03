namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingClubCorVisibleToProgramModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Programs", "ClubCorVisible", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Programs", "ClubCorVisible");
        }
    }
}
