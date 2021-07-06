using BCCclassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Xml.XPath;

namespace BCCclassLibrary.Library
{
    public class Utils
    {
        public SettingsForFilePath GetFilePathSettingsFromAppSettings() {

            SettingsForFilePath settings = new SettingsForFilePath();

            try
            {
                
                settings.InputFilePath = ConfigurationManager.AppSettings["INPUT_FILE_PATH"].Trim();
                settings.OutputFilePath = ConfigurationManager.AppSettings["OUTPUT_FILE_PATH"].Trim();
                settings.ReferenceDataFilePath = ConfigurationManager.AppSettings["REFERENCE_DATA_FILE_PATH"].Trim();
            }
            catch (Exception ex)
            {                
                throw new Exception(String.Format("opps! something went wrong when loading settings! {0}", ex.Message));
            }

            
            return settings;

        }

        public string ReadDataFromFile(string filePath)
        {

            string fileContent;

            try
            {
                fileContent = File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("opps! something went wrong when loading settings! {0}", ex.Message));
            }

            return fileContent;
        }



        public void WriteDataToFile(string filePath, string content)
        {
            

            try
            {
                File.WriteAllText(filePath, content);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("opps! something went wrong when writing to file! {0}", ex.Message));
            }

            
        }



        public Dictionary<string, SettingsForGeneratorWithFactorType> GetDictOfGeneratorWithFactorTypeSettings() {


            /* From the view of the extendablity, this dictionary could be filled from database as well*/

            Dictionary<string, SettingsForGeneratorWithFactorType> dictOfGeneratorwithFactorSettings = new Dictionary<string, SettingsForGeneratorWithFactorType>();
            SettingsForGeneratorWithFactorType gwf;

            //Offshore Wind
            gwf = new SettingsForGeneratorWithFactorType(
                            generatorType: Enums.ENUM_GENERATOR_TYPE.Offshore_Wind,
                            valueFactorType: Enums.ENUM_FACTOR_TYPE.Low,
                            emissionFactorType: Enums.ENUM_FACTOR_TYPE.NA);

            dictOfGeneratorwithFactorSettings.Add(gwf.GeneratorType.ToString(), gwf);


            //Offshore Wind
            gwf = new SettingsForGeneratorWithFactorType(
                            generatorType: Enums.ENUM_GENERATOR_TYPE.Onshore_Wind,
                            valueFactorType: Enums.ENUM_FACTOR_TYPE.High,
                            emissionFactorType: Enums.ENUM_FACTOR_TYPE.NA);

            dictOfGeneratorwithFactorSettings.Add(gwf.GeneratorType.ToString(), gwf);


            //Gas
            gwf = new SettingsForGeneratorWithFactorType(
                            generatorType: Enums.ENUM_GENERATOR_TYPE.Gas,
                            valueFactorType: Enums.ENUM_FACTOR_TYPE.Medium,
                            emissionFactorType: Enums.ENUM_FACTOR_TYPE.Medium);

            dictOfGeneratorwithFactorSettings.Add(gwf.GeneratorType.ToString(), gwf);


            //Coal
            gwf = new SettingsForGeneratorWithFactorType(
                            generatorType: Enums.ENUM_GENERATOR_TYPE.Coal,
                            valueFactorType: Enums.ENUM_FACTOR_TYPE.Medium,
                            emissionFactorType: Enums.ENUM_FACTOR_TYPE.High);

            dictOfGeneratorwithFactorSettings.Add(gwf.GeneratorType.ToString(), gwf);



            return dictOfGeneratorwithFactorSettings;
        }

       


    }
}
