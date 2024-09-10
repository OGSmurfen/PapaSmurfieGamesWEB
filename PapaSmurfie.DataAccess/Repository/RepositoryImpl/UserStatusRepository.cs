using PapaSmurfie.DataAccess.Repository.IRepository;
using PapaSmurfie.Database;
using PapaSmurfie.Models.Models;
using PapaSmurfie.Repository.RepositoryImpl;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSmurfie.DataAccess.Repository.RepositoryImpl
{
    public class UserStatusRepository : RepositoryGeneric<UserStatusModel>, IUserStatusRepository
    {
        public UserStatusRepository(ApplicationDbContext applicationDbContext
                                    ) : base(applicationDbContext)
        {
        }

        
    }
}
