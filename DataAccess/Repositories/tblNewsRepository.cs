                
using DataAccess.Models;
using DataAccess.DAL;
using DataAccess.IRepositories;

namespace DataAccess.Repositories
{           
public class tblNewsRepository : BaseRepository<tblNews>, ItblNewsRepository
{
 public tblNewsRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    

    //Override any generic method for your own custom implemention, add new repository methods to the ItblNewsRepository.cs file
}
}
