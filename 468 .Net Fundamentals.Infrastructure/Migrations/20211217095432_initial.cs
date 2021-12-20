using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _468_.Net_Fundamentals.Infrastructure.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardAssign_User_AssignTo",
                table: "CardAssign");

            migrationBuilder.DropForeignKey(
                name: "FK_CardAssign_Card_CardId",
                table: "CardAssign");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CardAssign",
                table: "CardAssign");

            migrationBuilder.DropIndex(
                name: "IX_CardAssign_AssignTo",
                table: "CardAssign");

            migrationBuilder.AlterColumn<int>(
                name: "CardId",
                table: "CardAssign",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AssignTo",
                table: "CardAssign",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Card",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CardAssign",
                table: "CardAssign",
                columns: new[] { "AssignTo", "CardId" });

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2021, 12, 17, 16, 54, 31, 773, DateTimeKind.Local).AddTicks(7546));

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2021, 12, 17, 16, 54, 31, 775, DateTimeKind.Local).AddTicks(2528));

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2021, 12, 17, 16, 54, 31, 775, DateTimeKind.Local).AddTicks(2586));

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2021, 12, 17, 16, 54, 31, 775, DateTimeKind.Local).AddTicks(2589));

            migrationBuilder.AddForeignKey(
                name: "FK_CardAssign_User_AssignTo",
                table: "CardAssign",
                column: "AssignTo",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CardAssign_Card_CardId",
                table: "CardAssign",
                column: "CardId",
                principalTable: "Card",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardAssign_User_AssignTo",
                table: "CardAssign");

            migrationBuilder.DropForeignKey(
                name: "FK_CardAssign_Card_CardId",
                table: "CardAssign");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CardAssign",
                table: "CardAssign");

            migrationBuilder.AlterColumn<int>(
                name: "CardId",
                table: "CardAssign",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "AssignTo",
                table: "CardAssign",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Card",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CardAssign",
                table: "CardAssign",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2021, 12, 17, 16, 5, 15, 982, DateTimeKind.Local).AddTicks(6800));

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2021, 12, 17, 16, 5, 15, 983, DateTimeKind.Local).AddTicks(7913));

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2021, 12, 17, 16, 5, 15, 983, DateTimeKind.Local).AddTicks(7967));

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2021, 12, 17, 16, 5, 15, 983, DateTimeKind.Local).AddTicks(7970));

            migrationBuilder.CreateIndex(
                name: "IX_CardAssign_AssignTo",
                table: "CardAssign",
                column: "AssignTo");

            migrationBuilder.AddForeignKey(
                name: "FK_CardAssign_User_AssignTo",
                table: "CardAssign",
                column: "AssignTo",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CardAssign_Card_CardId",
                table: "CardAssign",
                column: "CardId",
                principalTable: "Card",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
