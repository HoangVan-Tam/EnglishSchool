using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Data.Repositories
{
    public interface IPersonalInformationRepository : IRepository<PersonalInformation>
    {

    }
    public class PersonalInformationRepository : RepositoryBase<PersonalInformation>, IPersonalInformationRepository
    {
        public PersonalInformationRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
