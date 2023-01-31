using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersDetails_Cities_CityId",
                table: "UsersDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersDetails_Countries_CountryId",
                table: "UsersDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersDetails_Users_UserId",
                table: "UsersDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersDetails",
                table: "UsersDetails");

            migrationBuilder.RenameTable(
                name: "UsersDetails",
                newName: "UserDetails");

            migrationBuilder.RenameIndex(
                name: "IX_UsersDetails_Email",
                table: "UserDetails",
                newName: "IX_UserDetails_Email");

            migrationBuilder.RenameIndex(
                name: "IX_UsersDetails_CountryId",
                table: "UserDetails",
                newName: "IX_UserDetails_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_UsersDetails_CityId",
                table: "UserDetails",
                newName: "IX_UserDetails_CityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDetails",
                table: "UserDetails",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDetails_Cities_CityId",
                table: "UserDetails",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDetails_Countries_CountryId",
                table: "UserDetails",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDetails_Users_UserId",
                table: "UserDetails",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDetails_Cities_CityId",
                table: "UserDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDetails_Countries_CountryId",
                table: "UserDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDetails_Users_UserId",
                table: "UserDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDetails",
                table: "UserDetails");

            migrationBuilder.RenameTable(
                name: "UserDetails",
                newName: "UsersDetails");

            migrationBuilder.RenameIndex(
                name: "IX_UserDetails_Email",
                table: "UsersDetails",
                newName: "IX_UsersDetails_Email");

            migrationBuilder.RenameIndex(
                name: "IX_UserDetails_CountryId",
                table: "UsersDetails",
                newName: "IX_UsersDetails_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_UserDetails_CityId",
                table: "UsersDetails",
                newName: "IX_UsersDetails_CityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersDetails",
                table: "UsersDetails",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersDetails_Cities_CityId",
                table: "UsersDetails",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersDetails_Countries_CountryId",
                table: "UsersDetails",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersDetails_Users_UserId",
                table: "UsersDetails",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
