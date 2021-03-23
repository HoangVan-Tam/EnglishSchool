using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Model.ResponseService
{
    public class ResponseService<T>
    {
        public T result { get; set; }
        public string message { get; set; }
        public bool success { get; set; } = true;
    }
}
