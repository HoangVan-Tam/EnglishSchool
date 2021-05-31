using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Model.Models;

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
