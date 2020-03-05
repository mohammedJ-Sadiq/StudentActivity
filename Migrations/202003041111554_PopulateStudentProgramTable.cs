namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateStudentProgramTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO StudentPrograms VALUES ('1','36110425')");
            Sql("INSERT INTO StudentPrograms VALUES ('1','36110580')");
        }
        
        public override void Down()
        {
        }
    }
}
