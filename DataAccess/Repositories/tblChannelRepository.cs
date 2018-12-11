                
using DataAccess.Models;
using DataAccess.DAL;
using DataAccess.IRepositories;

namespace DataAccess.Repositories
{           
public class tblChannelRepository : BaseRepository<tblChannel>, ItblChannelRepository
{
 public tblChannelRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    

    //Override any generic method for your own custom implemention, add new repository methods to the ItblChannelRepository.cs file
}
}
