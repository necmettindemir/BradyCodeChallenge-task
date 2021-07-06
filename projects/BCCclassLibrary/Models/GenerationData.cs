using System;
using System.Collections.Generic;
using System.Text;

namespace BCCclassLibrary.Models
{
    public class GenerationData
    {
        private List<DayData> _listOfDayData;

        public List<DayData> ListOfDayData { get => _listOfDayData; set => _listOfDayData = value; }


        public GenerationData()
        {
            _listOfDayData = new List<DayData>();
        }
    }
}
