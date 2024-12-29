using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SAE_API_Gestion.Migrations
{
    /// <inheritdoc />
    public partial class SaeDbMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_e_batiment_bat",
                columns: table => new
                {
                    bat_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    bat_nom = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    bat_imagedata = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_batiment_bat", x => x.bat_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_marquecapteur_mar",
                columns: table => new
                {
                    mar_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mar_nom = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_marquecapteur_mar", x => x.mar_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_positionsurface_pos",
                columns: table => new
                {
                    pos_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pos_nom = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_positionsurface_pos", x => x.pos_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_typeequipement_teq",
                columns: table => new
                {
                    teq_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    teq_nom = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_typeequipement_teq", x => x.teq_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_typesalle_typ",
                columns: table => new
                {
                    typ_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    typ_nom = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_typesalle_typ", x => x.typ_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_unitemesurer_uni",
                columns: table => new
                {
                    uni_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uni_nom = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    uni_symbole = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_unitemesurer_uni", x => x.uni_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_capteur_cap",
                columns: table => new
                {
                    cap_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mar_id = table.Column<int>(type: "integer", nullable: false),
                    cap_nom = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    cap_description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    cap_reference = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    cap_hauteur = table.Column<decimal>(type: "numeric(100,2)", precision: 100, scale: 2, nullable: false),
                    cap_longueur = table.Column<decimal>(type: "numeric(100,2)", precision: 100, scale: 2, nullable: false),
                    cap_largeur = table.Column<decimal>(type: "numeric(100,2)", precision: 100, scale: 2, nullable: false),
                    cap_imagedata = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_capteur_cap", x => x.cap_id);
                    table.ForeignKey(
                        name: "FK_t_e_capteur_cap_t_e_marquecapteur_mar_mar_id",
                        column: x => x.mar_id,
                        principalTable: "t_e_marquecapteur_mar",
                        principalColumn: "mar_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_equipement_equ",
                columns: table => new
                {
                    equ_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    teq_id = table.Column<int>(type: "integer", nullable: false),
                    equ_nom = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    equ_hauteur = table.Column<decimal>(type: "numeric(100,2)", precision: 100, scale: 2, nullable: false),
                    equ_longueur = table.Column<decimal>(type: "numeric(100,2)", precision: 100, scale: 2, nullable: false),
                    equ_largeur = table.Column<decimal>(type: "numeric(100,2)", precision: 100, scale: 2, nullable: false),
                    equ_imagedata = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_equipement_equ", x => x.equ_id);
                    table.ForeignKey(
                        name: "FK_t_e_equipement_equ_t_e_typeequipement_teq_teq_id",
                        column: x => x.teq_id,
                        principalTable: "t_e_typeequipement_teq",
                        principalColumn: "teq_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_salle_sal",
                columns: table => new
                {
                    sal_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    bat_id = table.Column<int>(type: "integer", nullable: false),
                    typ_id = table.Column<int>(type: "integer", nullable: false),
                    sal_nom = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    sal_imagedata = table.Column<byte[]>(type: "bytea", nullable: true),
                    sal_capacite = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_salle_sal", x => x.sal_id);
                    table.ForeignKey(
                        name: "FK_t_e_salle_sal_t_e_batiment_bat_bat_id",
                        column: x => x.bat_id,
                        principalTable: "t_e_batiment_bat",
                        principalColumn: "bat_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_e_salle_sal_t_e_typesalle_typ_typ_id",
                        column: x => x.typ_id,
                        principalTable: "t_e_typesalle_typ",
                        principalColumn: "typ_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_a_capteur_unitemesurer_acu",
                columns: table => new
                {
                    uni_id = table.Column<int>(type: "integer", nullable: false),
                    cap_id = table.Column<int>(type: "integer", nullable: false),
                    acu_plagemin = table.Column<int>(type: "integer", nullable: true),
                    acu_plagemax = table.Column<int>(type: "integer", nullable: true),
                    acu_precision = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_a_capteur_unitemesurer_acu", x => new { x.uni_id, x.cap_id });
                    table.ForeignKey(
                        name: "FK_t_a_capteur_unitemesurer_acu_t_e_capteur_cap_cap_id",
                        column: x => x.cap_id,
                        principalTable: "t_e_capteur_cap",
                        principalColumn: "cap_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_a_capteur_unitemesurer_acu_t_e_unitemesurer_uni_uni_id",
                        column: x => x.uni_id,
                        principalTable: "t_e_unitemesurer_uni",
                        principalColumn: "uni_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_surface_sur",
                columns: table => new
                {
                    sur_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sal_id = table.Column<int>(type: "integer", nullable: false),
                    pos_id = table.Column<int>(type: "integer", nullable: false),
                    sur_longueur = table.Column<decimal>(type: "numeric(100,2)", precision: 100, scale: 2, nullable: false),
                    sur_hauteur = table.Column<decimal>(type: "numeric(100,2)", precision: 100, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_surface_sur", x => x.sur_id);
                    table.ForeignKey(
                        name: "FK_t_e_surface_sur_t_e_positionsurface_pos_pos_id",
                        column: x => x.pos_id,
                        principalTable: "t_e_positionsurface_pos",
                        principalColumn: "pos_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_e_surface_sur_t_e_salle_sal_sal_id",
                        column: x => x.sal_id,
                        principalTable: "t_e_salle_sal",
                        principalColumn: "sal_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_capteurinstalle_cin",
                columns: table => new
                {
                    cin_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sur_id = table.Column<int>(type: "integer", nullable: false),
                    cap_id = table.Column<int>(type: "integer", nullable: false),
                    sal_id = table.Column<int>(type: "integer", nullable: false),
                    cin_posx = table.Column<decimal>(type: "numeric(100,2)", precision: 100, scale: 2, nullable: false),
                    cin_posy = table.Column<decimal>(type: "numeric(100,2)", precision: 100, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_capteurinstalle_cin", x => x.cin_id);
                    table.ForeignKey(
                        name: "FK_t_e_capteurinstalle_cin_t_e_capteur_cap_cap_id",
                        column: x => x.cap_id,
                        principalTable: "t_e_capteur_cap",
                        principalColumn: "cap_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_e_capteurinstalle_cin_t_e_salle_sal_sal_id",
                        column: x => x.sal_id,
                        principalTable: "t_e_salle_sal",
                        principalColumn: "sal_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_e_capteurinstalle_cin_t_e_surface_sur_sur_id",
                        column: x => x.sur_id,
                        principalTable: "t_e_surface_sur",
                        principalColumn: "sur_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_equipementinstalle_ein",
                columns: table => new
                {
                    ein_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sal_id = table.Column<int>(type: "integer", nullable: false),
                    equ_id = table.Column<int>(type: "integer", nullable: false),
                    sur_id = table.Column<int>(type: "integer", nullable: false),
                    ein_posx = table.Column<decimal>(type: "numeric(100,2)", precision: 100, scale: 2, nullable: false),
                    ein_posy = table.Column<decimal>(type: "numeric(100,2)", precision: 100, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_equipementinstalle_ein", x => x.ein_id);
                    table.ForeignKey(
                        name: "FK_t_e_equipementinstalle_ein_t_e_equipement_equ_equ_id",
                        column: x => x.equ_id,
                        principalTable: "t_e_equipement_equ",
                        principalColumn: "equ_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_e_equipementinstalle_ein_t_e_salle_sal_sal_id",
                        column: x => x.sal_id,
                        principalTable: "t_e_salle_sal",
                        principalColumn: "sal_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_e_equipementinstalle_ein_t_e_surface_sur_sur_id",
                        column: x => x.sur_id,
                        principalTable: "t_e_surface_sur",
                        principalColumn: "sur_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_a_capteur_unitemesurer_acu_cap_id",
                table: "t_a_capteur_unitemesurer_acu",
                column: "cap_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_capteur_cap_mar_id",
                table: "t_e_capteur_cap",
                column: "mar_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_capteurinstalle_cin_cap_id",
                table: "t_e_capteurinstalle_cin",
                column: "cap_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_capteurinstalle_cin_sal_id",
                table: "t_e_capteurinstalle_cin",
                column: "sal_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_capteurinstalle_cin_sur_id",
                table: "t_e_capteurinstalle_cin",
                column: "sur_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_equipement_equ_teq_id",
                table: "t_e_equipement_equ",
                column: "teq_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_equipementinstalle_ein_equ_id",
                table: "t_e_equipementinstalle_ein",
                column: "equ_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_equipementinstalle_ein_sal_id",
                table: "t_e_equipementinstalle_ein",
                column: "sal_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_equipementinstalle_ein_sur_id",
                table: "t_e_equipementinstalle_ein",
                column: "sur_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_salle_sal_bat_id",
                table: "t_e_salle_sal",
                column: "bat_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_salle_sal_typ_id",
                table: "t_e_salle_sal",
                column: "typ_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_surface_sur_pos_id",
                table: "t_e_surface_sur",
                column: "pos_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_surface_sur_sal_id",
                table: "t_e_surface_sur",
                column: "sal_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_a_capteur_unitemesurer_acu");

            migrationBuilder.DropTable(
                name: "t_e_capteurinstalle_cin");

            migrationBuilder.DropTable(
                name: "t_e_equipementinstalle_ein");

            migrationBuilder.DropTable(
                name: "t_e_unitemesurer_uni");

            migrationBuilder.DropTable(
                name: "t_e_capteur_cap");

            migrationBuilder.DropTable(
                name: "t_e_equipement_equ");

            migrationBuilder.DropTable(
                name: "t_e_surface_sur");

            migrationBuilder.DropTable(
                name: "t_e_marquecapteur_mar");

            migrationBuilder.DropTable(
                name: "t_e_typeequipement_teq");

            migrationBuilder.DropTable(
                name: "t_e_positionsurface_pos");

            migrationBuilder.DropTable(
                name: "t_e_salle_sal");

            migrationBuilder.DropTable(
                name: "t_e_batiment_bat");

            migrationBuilder.DropTable(
                name: "t_e_typesalle_typ");
        }
    }
}
