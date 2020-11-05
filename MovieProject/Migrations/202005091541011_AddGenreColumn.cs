namespace MovieProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGenreColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "User_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Movies", "Genre", c => c.String(maxLength: 50));
            CreateIndex("dbo.Customers", "User_Id");
            AddForeignKey("dbo.Customers", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Customers", new[] { "User_Id" });
            DropColumn("dbo.Movies", "Genre");
            DropColumn("dbo.Customers", "User_Id");
        }
    }
}
