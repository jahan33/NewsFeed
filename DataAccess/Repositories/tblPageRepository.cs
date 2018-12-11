                
using DataAccess.Models;
using DataAccess.DAL;
using DataAccess.IRepositories;

namespace DataAccess.Repositories
{           
public class tblPageRepository : BaseRepository<tblPage>, ItblPageRepository
{
 public tblPageRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    

    //Override any generic method for your own custom implemention, add new repository methods to the ItblPageRepository.cs file
}
}
