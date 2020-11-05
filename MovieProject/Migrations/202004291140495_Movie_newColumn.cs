namespace MovieProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Movie_newColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "ImageUrl");
        }
    }
}
