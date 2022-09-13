using BusinessLayer;
using DataAccessLayer.Model;
using DataTransferObject.HelperModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RepositoryPattern.Controllers
{
    public class StudentController : ApiController
    {
        private readonly IBusinessLayer _iBusinessLayer;

        readonly IBusinessLayer businessLayer = new BusinessAccessLayer();

        public StudentController()
        {
            _iBusinessLayer = businessLayer;  
        }


        // GET: api/Student
        [HttpGet]
        [Route("api/student/getstudents")]
        public IEnumerable<StudentModel> Get()
        {
            return _iBusinessLayer.GetStudents();
        }


        // GET: api/Student/5
        [HttpGet]
        [Route("api/student/getstudent/{id}")]
        public List<StudentModel> Get(int id)
        {
            return _iBusinessLayer.GetStudent(id);
        }

        // POST: api/Student
        [HttpPost]
        [Route("api/student/poststudent")]
        public string Post(DataModel model)
        {
            return _iBusinessLayer.CreateStudent(model);
        }

        // PUT: api/Student/5
        [HttpPut]
        [Route("api/student/putstudent/{id}")]
        public string Put(int id, DataModel model)
        {
            return _iBusinessLayer.UpdateStudent(id, model);
        }

        // DELETE: api/Student/5
        [HttpDelete]
        [Route("api/student/delete/{id}")]
        public string Delete(int id)
        {
            return _iBusinessLayer.Delete(id);
        }
    }
}
