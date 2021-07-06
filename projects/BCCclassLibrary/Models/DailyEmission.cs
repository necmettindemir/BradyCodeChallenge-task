using BCCclassLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCCclassLibrary.Models
{
    public class DailyEmission
    {

        private ENUM_GENERATOR_TYPE _generatorType;
        private string _generatorName;
        private string _dateStr;
        private double _emissionValue;


        public DailyEmission() {}


        public DailyEmission(
                                ENUM_GENERATOR_TYPE generatorType,
                                string generatorName,
                                string dateStr,
                                double emissionValue) 
        {


            _generatorType = generatorType;
            _generatorName = generatorName;
            _dateStr       = dateStr;
            _emissionValue = emissionValue;
        }

        public ENUM_GENERATOR_TYPE GeneratorType { get => _generatorType; set => _generatorType = value; }
        public string GeneratorName { get => _generatorName; set => _generatorName = value; }
        public string DateStr { get => _dateStr; set => _dateStr = value; }
        public double EmissionValue { get => _emissionValue; set => _emissionValue = value; }
    }
}
