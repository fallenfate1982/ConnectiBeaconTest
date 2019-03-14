namespace ConnectEvent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class blah21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attendees", "PresentAtEvent", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Attendees", "PresentAtEvent");
        }
    }
}
