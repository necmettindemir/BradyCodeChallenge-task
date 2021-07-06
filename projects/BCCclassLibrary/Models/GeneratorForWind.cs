using System;
using System.Collections.Generic;
using System.Text;

namespace BCCclassLibrary.Models
{
    public class GeneratorForWind: Generator
    {
        private string _location;

        public string Location { get => _location; set => _location = value; }
    }
}
