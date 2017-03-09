namespace MvcCms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "ImageUrl", c => c.String());
            DropColumn("dbo.Posts", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Image", c => c.String());
            DropColumn("dbo.Posts", "ImageUrl");
        }
    }
}
