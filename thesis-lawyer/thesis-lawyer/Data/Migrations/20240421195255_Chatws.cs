using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace thesis_lawyer.Data.Migrations
{
    /// <inheritdoc />
    public partial class Chatws : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChatId",
                table: "HistoryChats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chat_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryChats_ChatId",
                table: "HistoryChats",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_UserId",
                table: "Chat",
                column: "UserId");

         
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryChats_Chat_ChatId",
                table: "HistoryChats");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropIndex(
                name: "IX_HistoryChats_ChatId",
                table: "HistoryChats");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "HistoryChats");
        }
    }
}
