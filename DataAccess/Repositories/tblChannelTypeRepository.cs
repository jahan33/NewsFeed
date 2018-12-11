                
using DataAccess.Models;
using DataAccess.DAL;
using DataAccess.IRepositories;

namespace DataAccess.Repositories
{           
public class tblChannelTypeRepository : BaseRepository<tblChannelType>, ItblChannelTypeRepository
{
 public tblChannelTypeRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    

    //Override any generic method for your own custom implemention, add new repository methods to the ItblChannelTypeRepository.cs file
}
}
