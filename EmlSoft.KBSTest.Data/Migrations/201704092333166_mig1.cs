namespace EmlSoft.KBSTest.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SourceId = c.Int(nullable: false),
                        Data = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sources", t => t.SourceId, cascadeDelete: true)
                .Index(t => t.SourceId);
            
            CreateTable(
                "dbo.Sources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Url, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contents", "SourceId", "dbo.Sources");
            DropIndex("dbo.Sources", new[] { "Url" });
            DropIndex("dbo.Contents", new[] { "SourceId" });
            DropTable("dbo.Sources");
            DropTable("dbo.Contents");
        }
    }
}
