using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig_comment_appuser_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Destinations_DestinationId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "DestinationId",
                table: "Comments",
                newName: "DestinationID");

            migrationBuilder.RenameColumn(
                name: "CommentId",
                table: "Comments",
                newName: "CommentID");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_DestinationId",
                table: "Comments",
                newName: "IX_Comments_DestinationID");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Destinations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "GuideID",
                table: "Destinations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AppUserID",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Destinations_GuideID",
                table: "Destinations",
                column: "GuideID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AppUserID",
                table: "Comments",
                column: "AppUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_AppUserID",
                table: "Comments",
                column: "AppUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Destinations_DestinationID",
                table: "Comments",
                column: "DestinationID",
                principalTable: "Destinations",
                principalColumn: "DestinationID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Destinations_Guides_GuideID",
                table: "Destinations",
                column: "GuideID",
                principalTable: "Guides",
                principalColumn: "GuideId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_AppUserID",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Destinations_DestinationID",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Destinations_Guides_GuideID",
                table: "Destinations");

            migrationBuilder.DropIndex(
                name: "IX_Destinations_GuideID",
                table: "Destinations");

            migrationBuilder.DropIndex(
                name: "IX_Comments_AppUserID",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Destinations");

            migrationBuilder.DropColumn(
                name: "GuideID",
                table: "Destinations");

            migrationBuilder.DropColumn(
                name: "AppUserID",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "DestinationID",
                table: "Comments",
                newName: "DestinationId");

            migrationBuilder.RenameColumn(
                name: "CommentID",
                table: "Comments",
                newName: "CommentId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_DestinationID",
                table: "Comments",
                newName: "IX_Comments_DestinationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Destinations_DestinationId",
                table: "Comments",
                column: "DestinationId",
                principalTable: "Destinations",
                principalColumn: "DestinationID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
