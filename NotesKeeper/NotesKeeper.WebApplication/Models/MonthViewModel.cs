using NotesKeeper.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesKeeper.WebApplication.Models
{
    public class MonthViewModel
    {
        public IEnumerable<Day> Days { get; set; }
    }
}
