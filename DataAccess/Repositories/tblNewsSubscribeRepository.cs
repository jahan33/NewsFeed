
    
using DataAccess.Models;
using DataAccess.DAL;
using DataAccess.IRepositories;

namespace DataAccess.Repositories
{           
public class tblNewsSubscribeRepository : BaseRepository<tblNewsSubscribe>, ItblNewsSubscribeRepository
{
 public tblNewsSubscribeRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    

    //Override any generic method for your own custom implemention, add new repository methods to the ItblNewsSubscribeRepository.cs file
}
}
