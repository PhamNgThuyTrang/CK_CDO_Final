using Microsoft.EntityFrameworkCore.Migrations;

namespace CK_CDO_Final.Migrations
{
    public partial class CompanyDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "KHOILUONG",
                table: "Upcom",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)");

            migrationBuilder.AlterColumn<long>(
                name: "KHOILUONG",
                table: "Index",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)");

            migrationBuilder.AlterColumn<long>(
                name: "KHOILUONG",
                table: "Hose",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)");

            migrationBuilder.AlterColumn<long>(
                name: "KHOILUONG",
                table: "Hnx",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)");

            migrationBuilder.CreateTable(
                name: "CompanyDetails",
                columns: table => new
                {
                    MA = table.Column<string>(nullable: false),
                    TEN = table.Column<string>(nullable: false),
                    NGANHNGHE = table.Column<string>(nullable: false),
                    DAIDIENPL = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDetails", x => x.MA);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Upcom_MA",
                table: "Upcom",
                column: "MA");

            migrationBuilder.CreateIndex(
                name: "IX_Hose_MA",
                table: "Hose",
                column: "MA");

            migrationBuilder.CreateIndex(
                name: "IX_Hnx_MA",
                table: "Hnx",
                column: "MA");

            migrationBuilder.AddForeignKey(
                name: "FK_Hnx_CompanyDetails_MA",
                table: "Hnx",
                column: "MA",
                principalTable: "CompanyDetails",
                principalColumn: "MA",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hose_CompanyDetails_MA",
                table: "Hose",
                column: "MA",
                principalTable: "CompanyDetails",
                principalColumn: "MA",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Upcom_CompanyDetails_MA",
                table: "Upcom",
                column: "MA",
                principalTable: "CompanyDetails",
                principalColumn: "MA",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hnx_CompanyDetails_MA",
                table: "Hnx");

            migrationBuilder.DropForeignKey(
                name: "FK_Hose_CompanyDetails_MA",
                table: "Hose");

            migrationBuilder.DropForeignKey(
                name: "FK_Upcom_CompanyDetails_MA",
                table: "Upcom");

            migrationBuilder.DropTable(
                name: "CompanyDetails");

            migrationBuilder.DropIndex(
                name: "IX_Upcom_MA",
                table: "Upcom");

            migrationBuilder.DropIndex(
                name: "IX_Hose_MA",
                table: "Hose");

            migrationBuilder.DropIndex(
                name: "IX_Hnx_MA",
                table: "Hnx");

            migrationBuilder.AlterColumn<int>(
                name: "KHOILUONG",
                table: "Upcom",
                type: "NUMBER(10)",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "KHOILUONG",
                table: "Index",
                type: "NUMBER(10)",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "KHOILUONG",
                table: "Hose",
                type: "NUMBER(10)",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "KHOILUONG",
                table: "Hnx",
                type: "NUMBER(10)",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
