using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jasmine.FastenerDepartment.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddLocalization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductHistoryEntries_ProductChangeReason_ChangeReasonId",
                table: "ProductHistoryEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductChangeReason",
                table: "ProductChangeReason");

            migrationBuilder.RenameTable(
                name: "ProductChangeReason",
                newName: "ProductChangeReasons");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "PriceTags");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PriceTags",
                type: "jsonb",
                nullable: true);

            migrationBuilder.DropColumn(
                name: "Name",
                table: "OrderStatuses");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "OrderStatuses",
                type: "jsonb",
                nullable: true);

            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "MeasurementUnits");

            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "MeasurementUnits",
                type: "jsonb",
                nullable: true);

            migrationBuilder.DropColumn(
                name: "Name",
                table: "MeasurementUnits");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MeasurementUnits",
                type: "jsonb",
                nullable: true);

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProductChangeReasons");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProductChangeReasons",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductChangeReasons",
                table: "ProductChangeReasons",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "MeasurementUnits",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "{\"En\":\"Pieces\",\"Ru\":\"Штуки\"}", "{\"En\":\"pcs\",\"Ru\":\"шт\"}" });

            migrationBuilder.UpdateData(
                table: "MeasurementUnits",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "{\"En\":\"Meters\",\"Ru\":\"Метры\"}", "{\"En\":\"m\",\"Ru\":\"м\"}" });

            migrationBuilder.UpdateData(
                table: "MeasurementUnits",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "{\"En\":\"Kilograms\",\"Ru\":\"Килограммы\"}", "{\"En\":\"kg\",\"Ru\":\"кг\"}" });

            migrationBuilder.UpdateData(
                table: "MeasurementUnits",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "{\"En\":\"Packages\",\"Ru\":\"Упаковки\"}", "{\"En\":\"pack\",\"Ru\":\"уп\"}" });

            migrationBuilder.UpdateData(
                table: "MeasurementUnits",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "{\"En\":\"Sets\",\"Ru\":\"Комплекты\"}", "{\"En\":\"sets\",\"Ru\":\"компл\"}" });

            migrationBuilder.UpdateData(
                table: "MeasurementUnits",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "{\"En\":\"Lists\",\"Ru\":\"Листы\"}", "{\"En\":\"l\",\"Ru\":\"л\"}" });

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "{\"En\":\"Created\",\"Ru\":\"Создан\"}");

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "{\"En\":\"Sent\",\"Ru\":\"Отправлен\"}");

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "{\"En\":\"Fulfilled\",\"Ru\":\"Доставлен\"}");

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "{\"En\":\"Cancelled\",\"Ru\":\"Отменен\"}");

            migrationBuilder.UpdateData(
                table: "PriceTags",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "{\"En\":\"Size S\",\"Ru\":\"Размер S\"}");

            migrationBuilder.UpdateData(
                table: "PriceTags",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "{\"En\":\"Size L\",\"Ru\":\"Размер L\"}");

            migrationBuilder.UpdateData(
                table: "PriceTags",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "{\"En\":\"Size M\",\"Ru\":\"Размер M\"}");

            migrationBuilder.UpdateData(
                table: "PriceTags",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "{\"En\":\"Size XL\",\"Ru\":\"Размер XL\"}");

            migrationBuilder.UpdateData(
                table: "ProductChangeReasons",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "{\"En\":\"Created\",\"Ru\":\"Создан\"}");

            migrationBuilder.UpdateData(
                table: "ProductChangeReasons",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "{\"En\":\"Changing the number\",\"Ru\":\"Изменение артикула\"}");

            migrationBuilder.UpdateData(
                table: "ProductChangeReasons",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "{\"En\":\"Changing the name\",\"Ru\":\"Изменение названия\"}");

            migrationBuilder.UpdateData(
                table: "ProductChangeReasons",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "{\"En\":\"Changing the price\",\"Ru\":\"Изменение цены\"}");

            migrationBuilder.UpdateData(
                table: "ProductChangeReasons",
                keyColumn: "Id",
                keyValue: 5,
                column: "Description",
                value: "{\"En\":\"Changing the price tag size\",\"Ru\":\"Изменение размера ценника\"}");

            migrationBuilder.UpdateData(
                table: "ProductChangeReasons",
                keyColumn: "Id",
                keyValue: 6,
                column: "Description",
                value: "{\"En\":\"Changing the measurement unit\",\"Ru\":\"Изменение единицы измерения\"}");

            migrationBuilder.UpdateData(
                table: "ProductChangeReasons",
                keyColumn: "Id",
                keyValue: 7,
                column: "Description",
                value: "{\"En\":\"Changing the order status\",\"Ru\":\"Изменение статуса заказа\"}");

            migrationBuilder.UpdateData(
                table: "ProductChangeReasons",
                keyColumn: "Id",
                keyValue: 8,
                column: "Description",
                value: "{\"En\":\"Changing the print status\",\"Ru\":\"Изменение статуса печати\"}");

            migrationBuilder.UpdateData(
                table: "ProductChangeReasons",
                keyColumn: "Id",
                keyValue: 9,
                column: "Description",
                value: "{\"En\":\"Deleted\",\"Ru\":\"Удален\"}");

            migrationBuilder.UpdateData(
                table: "ProductChangeReasons",
                keyColumn: "Id",
                keyValue: 10,
                column: "Description",
                value: "{\"En\":\"Recovered\",\"Ru\":\"Восстановлен\"}");

            migrationBuilder.UpdateData(
                table: "ProductChangeReasons",
                keyColumn: "Id",
                keyValue: 11,
                column: "Description",
                value: "{\"En\":\"Changing the type\",\"Ru\":\"Изменение типа\"}");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PriceTags",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OrderStatuses",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "ShortName",
                table: "MeasurementUnits",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MeasurementUnits",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ProductChangeReasons",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductHistoryEntries_ProductChangeReasons_ChangeReasonId",
                table: "ProductHistoryEntries",
                column: "ChangeReasonId",
                principalTable: "ProductChangeReasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductHistoryEntries_ProductChangeReasons_ChangeReasonId",
                table: "ProductHistoryEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductChangeReasons",
                table: "ProductChangeReasons");

            migrationBuilder.RenameTable(
                name: "ProductChangeReasons",
                newName: "ProductChangeReason");

            migrationBuilder.DropColumn(
               name: "Name",
               table: "PriceTags");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PriceTags",
                type: "text",
                nullable: true);

            migrationBuilder.DropColumn(
                name: "Name",
                table: "OrderStatuses");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "OrderStatuses",
                type: "text",
                nullable: true);

            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "MeasurementUnits");

            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "MeasurementUnits",
                type: "text",
                nullable: true);

            migrationBuilder.DropColumn(
                name: "Name",
                table: "MeasurementUnits");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MeasurementUnits",
                type: "text",
                nullable: true);

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProductChangeReasons");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProductChangeReasons",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductChangeReason",
                table: "ProductChangeReason",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "MeasurementUnits",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "Pieces", "pcs" });

            migrationBuilder.UpdateData(
                table: "MeasurementUnits",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "Meters", "m" });

            migrationBuilder.UpdateData(
                table: "MeasurementUnits",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "Kilograms", "kg" });

            migrationBuilder.UpdateData(
                table: "MeasurementUnits",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "Packages", "pack" });

            migrationBuilder.UpdateData(
                table: "MeasurementUnits",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "Sets", "sets" });

            migrationBuilder.UpdateData(
                table: "MeasurementUnits",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "Lists", "l" });

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Created");

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Sent");

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Fulfilled");

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Cancelled");

            migrationBuilder.UpdateData(
                table: "PriceTags",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "S");

            migrationBuilder.UpdateData(
                table: "PriceTags",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "L");

            migrationBuilder.UpdateData(
                table: "PriceTags",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "M");

            migrationBuilder.UpdateData(
                table: "PriceTags",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "XL");

            migrationBuilder.UpdateData(
                table: "ProductChangeReason",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Product was created");

            migrationBuilder.UpdateData(
                table: "ProductChangeReason",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Product number was changed");

            migrationBuilder.UpdateData(
                table: "ProductChangeReason",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "Product name was changed");

            migrationBuilder.UpdateData(
                table: "ProductChangeReason",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "Product price was changed");

            migrationBuilder.UpdateData(
                table: "ProductChangeReason",
                keyColumn: "Id",
                keyValue: 5,
                column: "Description",
                value: "Product price tag code was changed");

            migrationBuilder.UpdateData(
                table: "ProductChangeReason",
                keyColumn: "Id",
                keyValue: 6,
                column: "Description",
                value: "Product measurement unit code was changed");

            migrationBuilder.UpdateData(
                table: "ProductChangeReason",
                keyColumn: "Id",
                keyValue: 7,
                column: "Description",
                value: "Product order status was changed");

            migrationBuilder.UpdateData(
                table: "ProductChangeReason",
                keyColumn: "Id",
                keyValue: 8,
                column: "Description",
                value: "Product print status was changed");

            migrationBuilder.UpdateData(
                table: "ProductChangeReason",
                keyColumn: "Id",
                keyValue: 9,
                column: "Description",
                value: "Product was deleted");

            migrationBuilder.UpdateData(
                table: "ProductChangeReason",
                keyColumn: "Id",
                keyValue: 10,
                column: "Description",
                value: "Product was recovered");

            migrationBuilder.UpdateData(
                table: "ProductChangeReason",
                keyColumn: "Id",
                keyValue: 11,
                column: "Description",
                value: "Product type was changed");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PriceTags",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OrderStatuses",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "ShortName",
                table: "MeasurementUnits",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MeasurementUnits",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ProductChangeReasons",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductHistoryEntries_ProductChangeReason_ChangeReasonId",
                table: "ProductHistoryEntries",
                column: "ChangeReasonId",
                principalTable: "ProductChangeReason",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
