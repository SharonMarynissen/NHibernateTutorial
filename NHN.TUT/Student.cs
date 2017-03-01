using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateDemoApp
{
    public class Student
    {
        //public virtual Guid Id { get; set; }        //When using generator class="guid.comb" in mapping
        public virtual int Id { get; set; }
        public virtual string LastName { get; set; }
        public virtual string FirstName { get; set; }
        //StudentAcademicStanding is an enum: the values responding to the enum value used
        //will be added in the db (starts with 0 by default and is increased by 1 by default)
        public virtual StudentAcademicStanding AcademicStanding { get; set; }
        //Location is a component model: value object without having its own pk and is persistent in the same 
        //table as the owning object (Student)
        //Location is a class, not an entity
        //Component modeling gives flexibility to change your class layer, how your classes are
        //defined versus how your database is laid out
        public virtual Location Address { get; set; }
    }
}
