using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.HelperModel
{
    public class StudentModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public SchoolModel School { get; set; }
        public ClassModel Class { get; set; }
        public StudentDetailModel Detail { get; set; }
        public List<SubjectModel> Subject { get; set; }

    }
}
