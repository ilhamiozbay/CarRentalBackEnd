using Core.DataAccess.EntityFramework;
using Core.Entities.Dtos;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class CarRepository : EfEntityRepositoryBase<Car, ProjectDbContext>, ICarRepository
    {
        public CarRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<List<SelectionItem>> GetCarsLookUp()
        {
            var lookUp = await (from entity in Context.Cars
                                select new SelectionItem()
                                {
                                    Id = entity.Id,
                                    Label = entity.Description
                                }).ToListAsync();
            return lookUp;
        }

        //public async Task<List<SelectionItem>> GetLanguagesLookUpWithCode()
        //{
        //    var lookUp = await (from entity in Context.Languages
        //                        select new SelectionItem()
        //                        {
        //                            Id = entity.Code.ToString(),
        //                            Label = entity.Name
        //                        }).ToListAsync();
        //    return lookUp;
        //}
    }
}
