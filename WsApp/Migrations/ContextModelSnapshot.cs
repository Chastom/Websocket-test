﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WsApp.Models;

namespace WsApp.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WsApp.Models.BattleArena", b =>
                {
                    b.Property<int>("BattleArenaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PlayerId");

                    b.HasKey("BattleArenaId");

                    b.ToTable("BattleArenas");
                });

            modelBuilder.Entity("WsApp.Models.Cell", b =>
                {
                    b.Property<int>("CellId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BattleArenaId");

                    b.Property<bool>("IsArmored");

                    b.Property<bool>("IsHit");

                    b.Property<int>("PosX");

                    b.Property<int>("PosY");

                    b.Property<int>("ShipId");

                    b.HasKey("CellId");

                    b.HasIndex("BattleArenaId");

                    b.ToTable("Cells");
                });

            modelBuilder.Entity("WsApp.Models.Player", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BattleArenaId");

                    b.Property<string>("Socket");

                    b.Property<DateTime>("Timer");

                    b.Property<bool>("Turn");

                    b.Property<string>("Username");

                    b.HasKey("PlayerId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("WsApp.Models.Ship", b =>
                {
                    b.Property<int>("ShipId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CellId");

                    b.Property<string>("Name");

                    b.Property<int>("RemainingTiles");

                    b.Property<int>("ShipTypeId");

                    b.HasKey("ShipId");

                    b.ToTable("Ships");
                });

            modelBuilder.Entity("WsApp.Models.ShipType", b =>
                {
                    b.Property<int>("ShipTypeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count");

                    b.Property<int>("ShipId");

                    b.Property<int>("Size");

                    b.Property<string>("Type");

                    b.HasKey("ShipTypeId");

                    b.ToTable("ShipTypes");
                });

            modelBuilder.Entity("WsApp.Models.Cell", b =>
                {
                    b.HasOne("WsApp.Models.BattleArena")
                        .WithMany("Cells")
                        .HasForeignKey("BattleArenaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
