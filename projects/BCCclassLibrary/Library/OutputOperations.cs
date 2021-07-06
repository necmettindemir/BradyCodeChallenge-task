using BCCclassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCCclassLibrary.Library
{
    public class OutputOperations
    {
  
        public string GenerateOutputXml(List<Generator> generatorList, 
                                        Dictionary<string, SettingsForGeneratorWithFactorType> dictOfGeneratorWithFactorTypeSettings,
                                        FactorBundle valueFactorBundle,
                                        FactorBundle emissionFactorBundle)
        {

            StringBuilder resultXML = new StringBuilder();

            try
            {
                resultXML.Append("<GenerationOutput>");

                resultXML.Append(GenerateTotals(generatorList, dictOfGeneratorWithFactorTypeSettings,valueFactorBundle));
                resultXML.Append(GenerateMaxEmissionGenerators(generatorList, dictOfGeneratorWithFactorTypeSettings, emissionFactorBundle));
                resultXML.Append(GenerateActualHeatRates(generatorList));

                resultXML.Append("</GenerationOutput>");
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("opps! something went wrong when GenerateOutputXmlToFile! {0}", ex.Message));
            }

            return resultXML.ToString();

        }




        public string GenerateTotals(List<Generator> generatorList, 
                                     Dictionary<string, SettingsForGeneratorWithFactorType> dictOfGeneratorWithFactorTypeSettings,
                                     FactorBundle valueFactorBundle) {

            CalculationOperations calcOpr = new CalculationOperations();
            StringBuilder resultXML = new StringBuilder();

            resultXML.Append("<Totals>");


            foreach (var item in generatorList)
            {

                resultXML.Append("<Generator>");
                resultXML.Append("<Name>" + item.Name + "</Name>");

                double factor = 0.0;

                Enums.ENUM_FACTOR_TYPE factorType = dictOfGeneratorWithFactorTypeSettings[item.GeneratorType.ToString()].ValueFactor;

                switch (factorType)
                {
                    case Enums.ENUM_FACTOR_TYPE.NA:
                        break;
                    case Enums.ENUM_FACTOR_TYPE.Low:
                        factor = valueFactorBundle.Low;
                        break;
                    case Enums.ENUM_FACTOR_TYPE.Medium:
                        factor = valueFactorBundle.Medium;
                        break;
                    case Enums.ENUM_FACTOR_TYPE.High:
                        factor = valueFactorBundle.High;
                        break;
                    default:
                        break;
                }

          
                double total = 0.0;

                foreach (var dayData in item.GenerationData.ListOfDayData)
                {
                    total += calcOpr.GetDailyGenerationValue(dayData.Energy, dayData.Price, factor);
                }

                resultXML.Append("<Total>" + total.ToString() + "</Total>");
                resultXML.Append("</Generator>");
                
            }

            resultXML.Append("</Totals>");

            return resultXML.ToString();

        }



        public string GenerateMaxEmissionGenerators(List<Generator> generatorList, 
                                                    Dictionary<string, SettingsForGeneratorWithFactorType> dictOfGeneratorWithFactorTypeSettings,
                                                    FactorBundle emissionFactorBundle)
        {


            Dictionary<string, List<DailyEmission>> dictOfDailyEmission = GetDictOfDailyEmission(generatorList, dictOfGeneratorWithFactorTypeSettings, emissionFactorBundle);

            StringBuilder resultXML = new StringBuilder();

            resultXML.Append("<MaxEmissionGenerators>");

            foreach (var kvp in dictOfDailyEmission)
            {

                List<DailyEmission> listOfDailyEmission = (List<DailyEmission>)(kvp.Value);
                DailyEmission maxDailyEmission = FindMaxEmissionFromList(listOfDailyEmission);

                resultXML.Append("<Day>");

                resultXML.Append("<Name>"+ maxDailyEmission.GeneratorName + "</Name>");
                resultXML.Append("<Date>"+ maxDailyEmission.DateStr +"</Date>");
                resultXML.Append("<Emission>" + maxDailyEmission.EmissionValue.ToString()  + "</Emission>");

                resultXML.Append("</Day>");

            }

            resultXML.Append("</MaxEmissionGenerators>");

            return resultXML.ToString();
        }



        public DailyEmission FindMaxEmissionFromList(List<DailyEmission> listOfDailyEmission) {

            DailyEmission maxDailyEmission = null;


            if (listOfDailyEmission.Count > 0) {

                maxDailyEmission = listOfDailyEmission[0];

                foreach (var item in listOfDailyEmission)
                {
                    if (maxDailyEmission.EmissionValue < item.EmissionValue)
                        maxDailyEmission = item;
                }

            }

            return maxDailyEmission;
        }

        public Dictionary<string, List<DailyEmission>> GetDictOfDailyEmission(
                                                    List<Generator> generatorList,
                                                    Dictionary<string, SettingsForGeneratorWithFactorType> dictOfGeneratorWithFactorTypeSettings,
                                                    FactorBundle emissionFactorBundle) {


            Dictionary<string, List<DailyEmission>> dictOfDailyEmission = new Dictionary<string, List<DailyEmission>>();            
            CalculationOperations calcOpr = new CalculationOperations();
           
            foreach (var item in generatorList)
            {

                if (item.GeneratorType == Enums.ENUM_GENERATOR_TYPE.Gas || item.GeneratorType == Enums.ENUM_GENERATOR_TYPE.Coal) {

                    double factor = 0.0;

                    Enums.ENUM_FACTOR_TYPE factorType = dictOfGeneratorWithFactorTypeSettings[item.GeneratorType.ToString()].EmissionFactor;

                    switch (factorType)
                    {
                        case Enums.ENUM_FACTOR_TYPE.NA:
                            break;
                        case Enums.ENUM_FACTOR_TYPE.Low:
                            factor = emissionFactorBundle.Low;
                            break;
                        case Enums.ENUM_FACTOR_TYPE.Medium:
                            factor = emissionFactorBundle.Medium;
                            break;
                        case Enums.ENUM_FACTOR_TYPE.High:
                            factor = emissionFactorBundle.High;
                            break;
                        default:
                            break;
                    }


                    string dateStr = "";
                    double emissionValue = 0;
                    double emissionRating = item.GeneratorType == Enums.ENUM_GENERATOR_TYPE.Gas ? ((GeneratorForGas)(item)).EmissionsRating : ((GeneratorForCoal)(item)).EmissionsRating;

                    foreach (var dayData in item.GenerationData.ListOfDayData)
                    {
                        dateStr = dayData.DateStr;
                        emissionValue = calcOpr.GetDailyEmissions(dayData.Energy, emissionRating, factor);
                        DailyEmission dailyEmission = new DailyEmission(item.GeneratorType, item.Name, dateStr, emissionValue);
                        
                        if (dictOfDailyEmission.ContainsKey(dateStr)) {
                            dictOfDailyEmission[dateStr].Add(dailyEmission);
                        }
                        else {
                            List<DailyEmission> list = new List<DailyEmission>();
                            list.Add(dailyEmission);
                            dictOfDailyEmission.Add(dateStr, list);
                        }

                        
                    }
                }

            }
                    

            return dictOfDailyEmission;
        }



        public string GenerateActualHeatRates(List<Generator> generatorList)
        {

            CalculationOperations calcOpr = new CalculationOperations();
            StringBuilder resultXML = new StringBuilder();

            resultXML.Append("<ActualHeatRates>");

            //---------------------
            
            foreach (var item in generatorList)
            {

                if (item.GeneratorType == Enums.ENUM_GENERATOR_TYPE.Coal) {


                    resultXML.Append("<ActualHeatRate>");
                    resultXML.Append("<Name>" + item.Name + "</Name>");

                    double heatRate = calcOpr.GetActualHeatRate(  ((GeneratorForCoal)item).TotalHeatInput, ((GeneratorForCoal)item).ActualNetGeneration  );
                    resultXML.Append("<HeatRate>" + heatRate.ToString() + "</HeatRate>");

                    resultXML.Append("</ActualHeatRate>");
                }

            }
            
            //---------------------

            resultXML.Append("</ActualHeatRates>");


            return resultXML.ToString();

        }

    }
}
