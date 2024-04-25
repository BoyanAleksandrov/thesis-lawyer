using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace thesis_lawyer.Data.Migrations
{
    /// <inheritdoc />
    public partial class OneToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            

            migrationBuilder.CreateTable(
                name: "HistoryChats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserForeignKey = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryChats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryChats_AspNetUsers_UserForeignKey",
                        column: x => x.UserForeignKey,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

          

            migrationBuilder.CreateIndex(
                name: "IX_HistoryChats_UserForeignKey",
                table: "HistoryChats",
                column: "UserForeignKey");

          
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ChatHistory_UserId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ChatHistory");

            migrationBuilder.DropTable(
                name: "HistoryChats");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AspNetUsers");
        }
    }
}
