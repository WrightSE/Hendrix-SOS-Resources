using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOSResources.Migrations.ResourcesMigrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Participant",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participant", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Resource",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Type = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Textbook",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    AuthorLastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Edition = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    CheckedOut = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Textbook", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ResourceRequest",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RequestDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ParticipantID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceRequest", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ResourceRequest_Participant_ParticipantID",
                        column: x => x.ParticipantID,
                        principalTable: "Participant",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TextbookRequest",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RequestDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ParticipantID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextbookRequest", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TextbookRequest_Participant_ParticipantID",
                        column: x => x.ParticipantID,
                        principalTable: "Participant",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResourceResourceRequest",
                columns: table => new
                {
                    ResourceRequestsID = table.Column<int>(type: "INTEGER", nullable: false),
                    ResourcesID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceResourceRequest", x => new { x.ResourceRequestsID, x.ResourcesID });
                    table.ForeignKey(
                        name: "FK_ResourceResourceRequest_ResourceRequest_ResourceRequestsID",
                        column: x => x.ResourceRequestsID,
                        principalTable: "ResourceRequest",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceResourceRequest_Resource_ResourcesID",
                        column: x => x.ResourcesID,
                        principalTable: "Resource",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TextbookTextbookRequest",
                columns: table => new
                {
                    TextbookID = table.Column<int>(type: "INTEGER", nullable: false),
                    TextbookRequestsID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextbookTextbookRequest", x => new { x.TextbookID, x.TextbookRequestsID });
                    table.ForeignKey(
                        name: "FK_TextbookTextbookRequest_TextbookRequest_TextbookRequestsID",
                        column: x => x.TextbookRequestsID,
                        principalTable: "TextbookRequest",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TextbookTextbookRequest_Textbook_TextbookID",
                        column: x => x.TextbookID,
                        principalTable: "Textbook",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResourceRequest_ParticipantID",
                table: "ResourceRequest",
                column: "ParticipantID");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceResourceRequest_ResourcesID",
                table: "ResourceResourceRequest",
                column: "ResourcesID");

            migrationBuilder.CreateIndex(
                name: "IX_TextbookRequest_ParticipantID",
                table: "TextbookRequest",
                column: "ParticipantID");

            migrationBuilder.CreateIndex(
                name: "IX_TextbookTextbookRequest_TextbookRequestsID",
                table: "TextbookTextbookRequest",
                column: "TextbookRequestsID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResourceResourceRequest");

            migrationBuilder.DropTable(
                name: "TextbookTextbookRequest");

            migrationBuilder.DropTable(
                name: "ResourceRequest");

            migrationBuilder.DropTable(
                name: "Resource");

            migrationBuilder.DropTable(
                name: "TextbookRequest");

            migrationBuilder.DropTable(
                name: "Textbook");

            migrationBuilder.DropTable(
                name: "Participant");
        }
    }
}
