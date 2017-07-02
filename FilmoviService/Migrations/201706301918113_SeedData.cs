namespace FilmoviService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedData : DbMigration
    {
        public override void Up()
        {
            // Seed "Reziseri" table
            Sql(@"INSERT INTO Reziseri (Ime, Prezime, Starost) VALUES (N'Christopher', N'Nolan', 47 );
                INSERT INTO Reziseri (Ime, Prezime, Starost) VALUES (N'Guy', N'Ritchie', 49 );
                INSERT INTO Reziseri (Ime, Prezime, Starost) VALUES (N'Quentin', N'Tarantino', 54 );

                ");

            // Seed "Filmovi" table
            Sql(@"INSERT INTO Filmovi (Naziv, Zanr, Godina, ReziserId) VALUES (N'Memento', N'Thriller', 2000, 1 );
                INSERT INTO Filmovi (Naziv, Zanr, Godina, ReziserId) VALUES (N'The Prestige', N'Mystery', 2006, 1 );
                INSERT INTO Filmovi (Naziv, Zanr, Godina, ReziserId) VALUES (N'Lock, Stock and Two Smoking Barrels', N'Crime', 1998, 2 );
                INSERT INTO Filmovi (Naziv, Zanr, Godina, ReziserId) VALUES (N'Sherlock Holmes', N'Adventure', 2009, 2 );
                INSERT INTO Filmovi (Naziv, Zanr, Godina, ReziserId) VALUES (N'Inglourious Basterds', N'War', 2009, 3 );
                INSERT INTO Filmovi (Naziv, Zanr, Godina, ReziserId) VALUES (N'Pulp Fiction', N'Crime', 1994, 3 );
                ");
        }
        
        public override void Down()
        {
        }
    }
}
