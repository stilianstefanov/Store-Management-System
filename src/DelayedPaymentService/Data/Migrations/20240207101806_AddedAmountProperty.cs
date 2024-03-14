﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DelayedPaymentService.Migrations
{
    /// <inheritdoc />
    public partial class AddedAmountProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Purchases",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Purchases");
        }
    }
}
