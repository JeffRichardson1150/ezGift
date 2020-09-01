namespace ezGift.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedEventLocation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegistryEvent", "EventLocation", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegistryEvent", "EventLocation");
        }
    }
}
