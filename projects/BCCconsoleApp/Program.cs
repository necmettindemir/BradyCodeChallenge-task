using BCCclassLibrary.Library;
using BCCclassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace BCCconsoleApp
{
    class Program
    {

        public static SettingsForFilePath filePathSettings;
        public static FactorBundle valueFactorBundle;
        public static FactorBundle emissionFactorBundle;
        public static Dictionary<string, SettingsForGeneratorWithFactorType> dictOfGeneratorWithFactorSettings;

        static void Main(string[] args)
        {

            Utils utils = new Utils();
            XmlOperations xmlOpr = new XmlOperations();
            PrintOperations prntOpr = new PrintOperations();
            OutputOperations outOpr = new OutputOperations();


            #region step-1 ---------- load-settings

            try
            {
                filePathSettings = utils.GetFilePathSettingsFromAppSettings();
                
                Console.WriteLine("\nsettings.InputFilePath: {0}", filePathSettings.InputFilePath);
                Console.WriteLine("settings.OutputFilePath: {0}", filePathSettings.OutputFilePath);
                Console.WriteLine("settings.ReferenceDataFilePath: {0}", filePathSettings.ReferenceDataFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError: {0}", ex.Message);
                return;
            }

            #endregion step-1 ---------- load-settings







            #region step-2 ---------- load factor data

            string factorXMLdata;

            try
            {
                factorXMLdata = utils.ReadDataFromFile(filePathSettings.ReferenceDataFilePath);
                Console.WriteLine("\nfactorXMLdata: {0}", factorXMLdata);

                valueFactorBundle = xmlOpr.GetValueFactorFromXMLstr(factorXMLdata);
                emissionFactorBundle = xmlOpr.GetEmissionFactorFromXMLstr(factorXMLdata);


                Console.WriteLine("\nvalueFactor =  high:{0}, meidum:{1}, low:{2}", valueFactorBundle.High, valueFactorBundle.Medium, valueFactorBundle.Low);
                Console.WriteLine("emissionFactor =  high:{0}, meidum:{1}, low:{2}", emissionFactorBundle.High, emissionFactorBundle.Medium, emissionFactorBundle.Low);

            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError: {0}", ex.Message);
                return;
            }

            #endregion step-2 ---------- load factor data







            #region step-3 ---------- load values of map of generationType-Factor 

            dictOfGeneratorWithFactorSettings = utils.GetDictOfGeneratorWithFactorTypeSettings();
            prntOpr.PrintDictOfGeneratorwithFactor(dictOfGeneratorWithFactorSettings);

            #endregion step-3 ---------- load values of map of generationType-Factor 






            #region step-4 ---------- start input file watcher                     

            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = filePathSettings.InputFilePath;
            
            // Watch both files and subdirectories.  
            watcher.IncludeSubdirectories = true;

            // Watch for all changes specified in the NotifyFilters enumeration.  
            watcher.NotifyFilter = NotifyFilters.Attributes |
                                        NotifyFilters.CreationTime |
                                        NotifyFilters.DirectoryName |
                                        NotifyFilters.FileName |
                                        NotifyFilters.LastAccess |
                                        NotifyFilters.LastWrite |
                                        NotifyFilters.Security |
                                        NotifyFilters.Size;
            // Watch all files.  
            watcher.Filter = "*.*";

            // Add event handlers.  
            watcher.Created += new FileSystemEventHandler(FileEventHandler);
            watcher.Changed += new FileSystemEventHandler(FileEventHandler);    
            
            //Start monitoring.  
            watcher.EnableRaisingEvents = true;

            

            Console.WriteLine("\nWatcher started!");


            #endregion step-4 ---------- start input file watcher



            Console.ReadLine();

        }//main

        public static void FileEventHandler(object source, FileSystemEventArgs e)
        {


            // Specify what is done when a file is changed.  
            Console.WriteLine("File: {0}, \nPath:{1} \nEvent: {2}", e.Name, e.FullPath, e.ChangeType);


            Utils utils = new Utils();
            XmlOperations xmlOpr = new XmlOperations();
            PrintOperations prntOpr = new PrintOperations();
            OutputOperations outOpr = new OutputOperations();


            string inputXMLdata;
            List<Generator> generatorList;

            try
            {                
                inputXMLdata = utils.ReadDataFromFile(e.FullPath);
                Console.WriteLine("\ninputXMLdata: {0}", inputXMLdata);

                generatorList = xmlOpr.FillinputXMLintoList(inputXMLdata);

            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError: {0}", ex.Message);
                return;
            }


            prntOpr.PrintGeneratorList(generatorList);

            try
            {
                string outputXML = outOpr.GenerateOutputXml(generatorList, dictOfGeneratorWithFactorSettings, valueFactorBundle, emissionFactorBundle);
                utils.WriteDataToFile(filePathSettings.OutputFilePath, outputXML);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError: {0}", ex.Message);
                return;
            }


        }

    }
}
