                
using DataAccess.Models;
using DataAccess.DAL;
using DataAccess.IRepositories;

namespace DataAccess.Repositories
{           
public class tblUserLogRepository : BaseRepository<tblUserLog>, ItblUserLogRepository
{
 public tblUserLogRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    

    //Override any generic method for your own custom implemention, add new repository methods to the ItblUserLogRepository.cs file
}
}
