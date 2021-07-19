using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaMAPAN
{
    class clsEventosProx
    {
        //  Monday Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday 

        public DateTime FirstDay()
        {
            string Dia = System.DateTime.Today.DayOfWeek.ToString();

            if (Dia == "Sunday")
            {
                return System.DateTime.Today.Date;
            }
            else if (Dia == "Monday")
            {
                return DateTime.Parse(System.DateTime.Today.Day - 1 + "/" + System.DateTime.Today.Month + "/" + System.DateTime.Today.Year);
            }
            else if (Dia == "Tuesday")
            {
                return DateTime.Parse(System.DateTime.Today.Day - 2 + "/" + System.DateTime.Today.Month + "/" + System.DateTime.Today.Year);
            }
            else if (Dia == "Wednesday")
            {
                return DateTime.Parse(System.DateTime.Today.Day - 3 + "/" + System.DateTime.Today.Month + "/" + System.DateTime.Today.Year);
            }
            else if (Dia == "Thursday")
            {
                return DateTime.Parse(System.DateTime.Today.Day - 4 + "/" + System.DateTime.Today.Month + "/" + System.DateTime.Today.Year);
            }
            else if (Dia == "Friday")
            {
                return DateTime.Parse(System.DateTime.Today.Day - 5 + "/" + System.DateTime.Today.Month + "/" + System.DateTime.Today.Year);
            }
            else
            {
                return DateTime.Parse(System.DateTime.Today.Day - 6 + "/" + System.DateTime.Today.Month + "/" + System.DateTime.Today.Year);
            }
        }

        public DateTime LastDay(DateTime First)
        {
            return DateTime.Parse(First.Day + 6 + "/" + First.Month + "/" + First.Year);
        }
    }
}
