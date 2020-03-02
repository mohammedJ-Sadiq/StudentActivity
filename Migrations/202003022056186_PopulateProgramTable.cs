namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateProgramTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Programs(Title, Time, StartDate, EndDate, MaximumStudentNumber,ClubId) VALUES('AutoCad', '7:30', '2/3/2020','4/3/2020', 30, 1)");
        }
        
        public override void Down()
        {
        }
    }
}
