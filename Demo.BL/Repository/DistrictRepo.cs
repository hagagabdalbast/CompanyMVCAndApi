using Demo.BL.Interface;
using Demo.DAL.Entity;
using System.Linq.Expressions;

namespace Demo.BL.Repository
{
    public class DistrictRepo : IDistrictRepo
    {

        private readonly DemoContext db;

        public DistrictRepo(DemoContext db)
        {
            this.db = db;
        }

        public IEnumerable<District> Get(Expression<Func<District, bool>> filter = null)
        {
            if (filter == null)
            {
                var data = db.District.Select(a => a);
                return data;
            }
            else
            {
                return db.District.Where(filter);
            }
        }

        public District GetById(int id)
        {
            var data = db.District.Where(a => a.Id == id).FirstOrDefault();
            return data;
        }
    }
}
