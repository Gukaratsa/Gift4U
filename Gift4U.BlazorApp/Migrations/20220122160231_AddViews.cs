using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gift4U.BlazorApp.Migrations
{
    public partial class AddViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW GivenGiftView AS
                SELECT 
	                gg.Id AS Id,
	                giver.Email AS GiverEmail,
	                getter.Email AS ReceiverEmail,
	                gift.Name AS Gift,
	                gg.Amount AS Amount
                FROM GivenGifts AS gg
                LEFT JOIN Users AS giver ON giver.Id = gg.GiverId
                LEFT JOIN Users AS getter ON getter.Id = gg.ReceiverId
                LEFT JOIN Gifts AS gift ON gift.Id = gg.GiftId;
                ");

            migrationBuilder.Sql(@"
                CREATE VIEW PendingRequestView AS
                SELECT
	                req.Id AS Id,
	                reqState.Name AS State,
	                giftView.GiverEmail AS GiverEmail,
	                giftView.ReceiverEmail AS ReceiverEmail,
	                giftView.Gift AS Gift,
	                giftView.Amount AS Amount
                FROM Requests AS req
                LEFT JOIN RequestStates AS reqState ON reqState.Id =  req.RequestStateId
                LEFT JOIN GivenGiftView AS giftView ON giftView.Id = req.GivenInRequestId
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW GivenGiftView;");
            migrationBuilder.Sql(@"DROP VIEW PendingRequestView;");
        }
    }
}
