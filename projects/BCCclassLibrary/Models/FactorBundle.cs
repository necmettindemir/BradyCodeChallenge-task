using System;
using System.Collections.Generic;
using System.Text;

namespace BCCclassLibrary.Models
{
    public class FactorBundle
    {

        private double _high;
        private double _medium;
        private double _low;


        public FactorBundle(){}

        public FactorBundle(double high, double medium, double low) {
            _high = high;
            _medium = medium;
            _low = low;
        }

        public double High { get => _high; set => _high = value; }
        public double Medium { get => _medium; set => _medium = value; }
        public double Low { get => _low; set => _low = value; }
        

    }
}
