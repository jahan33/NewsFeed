                
using DataAccess.Models;
using DataAccess.DAL;
using DataAccess.IRepositories;

namespace DataAccess.Repositories
{           
public class tblRolePageRepository : BaseRepository<tblRolePage>, ItblRolePageRepository
{
 public tblRolePageRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    

    //Override any generic method for your own custom implemention, add new repository methods to the ItblRolePageRepository.cs file
}
}
