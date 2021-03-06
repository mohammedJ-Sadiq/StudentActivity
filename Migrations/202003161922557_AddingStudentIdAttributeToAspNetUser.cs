namespace StudentActivity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingStudentIdAttributeToAspNetUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUserClaims", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.AspNetUserLogins", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.AspNetUserRoles", "Student_Id", "dbo.Students");
            DropIndex("dbo.AspNetUserClaims", new[] { "Student_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "Student_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "Student_Id" });
            DropColumn("dbo.Students", "EmailConfirmed");
            DropColumn("dbo.Students", "PasswordHash");
            DropColumn("dbo.Students", "SecurityStamp");
            DropColumn("dbo.Students", "PhoneNumber");
            DropColumn("dbo.Students", "PhoneNumberConfirmed");
            DropColumn("dbo.Students", "TwoFactorEnabled");
            DropColumn("dbo.Students", "LockoutEndDateUtc");
            DropColumn("dbo.Students", "LockoutEnabled");
            DropColumn("dbo.Students", "AccessFailedCount");
            DropColumn("dbo.Students", "UserName");
            DropColumn("dbo.AspNetUserClaims", "Student_Id");
            DropColumn("dbo.AspNetUserLogins", "Student_Id");
            DropColumn("dbo.AspNetUserRoles", "Student_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUserRoles", "Student_Id", c => c.String(maxLength: 14));
            AddColumn("dbo.AspNetUserLogins", "Student_Id", c => c.String(maxLength: 14));
            AddColumn("dbo.AspNetUserClaims", "Student_Id", c => c.String(maxLength: 14));
            AddColumn("dbo.Students", "UserName", c => c.String());
            AddColumn("dbo.Students", "AccessFailedCount", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "LockoutEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.Students", "LockoutEndDateUtc", c => c.DateTime());
            AddColumn("dbo.Students", "TwoFactorEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.Students", "PhoneNumberConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Students", "PhoneNumber", c => c.String());
            AddColumn("dbo.Students", "SecurityStamp", c => c.String());
            AddColumn("dbo.Students", "PasswordHash", c => c.String());
            AddColumn("dbo.Students", "EmailConfirmed", c => c.Boolean(nullable: false));
            CreateIndex("dbo.AspNetUserRoles", "Student_Id");
            CreateIndex("dbo.AspNetUserLogins", "Student_Id");
            CreateIndex("dbo.AspNetUserClaims", "Student_Id");
            AddForeignKey("dbo.AspNetUserRoles", "Student_Id", "dbo.Students", "Id");
            AddForeignKey("dbo.AspNetUserLogins", "Student_Id", "dbo.Students", "Id");
            AddForeignKey("dbo.AspNetUserClaims", "Student_Id", "dbo.Students", "Id");
        }
    }
}
