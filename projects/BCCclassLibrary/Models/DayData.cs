using System;
using System.Collections.Generic;
using System.Text;

namespace BCCclassLibrary.Models
{
    public class DayData
    {

        private string _dateStr;
        private double _energy;
        private double _price;

        public DayData() {}

        public DayData(string dateStr, double energy, double price) {
            _dateStr = dateStr;
            _energy = energy;
            _price = price;
        }

        public string DateStr { get => _dateStr; set => _dateStr = value; }
        public double Energy { get => _energy; set => _energy = value; }
        public double Price { get => _price; set => _price = value; }

    }
}
