using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyBudget.Infra.Migrations
{
    /// <inheritdoc />
    public partial class EnhanceBudgetItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BudgetItems_Budgets_BudgetId",
                table: "BudgetItems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BudgetItems");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "BudgetItems",
                newName: "Category");

            migrationBuilder.AlterColumn<Guid>(
                name: "BudgetId",
                table: "BudgetItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Currency",
                table: "BudgetItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "BudgetItems",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<decimal>(
                name: "Value",
                table: "BudgetItems",
                type: "SMALLMONEY",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetItems_Budgets_BudgetId",
                table: "BudgetItems",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BudgetItems_Budgets_BudgetId",
                table: "BudgetItems");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "BudgetItems");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "BudgetItems");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "BudgetItems");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "BudgetItems",
                newName: "UserName");

            migrationBuilder.AlterColumn<Guid>(
                name: "BudgetId",
                table: "BudgetItems",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "BudgetItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetItems_Budgets_BudgetId",
                table: "BudgetItems",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "Id");
        }
    }
}
