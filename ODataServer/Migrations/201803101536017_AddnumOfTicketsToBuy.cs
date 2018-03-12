namespace ODataServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddnumOfTicketsToBuy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MovieTickets", "numOfTicketsToBuy", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MovieTickets", "numOfTicketsToBuy");
        }
    }
}
