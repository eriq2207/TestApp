using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models
{
    class Towar
    {
        public Towar()
        {
            this.Opis = new Opisy();
        }
        public String Nazwa;
        public float Cena;
        public Opisy Opis;
    }
}
