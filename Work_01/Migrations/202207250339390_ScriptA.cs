namespace Work_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScriptA : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        DeptName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        Roll = c.Int(nullable: false),
                        StudentName = c.String(nullable: false, maxLength: 50),
                        StudentDob = c.DateTime(nullable: false),
                        phone = c.String(nullable: false, maxLength: 14),
                        email = c.String(nullable: false, maxLength: 40),
                        Picture = c.String(),
                        DepartmentId = c.Int(nullable: false),
                        InstituteId = c.Int(nullable: false),
                        isActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.StudentId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Institutes", t => t.InstituteId, cascadeDelete: true)
                .Index(t => t.DepartmentId)
                .Index(t => t.InstituteId);
            
            CreateTable(
                "dbo.Institutes",
                c => new
                    {
                        InstituteId = c.Int(nullable: false, identity: true),
                        InstituteName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.InstituteId);
            
            CreateTable(
                "dbo.InstituteCosts",
                c => new
                    {
                        InstituteId = c.Int(nullable: false),
                        Cost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.InstituteId)
                .ForeignKey("dbo.Institutes", t => t.InstituteId)
                .Index(t => t.InstituteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "InstituteId", "dbo.Institutes");
            DropForeignKey("dbo.InstituteCosts", "InstituteId", "dbo.Institutes");
            DropForeignKey("dbo.Students", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.InstituteCosts", new[] { "InstituteId" });
            DropIndex("dbo.Students", new[] { "InstituteId" });
            DropIndex("dbo.Students", new[] { "DepartmentId" });
            DropTable("dbo.InstituteCosts");
            DropTable("dbo.Institutes");
            DropTable("dbo.Students");
            DropTable("dbo.Departments");
        }
    }
}
