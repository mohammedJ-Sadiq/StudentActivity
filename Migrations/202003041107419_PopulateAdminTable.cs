namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateAdminTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Admins (Id, Name) VALUES ('4002541', 'Saeed')");
            Sql("INSERT INTO Admins (Id, Name) VALUES ('4011234', 'Ahmed')");
        }
        
        public override void Down()
        {
        }
    }
}
