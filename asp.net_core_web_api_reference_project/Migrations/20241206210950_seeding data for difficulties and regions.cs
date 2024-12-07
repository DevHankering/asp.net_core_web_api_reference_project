using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace asp.net_core_web_api_reference_project.Migrations
{
    /// <inheritdoc />
    public partial class seedingdatafordifficultiesandregions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("10d6c1fe-e783-4649-88b6-a1d895c04a34"), "Hard" },
                    { new Guid("cd4e2ab8-c12e-4ecd-aa38-2cb05f65f518"), "Easy" },
                    { new Guid("e8cfd57f-247e-416a-bd2c-7c71dd0ccc32"), "Medium" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("2570dcb7-1bd1-47b1-bc02-8bbcd82f2d08"), "NOIDA", "NOIDA", null },
                    { new Guid("51b3adc3-d4b1-4e3b-8d94-a3d623b56c82"), "RBL", "RAEBARELI", "https://cdn.s3waas.gov.in/s3e3796ae838835da0b6f6ea37bcf8bcb7/uploads/2018/07/2018072687.jpg" },
                    { new Guid("757c096c-97e9-4cb1-8b80-f212b140c12c"), "DL", "DELHI", "https://deih43ym53wif.cloudfront.net/Rajpath-delhi-shutterstock_1195751923.jpg_7647e1aad2.jpg" },
                    { new Guid("f347a2a8-978b-4c5a-89c4-62f01ed716f2"), "LKO", "LUCKNOW", "https://t4.ftcdn.net/jpg/05/13/77/31/240_F_513773104_G7Pin2bxWwpMAWqI5MIvrSnWDpYs80WN.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("10d6c1fe-e783-4649-88b6-a1d895c04a34"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("cd4e2ab8-c12e-4ecd-aa38-2cb05f65f518"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("e8cfd57f-247e-416a-bd2c-7c71dd0ccc32"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("2570dcb7-1bd1-47b1-bc02-8bbcd82f2d08"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("51b3adc3-d4b1-4e3b-8d94-a3d623b56c82"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("757c096c-97e9-4cb1-8b80-f212b140c12c"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f347a2a8-978b-4c5a-89c4-62f01ed716f2"));
        }
    }
}
