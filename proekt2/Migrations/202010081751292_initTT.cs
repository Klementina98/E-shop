namespace proekt2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initTT : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ArtWorks", "artist_Id", "dbo.Artists");
            DropIndex("dbo.ArtWorks", new[] { "artist_Id" });
            AddColumn("dbo.ArtWorks", "artist_Id1", c => c.Int());
            AlterColumn("dbo.ArtWorks", "artist_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.ArtWorks", "artist_Id1");
            AddForeignKey("dbo.ArtWorks", "artist_Id1", "dbo.Artists", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArtWorks", "artist_Id1", "dbo.Artists");
            DropIndex("dbo.ArtWorks", new[] { "artist_Id1" });
            AlterColumn("dbo.ArtWorks", "artist_Id", c => c.Int());
            DropColumn("dbo.ArtWorks", "artist_Id1");
            CreateIndex("dbo.ArtWorks", "artist_Id");
            AddForeignKey("dbo.ArtWorks", "artist_Id", "dbo.Artists", "Id");
        }
    }
}
