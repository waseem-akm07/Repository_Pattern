using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.HelperModel
{
    public class SubjectModel
    {
        public SubjectModel(Subject studentSubject)
        {
            SubjectId = studentSubject.SubjectId;
            SubjectName = studentSubject.SubjectName; 
        }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
               
    }
}
