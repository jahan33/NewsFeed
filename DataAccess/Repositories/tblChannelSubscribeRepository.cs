
    
using DataAccess.Models;
using DataAccess.DAL;
using DataAccess.IRepositories;

namespace DataAccess.Repositories
{           
public class tblChannelSubscribeRepository : BaseRepository<tblChannelSubscribe>, ItblChannelSubscribeRepository
{
 public tblChannelSubscribeRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    

    //Override any generic method for your own custom implemention, add new repository methods to the ItblChannelSubscribeRepository.cs file
}
}
