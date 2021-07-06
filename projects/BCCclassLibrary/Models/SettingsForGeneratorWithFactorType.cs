using BCCclassLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCCclassLibrary.Models
{
    public class SettingsForGeneratorWithFactorType
    {
        private ENUM_GENERATOR_TYPE _generatorType;
        private ENUM_FACTOR_TYPE _valueFactorType;
        private ENUM_FACTOR_TYPE _emissionFactorType;

        public SettingsForGeneratorWithFactorType() {}

        public SettingsForGeneratorWithFactorType(ENUM_GENERATOR_TYPE generatorType, ENUM_FACTOR_TYPE valueFactorType, ENUM_FACTOR_TYPE emissionFactorType) {
            _generatorType = generatorType;
            _valueFactorType = valueFactorType;
            _emissionFactorType = emissionFactorType;
        }

        public ENUM_GENERATOR_TYPE GeneratorType { get => _generatorType; set => _generatorType = value; }
        public ENUM_FACTOR_TYPE ValueFactor { get => _valueFactorType; set => _valueFactorType = value; }
        public ENUM_FACTOR_TYPE EmissionFactor { get => _emissionFactorType; set => _emissionFactorType = value; }
    }
}
