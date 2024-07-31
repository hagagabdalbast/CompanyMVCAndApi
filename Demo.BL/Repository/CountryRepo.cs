using Demo.BL.Interface;
using Demo.DAL.Entity;

namespace Demo.BL.Repository
{
    public class CountryRepo : ICountryRepo
    {
        private readonly DemoContext db;

        public CountryRepo(DemoContext db)
        {
            this.db = db;
        }
        public IEnumerable<Country> Get()
        {
            var data = db.Country.Select(a => a);
            return data;
        }

        public Country GetById(int id)
        {
            var data = db.Country.Where(a => a.Id == id).FirstOrDefault();
            return data;
        }
    }
}
