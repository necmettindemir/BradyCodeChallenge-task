using BCCclassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCCclassLibrary.Library
{
    /// <summary>
    /// the aim is to print some lists into console
    /// </summary>
    public class PrintOperations
    {

        public void PrintDictOfGeneratorwithFactor(Dictionary<string, SettingsForGeneratorWithFactorType> dictOfGeneratorwithFactor)
        {

            Console.WriteLine("\n---- content of dictionary -----");

            foreach (KeyValuePair<string, SettingsForGeneratorWithFactorType> kvp in dictOfGeneratorwithFactor)
            {
                SettingsForGeneratorWithFactorType gwf = (SettingsForGeneratorWithFactorType)(kvp.Value);

                Console.WriteLine("Key = {0}, Value = {1} - {2} - {3} ",
                                    kvp.Key,
                                    gwf.GeneratorType.ToString(),
                                    gwf.ValueFactor.ToString(),
                                    gwf.EmissionFactor.ToString()
                                    );
            }
        }


        public void PrintGeneratorList(List<Generator> generatorList)
        {


            Console.WriteLine("\nGenerator List");

            foreach (var item in generatorList)
            {
                Console.WriteLine("\nGenerator Type: {0}", item.GeneratorType);
                Console.WriteLine("Generator Name: {0}", item.Name);

                if (item.GetType() == typeof(GeneratorForWind))
                {
                    Console.WriteLine("Location : {0}", ((GeneratorForWind)item).Location);
                }
                else if (item.GetType() == typeof(GeneratorForGas))
                {
                    Console.WriteLine("EmissionsRating : {0}", ((GeneratorForGas)item).EmissionsRating.ToString());
                }
                else if (item.GetType() == typeof(GeneratorForCoal))
                {
                    Console.WriteLine("TotalHeatInput : {0}", ((GeneratorForCoal)item).TotalHeatInput.ToString());
                    Console.WriteLine("ActualNetGeneration : {0}", ((GeneratorForCoal)item).ActualNetGeneration.ToString());
                    Console.WriteLine("EmissionsRating : {0}", ((GeneratorForCoal)item).EmissionsRating.ToString());
                }


                foreach (var dayData in item.GenerationData.ListOfDayData)
                {
                    Console.WriteLine(" day data: {0} - {1} - {2}", dayData.DateStr.ToString(), dayData.Energy.ToString(), dayData.Price.ToString());
                }
            }

        }

    }
}
