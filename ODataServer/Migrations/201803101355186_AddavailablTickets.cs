namespace ODataServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddavailablTickets : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MovieTickets", "availableTickets", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MovieTickets", "availableTickets");
        }
    }
}
