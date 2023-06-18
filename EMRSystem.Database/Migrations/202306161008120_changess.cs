namespace EMRSystem.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changess : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HospitalRecords",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HopistalID = c.Int(nullable: false),
                        Disease = c.String(),
                        Doctor = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Patient = c.String(),
                        Doctor = c.String(),
                        SuggestMedicine = c.String(),
                        Attachment = c.String(),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.AspNetUsers", "CNIC", c => c.String());
            AddColumn("dbo.AspNetUsers", "DOB", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "Gender", c => c.String());
            AddColumn("dbo.AspNetUsers", "City", c => c.String());
            AddColumn("dbo.AspNetUsers", "RegisteredNo", c => c.String());
            AddColumn("dbo.AspNetUsers", "Image", c => c.String());
            AddColumn("dbo.AspNetUsers", "Age", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Age");
            DropColumn("dbo.AspNetUsers", "Image");
            DropColumn("dbo.AspNetUsers", "RegisteredNo");
            DropColumn("dbo.AspNetUsers", "City");
            DropColumn("dbo.AspNetUsers", "Gender");
            DropColumn("dbo.AspNetUsers", "DOB");
            DropColumn("dbo.AspNetUsers", "CNIC");
            DropTable("dbo.Invoices");
            DropTable("dbo.HospitalRecords");
        }
    }
}
