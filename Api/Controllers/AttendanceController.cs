using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Data;
using SharedClass.Models;
using SharedClass.ViewModels;
using SharedClass;
using RestSharp;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AttendanceController : ControllerBase
    {
        private static readonly string apiKey = "1vI6Ri0Ek3oAriAOlmKakibB9Lb";
        private static readonly string apiSecret = "LQpj7KEpoyaUgHjqw8xuZ43sgxAF3y14Kuh9nF0t";


        private readonly IDbContextFactory<SQLiteDbContext> dbFactory;
        public readonly IWebHostEnvironment env;
        private readonly IMemoryCache cache;

        public HttpClient http { get; }

        public AttendanceController(IDbContextFactory<SQLiteDbContext> _dbFactory, IWebHostEnvironment _env, IMemoryCache _cache, HttpClient _http)
        {
            dbFactory = _dbFactory;
            env = _env;
            this.cache = _cache;
            http = _http;
        }
        [HttpGet("GetStudentList/{userId}")]
        public IActionResult GetStudentList(string userId)
        {
            using var db = dbFactory.CreateDbContext();
            var list = db.Student.Where(x => x.UserId == userId).OrderBy(x => x.LastName).ToList();
            return Ok(list);
        }
        [HttpGet("RegisterEmail/{email}")]
        public IActionResult RegisterEmail(string email)
        {
            using var db = dbFactory.CreateDbContext();
            var a = db.User.Where(x => x.Email == email).FirstOrDefault();
            Random r = new Random();
            var x = r.Next(0, 100000);
            if (a == null)
            {
                a = new();
                a.Email = email;
                string s = x.ToString("D5");
                a.PIN = s;
                a.DateRegistered = DateTime.Now;
                a.SMS = 2;
                db.User.Add(a);
                db.SaveChanges();
            }
            else
            {
                string s = x.ToString("D5");
                a.PIN = s;
                a.DateRegistered = DateTime.Now;
                db.User.Update(a);
                db.SaveChanges();
            }
            var textMessage = $"Your One-Time PIN (OTP) for AttendancePH is {a.PIN}. For your account security, do not share this code with anyone.";
            string body = $"api_key={apiKey}&api_secret={apiSecret}&to=63{a.Email}&text={textMessage}";
            var reqMessage = new HttpRequestMessage();
            reqMessage.RequestUri = new Uri("https://api.movider.co/v1/sms");
            reqMessage.Method = HttpMethod.Post;
            reqMessage.Headers.Add("Accept", "application/json");
            reqMessage.Content = new StringContent(body, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
            http.SendAsync(reqMessage);

            return Ok(a);
        }
        [HttpPost("SaveSection")]
        public IActionResult SaveSection(Section data)
        {
            using var db = dbFactory.CreateDbContext();
            data.Name = data.Name.ToUpper();
            db.Section.Add(data);
            db.SaveChanges();
            return Ok(data);
        }
        [HttpPost("SaveStudent")]
        public IActionResult SaveStudent(Student data)
        {
            using var db = dbFactory.CreateDbContext();
            data.FistName = data.FistName.ToUpper();
            data.LastName = data.LastName.ToUpper();
            db.Student.Add(data);
            db.SaveChanges();
            return Ok(data);
        }
        [HttpPost("SaveSubject")]
        public IActionResult SaveSubject(Subject data)
        {
            using var db = dbFactory.CreateDbContext();
            data.SubjectName = data.SubjectName.ToUpper();
            db.Subject.Add(data);
            db.SaveChanges();
            return Ok(data);
        }
        [HttpPost("UpdateStudent")]
        public IActionResult UpdateStudent(Student data)
        {
            using var db = dbFactory.CreateDbContext();
            if (!string.IsNullOrEmpty(data.Id))
            {
                db.Student.Update(data);
                db.SaveChanges();
            }
            return Ok(data);
        }

        [HttpPost("SendCustomSMS")]
        public async Task<IActionResult> SendCustomSMS(SendSMSViewModel data)
        {
            using var db = dbFactory.CreateDbContext();
            if (data.listabsent.Count > 0)
            {
                var userId = data.listabsent.Select(x => x.UserId).FirstOrDefault();
                var user = db.User.Where(x => x.Email == userId).FirstOrDefault();
                foreach (var rec in data.listabsent)
                {
                    if (user != null && user.SMS > 0)
                    {
                        var std = db.Student.Where(x => x.Id == rec.StudentId).FirstOrDefault();
                        var textMessage = data.msg.Message.Replace("#student#", $"{std.FistName} {std.LastName}");
                        textMessage = $"{textMessage}\n\nSchedule: {data.selectedDate.ToString("MM-dd-yyyy")} {data.sub.Time}";
                        string body = $"api_key={apiKey}&api_secret={apiSecret}&to=63{std.ParentsPhoneNumber}&text={textMessage}";
                        var reqMessage = new HttpRequestMessage();
                        reqMessage.RequestUri = new Uri("https://api.movider.co/v1/sms");
                        reqMessage.Method = HttpMethod.Post;
                        reqMessage.Headers.Add("Accept", "application/json");
                        reqMessage.Content = new StringContent(body, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
                        var postResult = await http.SendAsync(reqMessage);
                        if (postResult.IsSuccessStatusCode)
                        {
                            user.SMS -= 1;
                        }
                    }
                }
                db.User.Update(user);
                db.SaveChanges();
            }
            return Ok();
        }

        [HttpGet("GetSection/{id}")]
        public IActionResult GetSection(string id)
        {
            using var db = dbFactory.CreateDbContext();
            var a = db.Section.Where(x => x.Id == id).FirstOrDefault();
            return Ok(a);
        }
        [HttpGet("GetSubject/{id}")]
        public IActionResult GetSubject(string id)
        {
            using var db = dbFactory.CreateDbContext();
            var a = db.Subject.Where(x => x.Id == id).FirstOrDefault();
            return Ok(a);
        }
        [HttpGet("GetStudent/{id}")]
        public IActionResult GetStudent(string id)
        {
            using var db = dbFactory.CreateDbContext();
            var a = db.Student.Where(x => x.Id == id).FirstOrDefault();
            return Ok(a);
        }
        [HttpGet("GetSubjectRecord/{subjectId}")]
        public IActionResult GetSubjectRecord(string subjectId)
        {
            using var db = dbFactory.CreateDbContext();
            var list = db.SubjectRecord.Where(x => x.SubjectId == subjectId).ToList();
            return Ok(list);
        }
        [HttpPost("GetAttendanceRecord")]
        public IActionResult GetAttendanceRecord(ParamViewModel data)
        {
            using var db = dbFactory.CreateDbContext();
            var a = db.AttendanceRecord.Where(x => x.DateAttended.Value.Date == data.Date && x.SubjectId == data.SubjectId && x.StudentId == data.StudentId).FirstOrDefault();
            if (a != null)
                return Ok(a);
            return Ok(new AttendanceRecord());
        }
        [HttpGet("GetAttendanceRecord/{userId}")]
        public IActionResult GetAttendanceRecord(string userId)
        {
            using var db = dbFactory.CreateDbContext();
            var list = db.AttendanceRecord.Where(x => x.UserId == userId).ToList();
            return Ok(list);
        }
        [HttpPost("UpdateAttendanceRecord")]
        public IActionResult UpdateAttendanceRecord(AttendanceRecord data)
        {
            using var db = dbFactory.CreateDbContext();
            var a = db.AttendanceRecord.Where(x => x.SubjectId == data.SubjectId && x.StudentId == data.StudentId && x.UserId == data.UserId && x.DateAttended.Value.Date == data.DateAttended.Value.Date).FirstOrDefault();
            if (a == null)
            {
                a = new();
                a.SubjectId = data.SubjectId;
                a.StudentId = data.StudentId;
                a.UserId = data.UserId;
                a.IsPresent = !a.IsPresent;
                db.AttendanceRecord.Add(a);
                db.SaveChanges();
            }
            else
            {
                a.IsPresent = !a.IsPresent;
                db.AttendanceRecord.Update(a);
                db.SaveChanges();
            }
            return Ok(a);
        }
        [HttpGet("GetListSection/{userId}")]
        public IActionResult GetListSection(string userId)
        {
            using var db = dbFactory.CreateDbContext();
            var list = db.Section.Where(x => x.UserId == userId).ToList();
            return Ok(list);
        }
        [HttpGet("GetListSubject/{userId}/{sectionId}")]
        public IActionResult GetListSubject(string userId, string sectionId)
        {
            using var db = dbFactory.CreateDbContext();
            var list = db.Subject.Where(x => x.UserId == userId && x.SectionId == sectionId).ToList();
            return Ok(list);
        }
        [HttpGet("RemoveStudent/{studentId}")]
        public IActionResult RemoveStudent(string studentId)
        {
            using var db = dbFactory.CreateDbContext();
            var std = db.Student.Where(x => x.Id == studentId).FirstOrDefault();
            if (std != null)
            {
                db.Student.Remove(std);
                db.SaveChanges();
            }
            return Ok();
        }
        [HttpGet("RemoveSection/{id}")]
        public IActionResult RemoveSection(string id)
        {
            using var db = dbFactory.CreateDbContext();
            var a = db.Section.Where(x => x.Id == id).FirstOrDefault();
            if (a != null)
            {
                db.Section.Remove(a);
                db.SaveChanges();
            }
            return Ok();
        }

        [HttpPost("GetStudentFilter")]
        public IActionResult GetStudentFilter(GetStudentViewModel data)
        {
            using var db = dbFactory.CreateDbContext();
            var list = db.Student.Where(x => x.UserId == data.userId && !data.idlist.Contains(x.Id)).ToList();
            return Ok(list);
        }

        [HttpPost("SaveSubjectRecordList")]
        public IActionResult SaveSubjectRecordList(List<SubjectRecord> data)
        {
            using var db = dbFactory.CreateDbContext();
            db.SubjectRecord.AddRange(data);
            db.SaveChanges();
            var subjectId = data.Select(x => x.SubjectId).FirstOrDefault();
            var sub = db.Subject.Where(x => x.Id == subjectId).FirstOrDefault();
            var list = db.SubjectRecord.Where(x => x.SubjectId == subjectId).ToList();
            /*
                        foreach (var i in data)
                        {
                            var attrec = db.AttendanceRecord.Where(x => x.StudentId == i.StudentId && x.SubjectId == i.SubjectId).FirstOrDefault();
                            if(attrec == null)
                            {
                                attrec = new();
                                attrec.StudentId = i.StudentId;
                                attrec.SubjectId = i.SubjectId;
                                attrec.UserId = sub.UserId;
                                db.AttendanceRecord.Add(attrec);
                                db.SaveChanges();
                            }
                        }*/

            return Ok(list);
        }
        [HttpGet("RemoveStudentFromSubect/{studentId}/{subjectId}")]
        public IActionResult RemoveStudentFromSubect(string studentId, string subjectId)
        {
            using var db = dbFactory.CreateDbContext();
            var a = db.SubjectRecord.Where(x => x.StudentId == studentId && x.SubjectId == subjectId).FirstOrDefault();
            if (a != null)
            {
                db.SubjectRecord.Remove(a);
                db.SaveChanges();
            }
            var attrec = db.AttendanceRecord.Where(x => x.StudentId == studentId && x.SubjectId == subjectId).ToList();
            if (attrec != null)
            {
                db.AttendanceRecord.RemoveRange(attrec);
                db.SaveChanges();
            }
            return Ok();
        }
        [HttpPost("SaveAttendanceRecord")]
        public IActionResult SaveAttendanceRecord(AttendanceRecord data)
        {
            using var db = dbFactory.CreateDbContext();
            var isExist = db.AttendanceRecord.Any(x => x.StudentId == data.StudentId && x.SubjectId == data.SubjectId && x.DateAttended.Value.Date == data.DateAttended.Value.Date);
            if (!isExist)
                db.AttendanceRecord.Add(data);
            db.SaveChanges();
            return Ok(data);
        }

        [HttpPost("GetAttendanceRecords")]
        public IActionResult GetAttendanceRecords(AttendanceViewModel param)
        {
            using var db = dbFactory.CreateDbContext();
            var list = db.AttendanceRecord.Where(x => x.UserId == param.userId && x.SubjectId == param.subjectId && x.DateAttended.Value.Date == param.selectedDate.Date).DistinctBy(x => x.StudentId).ToList();
            return Ok(list);
        }

        [HttpGet("GetSMSBalance")]
        public async Task<IActionResult> GetSMSBalance()
        {
            HttpClient http = new HttpClient();
            string data = $"api_key={apiKey}&api_secret={apiSecret}";
            var reqMessage = new HttpRequestMessage();
            reqMessage.RequestUri = new Uri("https://api.movider.co/v1/balance");
            reqMessage.Method = HttpMethod.Post;
            reqMessage.Headers.Add("Accept", "application/json");
            reqMessage.Content = new StringContent(data, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
            var postReq = await http.SendAsync(reqMessage);

            var strjson = await postReq.Content.ReadAsStringAsync();
            var a = System.Text.Json.JsonSerializer.Deserialize<BalanceViewModel>(strjson);
            var remaining = a.amount / 0.006;
            a.remaining_sms = remaining;
            return Ok(a);
        }

        [HttpGet("GetListUsers")]
        public IActionResult GetListUsers()
        {
            using var db = dbFactory.CreateDbContext();
            var list = db.User.OrderByDescending(x => x.DateRegistered).ToList();
            return Ok(list);
        }

        [HttpGet("DeleteSection/{sectionId}")]
        public IActionResult DeleteSection(string sectionId)
        {
            using var db = dbFactory.CreateDbContext();
            var sec = db.Section.Where(x => x.Id == sectionId).FirstOrDefault();
            if (sec != null)
            {
                var listsec = db.SectionRecord.Where(x => x.SectionId == sectionId).ToList();
                var listsubs = db.Subject.Where(x => x.SectionId == sectionId).ToList();
                db.Subject.RemoveRange(listsubs);
                db.SectionRecord.RemoveRange(listsec);
                db.Section.Remove(sec);
                db.SaveChanges();
                return Ok(sectionId);
            }
            return BadRequest();
        }

        [HttpGet("DeleteSubject/{subjectId}")]
        public IActionResult DeleteSubject(string subjectId)
        {
            using var db = dbFactory.CreateDbContext();
            var sub = db.Subject.Where(x => x.Id == subjectId).FirstOrDefault();
            if (sub != null)
            {
                db.Subject.Remove(sub);
                db.SaveChanges();
                return Ok(subjectId);
            }
            return BadRequest();
        }

        [HttpPost("UpdateSection")]
        public IActionResult UpdateSection(Section data)
        {
            using var db = dbFactory.CreateDbContext();
            data.Name = data.Name.ToUpper();
            db.Section.Update(data);
            db.SaveChanges();
            return Ok(data);
        }

        [HttpPost("UpdateSubject")]
        public IActionResult UpdateSubject(Subject data)
        {
            using var db = dbFactory.CreateDbContext();
            data.SubjectName = data.SubjectName.ToUpper();
            db.Subject.Update(data);
            db.SaveChanges();
            return Ok(data);
        }

        [HttpGet("DownloadDatabase")]
        public IActionResult DownloadDatabase()
        {
            var origPath = Path.Combine(env.ContentRootPath, "Data", "1");
            var destPath = Path.Combine(env.ContentRootPath, "Data", $"{DateTime.Now.ToString("MMddyyyy_HHmmss")}");
            System.IO.File.Copy(origPath, destPath);
            var sr = System.IO.File.OpenRead(destPath);
            return File(sr, "application/octet-stream", "appdatabase.db");
        }

        [HttpGet("GetUser/{number}")]
        public IActionResult GetUser(string number)
        {
            using var db = dbFactory.CreateDbContext();
            var a = db.User.Where(x => x.Email == number).FirstOrDefault();
            return Ok(a);
        }

        [HttpPost("UpdateUser")]
        public IActionResult UpdateUser(User data)
        {
            using var db = dbFactory.CreateDbContext();
            db.User.Update(data);
            db.SaveChanges();
            return Ok(data);
        }

        [HttpPost("GcashPayment")]
        public IActionResult GcashPayment(GcashApiViewModel data)
        {
            using var db = dbFactory.CreateDbContext();
            var payment = db.Payment.Where(x => x.Reference == data.request_id).FirstOrDefault();
            if (payment != null)
            {
                payment.Reference = "PAID";
                db.Payment.Update(payment);
                db.SaveChanges();
            }
            return Ok(data);
        }

        [HttpGet("GetPaid/{amount}/{userId}")]
        public async Task<IActionResult> GetPaid(double amount, string userId)
        {
            using var db = dbFactory.CreateDbContext();

            var client = new RestClient("https://g.payx.ph/payment_request");
            var request = new RestRequest("", RestSharp.Method.Post);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("x-public-key", "pk_7a2075eefec680c4e4d3f5cdd140638a");
            request.AddParameter("amount", amount);
            request.AddParameter("customermobile", userId);
            request.AddParameter("description", "Payment for AttendancePH");
            request.AddParameter("webhooksuccessurl", "https://happy-wave-070586300.1.azurestaticapps.net/");
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = response.Content.ConvertJsonTo<PaymentResponseHeaderViewModel>();

                var payment = new Payment();
                payment.UserId = userId;
                payment.Reference = json.data.request_id;
                payment.Amount = amount;
                db.Payment.Add(payment);
                db.SaveChanges();
            }
            return Ok(response.Content);
        }


    }
}
