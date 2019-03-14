namespace ConnectEvent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class blah4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Attendees", "NotAttending");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Attendees", "NotAttending", c => c.Boolean(nullable: false));
        }
    }
}
