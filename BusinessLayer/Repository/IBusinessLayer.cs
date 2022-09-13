using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject.HelperModel;

namespace BusinessLayer
{
    public interface IBusinessLayer
    {
        IEnumerable<StudentModel> GetStudents();
        List<StudentModel> GetStudent(int id);
        string CreateStudent(DataModel model);
        string UpdateStudent(int id, DataModel model);
        string Delete(int id);
    }
}
