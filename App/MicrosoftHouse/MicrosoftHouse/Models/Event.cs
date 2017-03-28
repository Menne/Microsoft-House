using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftHouse.Models
{
    class Event
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Place { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
    }
}
