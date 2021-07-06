using System;
using System.Collections.Generic;
using System.Text;

namespace BCCclassLibrary.Models
{
    public class GeneratorForCoal : Generator
    {
        private double _totalHeatInput;
        private double _actualNetGeneration;
        private double _emissionsRating;

        public double TotalHeatInput { get => _totalHeatInput; set => _totalHeatInput = value; }
        public double ActualNetGeneration { get => _actualNetGeneration; set => _actualNetGeneration = value; }
        public double EmissionsRating { get => _emissionsRating; set => _emissionsRating = value; }
    }
}
