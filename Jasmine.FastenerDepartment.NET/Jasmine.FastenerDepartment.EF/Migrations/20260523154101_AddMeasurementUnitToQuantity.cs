using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jasmine.FastenerDepartment.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddMeasurementUnitToQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_FulfilledMeasurementUnitId",
                table: "OrderProducts",
                column: "FulfilledMeasurementUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderedMeasurementUnitId",
                table: "OrderProducts",
                column: "OrderedMeasurementUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_MeasurementUnits_FulfilledMeasurementUnitId",
                table: "OrderProducts",
                column: "FulfilledMeasurementUnitId",
                principalTable: "MeasurementUnits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_MeasurementUnits_OrderedMeasurementUnitId",
                table: "OrderProducts",
                column: "OrderedMeasurementUnitId",
                principalTable: "MeasurementUnits",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_MeasurementUnits_FulfilledMeasurementUnitId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_MeasurementUnits_OrderedMeasurementUnitId",
                table: "OrderProducts");

            migrationBuilder.DropIndex(
                name: "IX_OrderProducts_FulfilledMeasurementUnitId",
                table: "OrderProducts");

            migrationBuilder.DropIndex(
                name: "IX_OrderProducts_OrderedMeasurementUnitId",
                table: "OrderProducts");
        }
    }
}
