namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class populateClubVision : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Clubs SET ClubVision = 'To offer the best environment for the comupter science students, where they can invest their ideas and efforts.' WHERE id = 2 ");
        }
        
        public override void Down()
        {
        }
    }
}
