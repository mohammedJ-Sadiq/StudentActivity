namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingStudentFieldToAspnetUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "EmailConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Students", "PasswordHash", c => c.String());
            AddColumn("dbo.Students", "SecurityStamp", c => c.String());
            AddColumn("dbo.Students", "PhoneNumber", c => c.String());
            AddColumn("dbo.Students", "PhoneNumberConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Students", "TwoFactorEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.Students", "LockoutEndDateUtc", c => c.DateTime());
            AddColumn("dbo.Students", "LockoutEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.Students", "AccessFailedCount", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "UserName", c => c.String());
            AddColumn("dbo.AspNetUserRoles", "Student_Id", c => c.String(maxLength: 14));
            AddColumn("dbo.AspNetUserClaims", "Student_Id", c => c.String(maxLength: 14));
            AddColumn("dbo.AspNetUserLogins", "Student_Id", c => c.String(maxLength: 14));
            CreateIndex("dbo.AspNetUserClaims", "Student_Id");
            CreateIndex("dbo.AspNetUserLogins", "Student_Id");
            CreateIndex("dbo.AspNetUserRoles", "Student_Id");
            AddForeignKey("dbo.AspNetUserClaims", "Student_Id", "dbo.Students", "Id");
            AddForeignKey("dbo.AspNetUserLogins", "Student_Id", "dbo.Students", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "Student_Id", "dbo.Students", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.AspNetUserLogins", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.AspNetUserClaims", "Student_Id", "dbo.Students");
            DropIndex("dbo.AspNetUserRoles", new[] { "Student_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "Student_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "Student_Id" });
            DropColumn("dbo.AspNetUserLogins", "Student_Id");
            DropColumn("dbo.AspNetUserClaims", "Student_Id");
            DropColumn("dbo.AspNetUserRoles", "Student_Id");
            DropColumn("dbo.Students", "UserName");
            DropColumn("dbo.Students", "AccessFailedCount");
            DropColumn("dbo.Students", "LockoutEnabled");
            DropColumn("dbo.Students", "LockoutEndDateUtc");
            DropColumn("dbo.Students", "TwoFactorEnabled");
            DropColumn("dbo.Students", "PhoneNumberConfirmed");
            DropColumn("dbo.Students", "PhoneNumber");
            DropColumn("dbo.Students", "SecurityStamp");
            DropColumn("dbo.Students", "PasswordHash");
            DropColumn("dbo.Students", "EmailConfirmed");
        }
    }
}
