namespace ConnectEvent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class blah1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attendees", "ResponseDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Attendees", "ResponseDate");
        }
    }
}
