using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAlquiler_DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeMotorcycles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeMotorcycles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GasolineConsumption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LuggageCapacity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassengerCapacity = table.Column<int>(type: "int", nullable: false),
                    Fuel = table.Column<int>(type: "int", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    ModelID = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Whell = table.Column<int>(type: "int", nullable: true),
                    FrameSize = table.Column<int>(type: "int", nullable: true),
                    NumberSpeeds = table.Column<int>(type: "int", nullable: true),
                    Dimension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Engine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stability = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Navigation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Facilities = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Boat_Sound = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Accessories = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Propulsion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberDoors = table.Column<int>(type: "int", nullable: true),
                    AirConditioning = table.Column<bool>(type: "bit", nullable: true),
                    Transmission = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Airbag = table.Column<bool>(type: "bit", nullable: true),
                    Car_Abs = table.Column<bool>(type: "bit", nullable: true),
                    Sound = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EngineLiters = table.Column<float>(type: "real", nullable: true),
                    Abs = table.Column<bool>(type: "bit", nullable: true),
                    cilindrada = table.Column<int>(type: "int", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: true),
                    TypeMotorcycleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehicles_Models_ModelID",
                        column: x => x.ModelID,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehicles_TypeMotorcycles_TypeMotorcycleId",
                        column: x => x.TypeMotorcycleId,
                        principalTable: "TypeMotorcycles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_BrandId",
                table: "Vehicles",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_ModelID",
                table: "Vehicles",
                column: "ModelID");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_TypeMotorcycleId",
                table: "Vehicles",
                column: "TypeMotorcycleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "TypeMotorcycles");
        }
    }
}
