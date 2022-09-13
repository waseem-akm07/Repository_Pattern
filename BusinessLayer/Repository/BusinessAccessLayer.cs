using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Model;
using DataTransferObject.HelperModel;

namespace BusinessLayer
{
    public class BusinessAccessLayer : IBusinessLayer
    {
        private MvcprctdbEntities _context = new MvcprctdbEntities();


        public IEnumerable<StudentModel> GetStudents()
        {
            try
            {
                var _data = _context.Students
                            .Include(s => s.School)
                            .Include(s => s.Class)
                            .Include(s => s.StudentDetails)
                            .Include(s => s.StudentSubjects.Select(sb=>sb.Subject))
                            .AsEnumerable()
                            .Select(s => new StudentModel
                            {
                                StudentId = s.StudentId,
                                StudentName = s.StudentName,
                                Class =new ClassModel
                                {
                                    ClassId =(int)s.ClassId,
                                    ClassName = s.Class.ClassName
                                },
                                School = new SchoolModel
                                {
                                    SchoolId =(int)s.SchoolId,
                                    SchoolName = s.School.SchoolName
                                },
                                Detail = new StudentDetailModel
                                {
                                    DetailId = (int)s.StudentDetails.Select(z => z.DetailId).FirstOrDefault(),
                                    FatherName = s.StudentDetails.Select(z => z.FatherName).FirstOrDefault(),
                                    Address = s.StudentDetails.Select(z => z.Address).FirstOrDefault(),
                                    PhoneNo = s.StudentDetails.Select(z => z.PhoneNo).FirstOrDefault(),
                                },
                                Subject = parseStudentSubjects(s.StudentSubjects.ToList())
                            }).ToList();

               
                return _data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public List<SubjectModel> parseStudentSubjects(List<StudentSubject> _studentSubjects)
        {
            List<SubjectModel> _subject = new List<SubjectModel>();
            
            foreach (var studentSubject in _studentSubjects)
            {
                _subject.Add(new SubjectModel(studentSubject.Subject??new Subject()));
            }
                                           
            return _subject;
        }

        public List<StudentModel> GetStudent(int id)
        {
            try
            {                             
                var _data = _context.Students
                            .Include(s => s.School)
                            .Include(s => s.Class)
                            .Include(s => s.StudentDetails)
                            .Include(s => s.StudentSubjects.Select(sb => sb.Subject))
                            .Where(s=>s.StudentId.Equals(id))
                            .AsEnumerable()
                            .Select(s => new StudentModel
                            {
                                StudentId = s.StudentId,
                                StudentName = s.StudentName,
                                Class = new ClassModel
                                {
                                    ClassId = (int)s.ClassId,
                                    ClassName = s.Class.ClassName
                                },
                                School = new SchoolModel
                                {
                                    SchoolId = (int)s.SchoolId,
                                    SchoolName = s.School.SchoolName
                                },
                                Detail = new StudentDetailModel
                                {
                                    DetailId = (int)s.StudentDetails.Select(z => z.DetailId).FirstOrDefault(),
                                    FatherName = s.StudentDetails.Select(z => z.FatherName).FirstOrDefault(),
                                    Address = s.StudentDetails.Select(z => z.Address).FirstOrDefault(),
                                    PhoneNo = s.StudentDetails.Select(z => z.PhoneNo).FirstOrDefault(),
                                },
                                Subject = parseStudentSubjects(s.StudentSubjects.ToList())
                            }).ToList();

                return _data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string CreateStudent(DataModel model)
        {
            try
            {
                Class classes = new Class();
                School school = new School();
                Student student = new Student();
                Subject subject = new Subject();
                StudentDetail studentDetail = new StudentDetail();
                StudentSubject studentSubject = new StudentSubject();

                school.SchoolName = model.SchoolName;
                _context.Schools.Add(school);
                _context.SaveChanges();
                int SchoolId = school.SchoolId;

                classes.ClassName = model.ClassName;
                _context.Classes.Add(classes);
                _context.SaveChanges();
                int ClassId = classes.ClassId;

                subject.SubjectName = model.SubjectName;
                _context.Subjects.Add(subject);
                _context.SaveChanges();
                int SubjectId = subject.SubjectId;

                student.StudentName = model.StudentName;
                student.ClassId = ClassId;
                student.SchoolId = SchoolId;
                _context.Students.Add(student);
                _context.SaveChanges();
                int StudentId = student.StudentId;

                studentDetail.FatherName = model.FatherName;
                studentDetail.Address = model.Address;
                studentDetail.PhoneNo = model.PhoneNo;
                studentDetail.StudentId = StudentId;
                _context.StudentDetails.Add(studentDetail);

                studentSubject.StudentId = StudentId;
                studentSubject.SubjectId = SubjectId;
                _context.StudentSubjects.Add(studentSubject);
                _context.SaveChanges();


                return ("Data Add Successfully");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string UpdateStudent(int id, DataModel model)
        {
            try
            {
                var student = _context.Students.Where(x => x.StudentId == id).FirstOrDefault();
                student.StudentName = model.StudentName;
                _context.Entry(student).State = System.Data.Entity.EntityState.Modified;

                var school = _context.Schools.Where(x => x.SchoolId == student.SchoolId).FirstOrDefault();
                school.SchoolName = model.SchoolName;
                _context.Entry(school).State = System.Data.Entity.EntityState.Modified;

                var classes = _context.Classes.Where(x => x.ClassId == student.ClassId).FirstOrDefault();
                classes.ClassName = model.ClassName;
                _context.Entry(classes).State = System.Data.Entity.EntityState.Modified;

                var studentDetail = _context.StudentDetails.Where(x => x.StudentId == student.StudentId).FirstOrDefault();
                studentDetail.FatherName = model.FatherName;
                studentDetail.Address = model.Address;
                studentDetail.PhoneNo = model.PhoneNo;
                _context.Entry(studentDetail).State = System.Data.Entity.EntityState.Modified;

                var studentSubject = _context.StudentSubjects.Where(x => x.StudentId == student.StudentId).Select(x => x.SubjectId).FirstOrDefault();

                var subject = _context.Subjects.Where(x => x.SubjectId == studentSubject).FirstOrDefault();
                subject.SubjectName = model.SubjectName;
                _context.Entry(subject).State = System.Data.Entity.EntityState.Modified;

                _context.SaveChanges();

                return ("Data Upadte Successfully");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string Delete(int id)
        {
            try
            {
                var data = _context.Students.Where(x => x.StudentId == id).FirstOrDefault();
                _context.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                _context.SaveChanges();
                return ("Delete Successfully");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
