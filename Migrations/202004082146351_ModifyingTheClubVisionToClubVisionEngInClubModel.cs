namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyingTheClubVisionToClubVisionEngInClubModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clubs", "ClubVisionEng", c => c.String(maxLength: 1000));
            DropColumn("dbo.Clubs", "ClubVision");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clubs", "ClubVision", c => c.String(maxLength: 1000));
            DropColumn("dbo.Clubs", "ClubVisionEng");
        }
    }
}
