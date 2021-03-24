using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp15
{
    public class Student 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Address { get; set; }
        public string FullName { get => $"{LastName} {FirstName} {FatherName}"; }
    }
}
