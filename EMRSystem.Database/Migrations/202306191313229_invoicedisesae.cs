namespace EMRSystem.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoicedisesae : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "Disease", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "Disease");
        }
    }
}
