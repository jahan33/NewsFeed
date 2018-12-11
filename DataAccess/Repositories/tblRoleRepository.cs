                
using DataAccess.Models;
using DataAccess.DAL;
using DataAccess.IRepositories;

namespace DataAccess.Repositories
{           
public class tblRoleRepository : BaseRepository<tblRole>, ItblRoleRepository
{
 public tblRoleRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    

    //Override any generic method for your own custom implemention, add new repository methods to the ItblRoleRepository.cs file
}
}
