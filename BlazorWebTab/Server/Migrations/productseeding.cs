using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlazorWebTab.Server.Migrations
{
    /// <inheritdoc />
    public partial class productseeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "Price", "Title" },
                values: new object[,]
                {
                    { 1, "Rok temu: Życie Elli było idealne. Miała cudownych przyjaciół i niesamowitego chłopaka. Wszystko się zmieniło w momencie wypadku samochodowego. Kiedy Ella obudziła się w szpitalu, nie pamiętała ani tragicznego wydarzenia, ani tygodni przed nim. Nie rozumiała też, dlaczego zerwała ze swoim chłopakiem.", "https://cf-taniaksiazka.statiki.pl/images/large/F00/9788367247054.jpg", 19.95m, "11 papierowych serc" },
                    { 2, "Pragniesz nieoczywistej historii, która wzbudzi w Tobie wiele emocji? W takim razie Flaw(less). Opowiedz mi naszą historię autorstwa Marty Łabeckiej jest tytułem właśnie dla Ciebie!", "https://cf-taniaksiazka.statiki.pl/images/large/ECC/9788328377431.jpg", 25.84m, "Opowiedz mi naszą historię. Flaw" },
                    { 3, "Rozpoczęło się pospieszne gromadzenie wojsk, jednak zadanie jest ponad nasze siły. Aby pozyskać sojuszników, ziemskie legiony zostają wysłane na Świat Krwi.", "https://cf-taniaksiazka.statiki.pl/images/large/181/978836705331033.jpg", 22.45m, "Świat krwi. Legion Nieśmiertelnych" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
