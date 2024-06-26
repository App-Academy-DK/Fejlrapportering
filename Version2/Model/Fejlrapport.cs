using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Version2.Model
{
    public class Fejlrapport
    {
        public string Titel { get; set; }
        public string Forventet { get; set; }
        public string Oplevet { get; set; }
        public string[] Trin { get; set; }
    }

}
