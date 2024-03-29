﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WsApp.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArmorSelections",
                columns: table => new
                {
                    ArmorSelectionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SocketId = table.Column<string>(nullable: true),
                    Selected = table.Column<bool>(nullable: false),
                    ArmorSize = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArmorSelections", x => x.ArmorSelectionId);
                });

            migrationBuilder.CreateTable(
                name: "BattleArenas",
                columns: table => new
                {
                    BattleArenaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PlayerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BattleArenas", x => x.BattleArenaId);
                });

            migrationBuilder.CreateTable(
                name: "Commands",
                columns: table => new
                {
                    PlacementCommandId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SocketId = table.Column<string>(nullable: true),
                    IsArmor = table.Column<bool>(nullable: false),
                    SelectionShipSelectionId = table.Column<int>(nullable: false),
                    SelectionCount = table.Column<int>(nullable: false),
                    SelectionSize = table.Column<int>(nullable: false),
                    SelectionButtonId = table.Column<string>(nullable: true),
                    SelectionIsSelected = table.Column<bool>(nullable: false),
                    SelectionShipTypeId = table.Column<int>(nullable: false),
                    SelectionButton0IsRemoved = table.Column<bool>(nullable: false),
                    SelectionButton1IsRemoved = table.Column<bool>(nullable: false),
                    SelectionButton2IsRemoved = table.Column<bool>(nullable: false),
                    SelectionButton3IsRemoved = table.Column<bool>(nullable: false),
                    SelectionButton4IsRemoved = table.Column<bool>(nullable: false),
                    CellId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commands", x => x.PlacementCommandId);
                });

            migrationBuilder.CreateTable(
                name: "Duels",
                columns: table => new
                {
                    DuelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PlayerTurnId = table.Column<string>(nullable: true),
                    FirstPlayerBAId = table.Column<int>(nullable: false),
                    SecondPlayerBAId = table.Column<int>(nullable: false),
                    FirstPlayerSocketId = table.Column<string>(nullable: true),
                    SecondPlayerSocketId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duels", x => x.DuelId);
                });

            migrationBuilder.CreateTable(
                name: "HitStreaks",
                columns: table => new
                {
                    HitStreakId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SocketId = table.Column<string>(nullable: true),
                    Streak = table.Column<int>(nullable: false),
                    ReachGoal = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HitStreaks", x => x.HitStreakId);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(nullable: true),
                    Socket = table.Column<string>(nullable: true),
                    Turn = table.Column<bool>(nullable: false),
                    Timer = table.Column<DateTime>(nullable: false),
                    BattleArenaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerId);
                });

            migrationBuilder.CreateTable(
                name: "Ships",
                columns: table => new
                {
                    ShipId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    RemainingTiles = table.Column<int>(nullable: false),
                    SocketId = table.Column<string>(nullable: true),
                    ShipTypeId = table.Column<int>(nullable: false),
                    CellId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ships", x => x.ShipId);
                });

            migrationBuilder.CreateTable(
                name: "ShipTypes",
                columns: table => new
                {
                    ShipTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(nullable: true),
                    Size = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    ShipId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipTypes", x => x.ShipTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Cells",
                columns: table => new
                {
                    CellId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsHit = table.Column<bool>(nullable: false),
                    IsArmored = table.Column<bool>(nullable: false),
                    PosX = table.Column<int>(nullable: false),
                    PosY = table.Column<int>(nullable: false),
                    ShipId = table.Column<int>(nullable: false),
                    BattleArenaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cells", x => x.CellId);
                    table.ForeignKey(
                        name: "FK_Cells_BattleArenas_BattleArenaId",
                        column: x => x.BattleArenaId,
                        principalTable: "BattleArenas",
                        principalColumn: "BattleArenaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShipSelections",
                columns: table => new
                {
                    ShipSelectionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Count = table.Column<int>(nullable: false),
                    Size = table.Column<int>(nullable: false),
                    ButtonId = table.Column<string>(nullable: true),
                    IsSelected = table.Column<bool>(nullable: false),
                    Button0IsRemoved = table.Column<bool>(nullable: false),
                    Button1IsRemoved = table.Column<bool>(nullable: false),
                    Button2IsRemoved = table.Column<bool>(nullable: false),
                    Button3IsRemoved = table.Column<bool>(nullable: false),
                    Button4IsRemoved = table.Column<bool>(nullable: false),
                    PlayerId = table.Column<int>(nullable: true),
                    ShipTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipSelections", x => x.ShipSelectionId);
                    table.ForeignKey(
                        name: "FK_ShipSelections_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cells_BattleArenaId",
                table: "Cells",
                column: "BattleArenaId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipSelections_PlayerId",
                table: "ShipSelections",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArmorSelections");

            migrationBuilder.DropTable(
                name: "Cells");

            migrationBuilder.DropTable(
                name: "Commands");

            migrationBuilder.DropTable(
                name: "Duels");

            migrationBuilder.DropTable(
                name: "HitStreaks");

            migrationBuilder.DropTable(
                name: "Ships");

            migrationBuilder.DropTable(
                name: "ShipSelections");

            migrationBuilder.DropTable(
                name: "ShipTypes");

            migrationBuilder.DropTable(
                name: "BattleArenas");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
