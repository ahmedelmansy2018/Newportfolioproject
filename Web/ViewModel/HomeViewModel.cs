using BL.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModel
{
    public class HomeViewModel
    {
        public Owner Owner { get; set; }
        public List<Project> Projects { get; set; }
    }
}
