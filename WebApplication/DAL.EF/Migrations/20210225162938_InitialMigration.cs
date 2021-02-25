using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.EF.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarAccessTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarAccessTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarErrorCodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CanId = table.Column<int>(type: "int", nullable: false),
                    CanData = table.Column<long>(type: "bigint", nullable: false),
                    DetectedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarErrorCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarMarks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarMarks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarMarkId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarTypes_CarMarks_CarMarkId",
                        column: x => x.CarMarkId,
                        principalTable: "CarMarks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CarModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CarTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarModels_CarTypes_CarTypeId",
                        column: x => x.CarTypeId,
                        principalTable: "CarTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CarModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_CarModels_CarModelId",
                        column: x => x.CarModelId,
                        principalTable: "CarModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CarAccesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CarId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CarAccessTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarAccesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarAccesses_CarAccessTypes_CarAccessTypeId",
                        column: x => x.CarAccessTypeId,
                        principalTable: "CarAccessTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CarAccesses_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GasRefills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmountRefilled = table.Column<float>(type: "real", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cost = table.Column<float>(type: "real", nullable: false),
                    CarId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GasRefills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GasRefills_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Distance = table.Column<float>(type: "real", nullable: false),
                    CarId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tracks_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrackLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Lat = table.Column<double>(type: "float", nullable: false),
                    Lng = table.Column<double>(type: "float", nullable: false),
                    Elevation = table.Column<float>(type: "real", nullable: false),
                    Accuracy = table.Column<float>(type: "real", nullable: false),
                    ElevationAccuracy = table.Column<float>(type: "real", nullable: false),
                    Speed = table.Column<float>(type: "real", nullable: false),
                    Rpm = table.Column<float>(type: "real", nullable: false),
                    TrackId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrackLocations_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarAccesses_CarAccessTypeId",
                table: "CarAccesses",
                column: "CarAccessTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CarAccesses_CarId",
                table: "CarAccesses",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_CarModels_CarTypeId",
                table: "CarModels",
                column: "CarTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarModelId",
                table: "Cars",
                column: "CarModelId");

            migrationBuilder.CreateIndex(
                name: "IX_CarTypes_CarMarkId",
                table: "CarTypes",
                column: "CarMarkId");

            migrationBuilder.CreateIndex(
                name: "IX_GasRefills_CarId",
                table: "GasRefills",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackLocations_TrackId",
                table: "TrackLocations",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_CarId",
                table: "Tracks",
                column: "CarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarAccesses");

            migrationBuilder.DropTable(
                name: "CarErrorCodes");

            migrationBuilder.DropTable(
                name: "GasRefills");

            migrationBuilder.DropTable(
                name: "TrackLocations");

            migrationBuilder.DropTable(
                name: "CarAccessTypes");

            migrationBuilder.DropTable(
                name: "Tracks");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "CarModels");

            migrationBuilder.DropTable(
                name: "CarTypes");

            migrationBuilder.DropTable(
                name: "CarMarks");
        }
    }
}
