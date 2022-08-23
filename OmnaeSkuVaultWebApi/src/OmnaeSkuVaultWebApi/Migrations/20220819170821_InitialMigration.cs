using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OmnaeSkuVaultWebApi.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "finance");

            migrationBuilder.CreateTable(
                name: "sku_vault_accounts",
                schema: "finance",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sku_vault_id = table.Column<int>(type: "int", nullable: false),
                    sku_vault_token_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    is_vendor = table.Column<bool>(type: "bit", nullable: false),
                    created_on = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    last_modified_on = table.Column<DateTime>(type: "datetime2", nullable: true),
                    last_modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sku_vault_accounts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sku_vault_tokens",
                schema: "finance",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tenant_token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    user_token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    sku_vault_company_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_revoked = table.Column<bool>(type: "bit", nullable: false),
                    created_on = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    last_modified_on = table.Column<DateTime>(type: "datetime2", nullable: true),
                    last_modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sku_vault_tokens", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sku_vault_accounts");

            migrationBuilder.DropTable(
                name: "sku_vault_tokens");
        }
    }
}
