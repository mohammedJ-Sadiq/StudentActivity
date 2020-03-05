namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class programAdmins : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO programAdmins VALUES(1,'4002541')");
        }
        
        public override void Down()
        {
        }
    }
}
