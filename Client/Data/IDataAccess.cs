using Client.Models;
using Client.ViewModels;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Data
{
    public interface IDataAccess
    {
        [Get("/GetStudentList/{userId}")]
        Task<List<Student>> GetStudentList(string userId);

        [Get("/RegisterEmail/{email}")]
        Task<User> RegisterEmail(string email);

        [Post("/SaveSection")]
        Task<Section> SaveSection(Section data);

        [Post("/SaveStudent")]
        Task<Student> SaveStudent(Student data);

        [Post("/SaveSubject")]
        Task<Subject> SaveSubject(Subject data);

        [Post("/UpdateStudent")]
        Task<Student> UpdateStudent(Student data);

        [Post("/SendCustomSMS")]
        Task SendCustomSMS(SendSMSViewModel data);

        [Get("/GetSection/{id}")]
        Task<Section> GetSection(string id);

        [Get("/GetSubject/{id}")]
        Task<Subject> GetSubject(string id);

        [Get("/GetStudent/{id}")]
        Task<Student> GetStudent(string id);

        [Get("/GetSubjectRecord/{subjectId}")]
        Task<List<SubjectRecord>> GetSubjectRecord(string subjectId);

        [Post("/GetAttendanceRecord")]
        Task<AttendanceRecord> GetAttendanceRecord(ParamViewModel data);

        [Get("/GetAttendanceRecord/{userId}")]
        Task<List<AttendanceRecord>> GetAttendanceRecord(string userId);

        [Post("/UpdateAttendanceRecord")]
        Task<AttendanceRecord> UpdateAttendanceRecord(AttendanceRecord data);

        [Get("/GetListSection/{userId}")]
        Task<List<Section>> GetListSection(string userId);

        [Get("/GetListSubject/{userId}/{sectionId}")]
        Task<List<Subject>> GetListSubject(string userId, string sectionId);

        [Get("/RemoveStudent/{studentId}")]
        Task RemoveStudent(string studentId);

        [Get("/RemoveSection/{id}")]
        Task RemoveSection(string id);

        [Post("/GetStudentFilter")]
        Task<List<Student>> GetStudentFilter(List<string> listid);

        [Post("/SaveSubjectRecordList")]
        Task<List<SubjectRecord>> SaveSubjectRecordList(List<SubjectRecord> data);

        [Get("/RemoveStudentFromSubect/{studentId}/{subjectId}")]
        Task RemoveStudentFromSubect(string studentId, string subjectId);

        [Post("/SaveAttendanceRecord")]
        Task<AttendanceRecord> SaveAttendanceRecord(AttendanceRecord data);

        [Post("/GetAttendanceRecords")]
        Task<List<AttendanceRecord>> GetAttendanceRecords(AttendanceViewModel param);

        [Get("/GetSMSBalance")]
        Task<BalanceViewModel> GetSMSBalance();

        [Get("/GetListUsers")]
        Task<List<User>> GetListUsers();

        [Get("/DeleteSection/{sectionId}")]
        Task<string> DeleteSection(string sectionId);

        [Get("/DeleteSubject/{subjectId}")]
        Task<string> DeleteSubject(string subjectId);

        [Post("/UpdateSection")]
        Task<Section> UpdateSection(Section data);

        [Post("/UpdateSubject")]
        Task<Subject> UpdateSubject(Subject data);
    }
}
