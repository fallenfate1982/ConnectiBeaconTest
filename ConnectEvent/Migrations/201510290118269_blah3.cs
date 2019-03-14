namespace ConnectEvent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class blah3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attendees", "Attending", c => c.Boolean(nullable: false));
            AddColumn("dbo.Attendees", "NotAttending", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Attendees", "NotAttending");
            DropColumn("dbo.Attendees", "Attending");
        }
    }
}
