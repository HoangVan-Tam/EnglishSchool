using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Model.DTOs
{
    public class ParentLoginDTO
    {
        public string parentId { get; set; }
        public string password { get; set; }
    }
    public class ParentLoginResponseDTO
    {

    }
    public class ParentDTO
    {
        public string parentId { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public bool sex { get; set; }
        public DateTime birdDay { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public bool status { get; set; }
    }
}
