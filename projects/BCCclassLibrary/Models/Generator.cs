using BCCclassLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCCclassLibrary.Models
{
    public class Generator
    {
        private string _name;
        private ENUM_GENERATOR_TYPE _generatorType;        
        private GenerationData _generationData;

        public Generator()
        {
            _generationData = new GenerationData();
        }

        public string Name { get => _name; set => _name = value; }
        public ENUM_GENERATOR_TYPE GeneratorType { get => _generatorType; set => _generatorType = value; }
        public GenerationData GenerationData { get => _generationData; set => _generationData = value; }
        
    }
}
