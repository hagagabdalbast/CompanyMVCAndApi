using Demo.BL.Interface;
using Demo.DAL.Entity;
using System.Linq.Expressions;

namespace Demo.BL.Repository
{
    public class CityRepo : ICityRepo
    {

        private readonly DemoContext db;

        public CityRepo(DemoContext db)
        {
            this.db = db;
        }

        public IEnumerable<City> Get(Expression<Func<City, bool>> filter = null)
        {
            

            if (filter == null)
            {
                var data = db.City.Select(a => a);
                return data;
            }
            else
            {
                return db.City.Where(filter);
            }

            
        }

        

        public City GetById(int id)
        {
            var data = db.City.Where(a => a.Id == id).FirstOrDefault();
            return data;
        }
    }
}
