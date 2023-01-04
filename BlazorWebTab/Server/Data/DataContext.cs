using BlazorWebTab.Shared;

namespace BlazorWebTab.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> contextOptions) : base (contextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData
            (
                new Product
                {
                    Id = 1,
                    Title = "11 papierowych serc",
                    Description =
                        "Rok temu: Życie Elli było idealne. Miała cudownych przyjaciół i niesamowitego chłopaka. Wszystko się zmieniło w momencie wypadku samochodowego. Kiedy Ella obudziła się w szpitalu, nie pamiętała ani tragicznego wydarzenia, ani tygodni przed nim. Nie rozumiała też, dlaczego zerwała ze swoim chłopakiem.",
                    Price = 19.95m,
                    ImageUrl = "https://cf-taniaksiazka.statiki.pl/images/large/F00/9788367247054.jpg"
                },
                new Product
                {
                    Id = 2,
                    Title = "Opowiedz mi naszą historię. Flaw",
                    Description =
                        "Pragniesz nieoczywistej historii, która wzbudzi w Tobie wiele emocji? W takim razie Flaw(less). Opowiedz mi naszą historię autorstwa Marty Łabeckiej jest tytułem właśnie dla Ciebie!",
                    Price = 25.84m,
                    ImageUrl = "https://cf-taniaksiazka.statiki.pl/images/large/ECC/9788328377431.jpg"
                },
                new Product
                {
                    Id = 3,
                    Title = "Świat krwi. Legion Nieśmiertelnych",
                    Description =
                        "Rozpoczęło się pospieszne gromadzenie wojsk, jednak zadanie jest ponad nasze siły. Aby pozyskać sojuszników, ziemskie legiony zostają wysłane na Świat Krwi.",
                    Price = 22.45m,
                    ImageUrl = "https://cf-taniaksiazka.statiki.pl/images/large/181/978836705331033.jpg"
                }
            );
        }

        public DbSet<Product> Products { get; set; }
    }
}
