using Core.DataAccess;
using Core.Entities.Dtos;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICarRepository : IEntityRepository<Car>
    {
        Task<List<SelectionItem>> GetCarsLookUp();
        //Task<List<SelectionItem>> GetLanguagesLookUpWithCode();

    }
}