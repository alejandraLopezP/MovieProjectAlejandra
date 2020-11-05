namespace MovieProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "BillingZip", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Customers", "BillingCity", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Customers", "DeliveryAddress", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.Customers", "DeliveryZip", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Customers", "DeliveryCity", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Customers", "PhoneNo", c => c.String(maxLength: 50));
            AlterColumn("dbo.Customers", "BillingAddress", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.Customers", "Phone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "Phone", c => c.String(maxLength: 50));
            AlterColumn("dbo.Customers", "BillingAddress", c => c.String(nullable: false));
            DropColumn("dbo.Customers", "PhoneNo");
            DropColumn("dbo.Customers", "DeliveryCity");
            DropColumn("dbo.Customers", "DeliveryZip");
            DropColumn("dbo.Customers", "DeliveryAddress");
            DropColumn("dbo.Customers", "BillingCity");
            DropColumn("dbo.Customers", "BillingZip");
        }
    }
}
