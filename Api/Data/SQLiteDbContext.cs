using Microsoft.EntityFrameworkCore;
using SharedClass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Data
{
    public class SQLiteDbContext : DbContext
    {
        public SQLiteDbContext(DbContextOptions<SQLiteDbContext> options) : base(options)
        {

        }
        public DbSet<AttendanceRecord> AttendanceRecord { get; set; }
        public DbSet<CustomMessage> CustomMessage { get; set; }
        public DbSet<Section> Section { get; set; }
        public DbSet<SectionRecord> SectionRecord { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<SubjectRecord> SubjectRecord { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Payment> Payment { get; set; }
    }
}
