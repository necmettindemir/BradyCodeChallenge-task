using System;
using System.Collections.Generic;
using System.Text;

namespace BCCclassLibrary.Models
{
    public class SettingsForFilePath
    {
        private string _inputFilePath;
        private string _outputFilePath;
        private string _referenceDataFilePath;

        public string InputFilePath { get => _inputFilePath; set => _inputFilePath = value; }
        public string OutputFilePath { get => _outputFilePath; set => _outputFilePath = value; }
        public string ReferenceDataFilePath { get => _referenceDataFilePath; set => _referenceDataFilePath = value; }
    }
}
