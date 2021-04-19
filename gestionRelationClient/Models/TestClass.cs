using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace gestionRelationClient.Models
{
    class TestClass
    {

        [Key]
        public int Id { get; set; }
        //public int TestClassId { get; set; }

        public String Login { get; set; }

        public TestClass()
        {
            this.Login = "a";
        }

        public TestClass(int i)
        {
            this.Id = i;
            this.Login = "bla";
        }
    }
}
