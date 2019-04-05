using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SacramentMeetingPlanner.Data.Migrations
{
    public partial class DataModelFirstDraft : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meeting",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MeetingDate = table.Column<DateTime>(nullable: false),
                    Conducting = table.Column<string>(maxLength: 255, nullable: false),
                    Invocation = table.Column<string>(maxLength: 255, nullable: false),
                    OpeningSong = table.Column<string>(maxLength: 255, nullable: false),
                    SacramentSong = table.Column<string>(maxLength: 255, nullable: false),
                    IntermediateSong = table.Column<string>(maxLength: 255, nullable: true),
                    ClosingSong = table.Column<string>(maxLength: 255, nullable: false),
                    Benediction = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meeting", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Speaker",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MeetingID = table.Column<int>(nullable: false),
                    SpeakerName = table.Column<string>(maxLength: 255, nullable: false),
                    Subject = table.Column<string>(maxLength: 255, nullable: false),
                    Block = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speaker", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Speaker_Meeting_MeetingID",
                        column: x => x.MeetingID,
                        principalTable: "Meeting",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Speaker_MeetingID",
                table: "Speaker",
                column: "MeetingID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "Speaker");

            migrationBuilder.DropTable(
                name: "Meeting");
        }
    }
}
