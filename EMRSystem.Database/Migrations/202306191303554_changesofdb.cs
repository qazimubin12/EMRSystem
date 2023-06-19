namespace EMRSystem.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changesofdb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "InvoiceNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "InvoiceNo");
        }
    }
}
