namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateClubTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Clubs(Name, StudentId) VALUES('Civil Engineering', '36110425')");
            Sql("INSERT INTO Clubs(Name, StudentId) VALUES('Computer Science', '36110580')");
        }
        
        public override void Down()
        {
        }
    }
}
