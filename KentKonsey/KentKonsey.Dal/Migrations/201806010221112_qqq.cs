namespace KentKonsey.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qqq : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Galeris",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FotoUrl = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Galeris");
        }
    }
}
