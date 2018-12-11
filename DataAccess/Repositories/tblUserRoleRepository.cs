                
using DataAccess.Models;
using DataAccess.DAL;
using DataAccess.IRepositories;

namespace DataAccess.Repositories
{           
public class tblUserRoleRepository : BaseRepository<tblUserRole>, ItblUserRoleRepository
{
 public tblUserRoleRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    

    //Override any generic method for your own custom implemention, add new repository methods to the ItblUserRoleRepository.cs file
}
}
