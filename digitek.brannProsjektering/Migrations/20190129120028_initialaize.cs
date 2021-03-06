﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace digitek.brannProsjektering.Migrations
{
    public partial class initialaize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UseRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Model = table.Column<string>(nullable: true),
                    InputJson = table.Column<string>(nullable: true),
                    ResponseCode = table.Column<int>(nullable: false),
                    ResponseText = table.Column<string>(nullable: true),
                    Navn = table.Column<string>(nullable: true),
                    Organisasjonsnummer = table.Column<string>(nullable: true),
                    OrganisasjonsNavn = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ExecutionNr = table.Column<string>(nullable: true),
                    Kapitel = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UseRecords", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UseRecords");
        }
    }
}
