using System;
using System.Collections.Generic;
using System.Text;

namespace BCCclassLibrary.Models
{
    public class GeneratorForGas: Generator
    {

        private double _emissionsRating;

        public double EmissionsRating { get => _emissionsRating; set => _emissionsRating = value; }
    }
}
