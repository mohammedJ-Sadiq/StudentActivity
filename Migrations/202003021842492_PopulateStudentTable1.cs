namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateStudentTable1 : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Students(Id, Name, MobileNo, Email) VALUES('36110580', 'Mohammed j. Sadiq', '0547158828', 'diamoh0@gmail.com')");
            Sql("INSERT INTO Students(Id, Name, MobileNo, Email) VALUES('36110425', 'Hassan a. Abu Shalwah', '0541689240', 'hassan@gmail.com')");
    }
        
        public override void Down()
        {
        }
    }
}
