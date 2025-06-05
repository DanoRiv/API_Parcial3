using ParcialAPI.DAL.Entities;

namespace ParcialAPI.DAL
{
    public class SeederDB
    {
        private readonly DataBaseContext _context;
        public SeederDB(DataBaseContext context)
        {
            _context = context;
        }

        public async Task SeederAsync()
        {
            // does the same as update-database, create the database 
            await _context.Database.EnsureCreatedAsync();

            //Methods to populate the database with initial data
            await PopulateCountriesAsync();

        }

        #region Private Methods
        private async Task PopulateCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                var countries = new List<Country>
                {
                    new Country { 
                        Name = "Chile", 
                        CreatedDate = DateTime.Now, 
                        States = new List<State>()
                        {
                            new State { Name = "Santiago", CreatedDate = DateTime.Now },
                            new State { Name = "Valparaíso", CreatedDate = DateTime.Now },
                            new State { Name = "Concepción", CreatedDate = DateTime.Now }
                        } 
                    },
                    new Country { 
                        Name = "Canada",
                        CreatedDate = DateTime.Now,
                        States = new List<State>()
                        {
                            new State { Name = "Ontario", CreatedDate = DateTime.Now },
                            new State { Name = "Quebec", CreatedDate = DateTime.Now },
                            new State { Name = "British Columbia", CreatedDate = DateTime.Now }
                        }

                    },
                    new Country {   
                        Name = "Mexico", 
                        CreatedDate = DateTime.Now,
                        States = new List<State>()
                        {
                            new State { Name = "Jalisco", CreatedDate = DateTime.Now },
                            new State { Name = "Nuevo León", CreatedDate = DateTime.Now },
                            new State { Name = "Yucatán", CreatedDate = DateTime.Now }
                        }
                    }
                };

                await _context.Countries.AddRangeAsync(countries);
                await _context.SaveChangesAsync();
            }
        }
        #endregion
    }
}
