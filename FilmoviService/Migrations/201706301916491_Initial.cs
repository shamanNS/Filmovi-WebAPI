namespace FilmoviService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Filmovi",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(nullable: false),
                        Zanr = c.String(nullable: false),
                        Godina = c.Int(nullable: false),
                        ReziserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Reziseri", t => t.ReziserId, cascadeDelete: true)
                .Index(t => t.ReziserId);
            
            CreateTable(
                "dbo.Reziseri",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ime = c.String(nullable: false),
                        Prezime = c.String(nullable: false),
                        Starost = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Filmovi", "ReziserId", "dbo.Reziseri");
            DropIndex("dbo.Filmovi", new[] { "ReziserId" });
            DropTable("dbo.Reziseri");
            DropTable("dbo.Filmovi");
        }
    }
}
