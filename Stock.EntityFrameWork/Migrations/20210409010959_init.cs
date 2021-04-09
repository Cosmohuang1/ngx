using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stock.EntityFrameWork.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AbuQuantModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: true),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    FW = table.Column<int>(type: "int", nullable: false),
                    KC = table.Column<int>(type: "int", nullable: false),
                    BS = table.Column<int>(type: "int", nullable: false),
                    MC = table.Column<int>(type: "int", nullable: false),
                    MP = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbuQuantModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Board",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CagegoryId = table.Column<string>(type: "longtext", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    BrokerSource = table.Column<int>(type: "int", nullable: false),
                    BoardCategory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Board", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BoardToStocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CagegoryId = table.Column<string>(type: "longtext", nullable: true),
                    StockCode = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardToStocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsActived = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OptionalPool",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "varchar(6)", maxLength: 6, nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionalPool", x => new { x.CategoryId, x.Code });
                });

            migrationBuilder.CreateTable(
                name: "StockComment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Code = table.Column<string>(type: "longtext", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    ChangePercent = table.Column<string>(type: "longtext", nullable: true),
                    PERation = table.Column<string>(type: "longtext", nullable: true),
                    TurnoverRate = table.Column<string>(type: "longtext", nullable: true),
                    ZLCB = table.Column<string>(type: "longtext", nullable: true),
                    ZLCB20R = table.Column<string>(type: "longtext", nullable: true),
                    ZLCB60R = table.Column<string>(type: "longtext", nullable: true),
                    JGCYD = table.Column<string>(type: "longtext", nullable: true),
                    JGCYDType = table.Column<string>(type: "longtext", nullable: true),
                    FLOWINXL = table.Column<string>(type: "longtext", nullable: true),
                    FLOWOUTXL = table.Column<string>(type: "longtext", nullable: true),
                    FLOWINL = table.Column<string>(type: "longtext", nullable: true),
                    FLOWOUTL = table.Column<string>(type: "longtext", nullable: true),
                    ZLJLR = table.Column<string>(type: "longtext", nullable: true),
                    Market = table.Column<string>(type: "longtext", nullable: true),
                    TotalScore = table.Column<string>(type: "longtext", nullable: true),
                    RankingUp = table.Column<string>(type: "longtext", nullable: true),
                    Ranking = table.Column<string>(type: "longtext", nullable: true),
                    Focus = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockComment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockEntity",
                columns: table => new
                {
                    Code = table.Column<string>(type: "varchar(6)", maxLength: 6, nullable: false),
                    Name = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockEntity", x => x.Code);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbuQuantModel");

            migrationBuilder.DropTable(
                name: "Board");

            migrationBuilder.DropTable(
                name: "BoardToStocks");

            migrationBuilder.DropTable(
                name: "CustomCategory");

            migrationBuilder.DropTable(
                name: "OptionalPool");

            migrationBuilder.DropTable(
                name: "StockComment");

            migrationBuilder.DropTable(
                name: "StockEntity");
        }
    }
}
