using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RAFS.Core.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "UserFunctionFarms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 15, 34, 46, 96, DateTimeKind.Utc).AddTicks(5795),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 16, 15, 19, 58, 684, DateTimeKind.Utc).AddTicks(4400));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdate",
                table: "TakeAndSendMaterials",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 15, 34, 46, 96, DateTimeKind.Utc).AddTicks(4008),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 16, 15, 19, 58, 684, DateTimeKind.Utc).AddTicks(1289));

            migrationBuilder.AlterColumn<DateTime>(
                name: "SendDate",
                table: "QuestionForms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 15, 34, 46, 96, DateTimeKind.Utc).AddTicks(3213),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 16, 15, 19, 58, 684, DateTimeKind.Utc).AddTicks(195));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdate",
                table: "Plants",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 15, 34, 46, 96, DateTimeKind.Utc).AddTicks(2216),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 16, 15, 19, 58, 683, DateTimeKind.Utc).AddTicks(8709));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdate",
                table: "Milestones",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 15, 34, 46, 96, DateTimeKind.Utc).AddTicks(957),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 16, 15, 19, 58, 683, DateTimeKind.Utc).AddTicks(6785));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdate",
                table: "Items",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 15, 34, 46, 95, DateTimeKind.Utc).AddTicks(9697),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 16, 15, 19, 58, 683, DateTimeKind.Utc).AddTicks(4459));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                table: "Items",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 15, 34, 46, 95, DateTimeKind.Utc).AddTicks(9322),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 16, 15, 19, 58, 683, DateTimeKind.Utc).AddTicks(3510));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdate",
                table: "Farms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 15, 34, 46, 95, DateTimeKind.Utc).AddTicks(8100),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 16, 15, 19, 58, 683, DateTimeKind.Utc).AddTicks(921));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EstablishedDate",
                table: "Farms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 15, 34, 46, 95, DateTimeKind.Utc).AddTicks(7543),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 16, 15, 19, 58, 683, DateTimeKind.Utc).AddTicks(169));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Farms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 15, 34, 46, 95, DateTimeKind.Utc).AddTicks(7789),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 16, 15, 19, 58, 683, DateTimeKind.Utc).AddTicks(517));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 15, 34, 46, 95, DateTimeKind.Utc).AddTicks(6429),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 16, 15, 19, 58, 682, DateTimeKind.Utc).AddTicks(7244));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdate",
                table: "Diaries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 15, 34, 46, 95, DateTimeKind.Utc).AddTicks(5564),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 16, 15, 19, 58, 682, DateTimeKind.Utc).AddTicks(5862));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDay",
                table: "Diaries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 15, 34, 46, 95, DateTimeKind.Utc).AddTicks(5116),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 16, 15, 19, 58, 682, DateTimeKind.Utc).AddTicks(5384));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                table: "CashFlows",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 15, 34, 46, 95, DateTimeKind.Utc).AddTicks(3839),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 16, 15, 19, 58, 682, DateTimeKind.Utc).AddTicks(3209));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Blogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 15, 34, 46, 95, DateTimeKind.Utc).AddTicks(1837),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 16, 15, 19, 58, 681, DateTimeKind.Utc).AddTicks(9105));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Blogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 15, 34, 46, 95, DateTimeKind.Utc).AddTicks(1554),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 16, 15, 19, 58, 681, DateTimeKind.Utc).AddTicks(8653));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 15, 34, 46, 95, DateTimeKind.Utc).AddTicks(707),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 16, 15, 19, 58, 681, DateTimeKind.Utc).AddTicks(7434));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "UserFunctionFarms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 15, 19, 58, 684, DateTimeKind.Utc).AddTicks(4400),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 16, 15, 34, 46, 96, DateTimeKind.Utc).AddTicks(5795));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdate",
                table: "TakeAndSendMaterials",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 15, 19, 58, 684, DateTimeKind.Utc).AddTicks(1289),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 16, 15, 34, 46, 96, DateTimeKind.Utc).AddTicks(4008));

            migrationBuilder.AlterColumn<DateTime>(
                name: "SendDate",
                table: "QuestionForms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 15, 19, 58, 684, DateTimeKind.Utc).AddTicks(195),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 16, 15, 34, 46, 96, DateTimeKind.Utc).AddTicks(3213));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdate",
                table: "Plants",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 15, 19, 58, 683, DateTimeKind.Utc).AddTicks(8709),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 16, 15, 34, 46, 96, DateTimeKind.Utc).AddTicks(2216));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdate",
                table: "Milestones",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 15, 19, 58, 683, DateTimeKind.Utc).AddTicks(6785),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 16, 15, 34, 46, 96, DateTimeKind.Utc).AddTicks(957));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdate",
                table: "Items",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 15, 19, 58, 683, DateTimeKind.Utc).AddTicks(4459),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 16, 15, 34, 46, 95, DateTimeKind.Utc).AddTicks(9697));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                table: "Items",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 15, 19, 58, 683, DateTimeKind.Utc).AddTicks(3510),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 16, 15, 34, 46, 95, DateTimeKind.Utc).AddTicks(9322));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdate",
                table: "Farms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 15, 19, 58, 683, DateTimeKind.Utc).AddTicks(921),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 16, 15, 34, 46, 95, DateTimeKind.Utc).AddTicks(8100));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EstablishedDate",
                table: "Farms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 15, 19, 58, 683, DateTimeKind.Utc).AddTicks(169),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 16, 15, 34, 46, 95, DateTimeKind.Utc).AddTicks(7543));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Farms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 15, 19, 58, 683, DateTimeKind.Utc).AddTicks(517),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 16, 15, 34, 46, 95, DateTimeKind.Utc).AddTicks(7789));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 15, 19, 58, 682, DateTimeKind.Utc).AddTicks(7244),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 16, 15, 34, 46, 95, DateTimeKind.Utc).AddTicks(6429));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdate",
                table: "Diaries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 15, 19, 58, 682, DateTimeKind.Utc).AddTicks(5862),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 16, 15, 34, 46, 95, DateTimeKind.Utc).AddTicks(5564));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDay",
                table: "Diaries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 15, 19, 58, 682, DateTimeKind.Utc).AddTicks(5384),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 16, 15, 34, 46, 95, DateTimeKind.Utc).AddTicks(5116));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                table: "CashFlows",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 15, 19, 58, 682, DateTimeKind.Utc).AddTicks(3209),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 16, 15, 34, 46, 95, DateTimeKind.Utc).AddTicks(3839));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Blogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 15, 19, 58, 681, DateTimeKind.Utc).AddTicks(9105),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 16, 15, 34, 46, 95, DateTimeKind.Utc).AddTicks(1837));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Blogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 15, 19, 58, 681, DateTimeKind.Utc).AddTicks(8653),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 16, 15, 34, 46, 95, DateTimeKind.Utc).AddTicks(1554));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 15, 19, 58, 681, DateTimeKind.Utc).AddTicks(7434),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 16, 15, 34, 46, 95, DateTimeKind.Utc).AddTicks(707));
        }
    }
}
