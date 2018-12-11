                
using DataAccess.Models;
using DataAccess.DAL;
using DataAccess.IRepositories;

namespace DataAccess.Repositories
{           
public class tblUserRepository : BaseRepository<tblUser>, ItblUserRepository
{
 public tblUserRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    

    //Override any generic method for your own custom implemention, add new repository methods to the ItblUserRepository.cs file
}
}
