namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingClubVisionToClubModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clubs", "ClubVision", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clubs", "ClubVision");
        }
    }
}
