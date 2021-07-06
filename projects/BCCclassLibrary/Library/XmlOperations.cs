using BCCclassLibrary.Enums;
using BCCclassLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace BCCclassLibrary.Library
{
    public class XmlOperations
    {

        public XPathNavigator GetXMLnavigator(string XMLstr)
        {
            XmlReaderSettings readerSettings = new XmlReaderSettings();
            readerSettings.ConformanceLevel = ConformanceLevel.Fragment;
            XPathDocument doc = new XPathDocument(XmlReader.Create(new StringReader(XMLstr), readerSettings));
            XPathNavigator nav = doc.CreateNavigator();

            return nav;
        }



        public FactorBundle GetValueFactorFromXMLstr(string XMLstr)
        {

            /*
           <ReferenceData>
            <Factors>
              <ValueFactor>
                <High>0.946</High>
                <Medium>0.696</Medium>
                <Low>0.265</Low>
              </ValueFactor>    
              <EmissionsFactor >
                <High>0.812</High>
                <Medium>0.562</Medium>
                <Low>0.312</Low>
              </EmissionsFactor>
            </Factors>
          </ReferenceData>
           */

            
            XPathNavigator nav;
            double high = 0.0;
            double medium = 0.0;
            double low = 0.0;

            try
            {
                nav = GetXMLnavigator(XMLstr);
                high = double.Parse(nav.SelectSingleNode("/ReferenceData/Factors/ValueFactor/High").ToString(), System.Globalization.CultureInfo.InvariantCulture);
                medium = double.Parse(nav.SelectSingleNode("/ReferenceData/Factors/ValueFactor/Medium").ToString(), System.Globalization.CultureInfo.InvariantCulture);
                low = double.Parse(nav.SelectSingleNode("/ReferenceData/Factors/ValueFactor/Low").ToString(), System.Globalization.CultureInfo.InvariantCulture);

            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("opps! something went wrong when loading ValueFactor! {0}", ex.Message));
            }


            return new FactorBundle(high, medium, low);
        }


        public FactorBundle GetEmissionFactorFromXMLstr(string XMLstr)
        {
            /*
            <ReferenceData>
            <Factors>
              <ValueFactor>
                <High>0.946</High>
                <Medium>0.696</Medium>
                <Low>0.265</Low>
              </ValueFactor>    
              <EmissionsFactor >
                <High>0.812</High>
                <Medium>0.562</Medium>
                <Low>0.312</Low>
              </EmissionsFactor>
            </Factors>
            </ReferenceData>
            */
            

            XPathNavigator nav;
            double high = 0.0;
            double medium = 0.0;
            double low = 0.0;

            try
            {
                nav = GetXMLnavigator(XMLstr);
                high = double.Parse(nav.SelectSingleNode("/ReferenceData/Factors/EmissionsFactor/High").ToString(), System.Globalization.CultureInfo.InvariantCulture);
                medium = double.Parse(nav.SelectSingleNode("/ReferenceData/Factors/EmissionsFactor/Medium").ToString(), System.Globalization.CultureInfo.InvariantCulture);
                low = double.Parse(nav.SelectSingleNode("/ReferenceData/Factors/EmissionsFactor/Low").ToString(), System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("opps! something went wrong when loading EmissionsFactor! {0}", ex.Message));
            }


            return new FactorBundle(high, medium, low);
        }


        public List<Generator> FillinputXMLintoList(string XMLstr) {


            List<Generator> generatorList = new List<Generator>();


            try
            {
                //wind
                FillWindDataintoList(XMLstr, generatorList);


                //gas
                FillGasDataintoList(XMLstr, generatorList);


                //coal
                FillCoalDataintoList(XMLstr, generatorList);

            }
            catch (Exception ex)
            {

                throw new Exception(String.Format("opps! something went wrong when FillinputXMLintoList! {0}", ex.Message));
            }

            return generatorList;
        }


        public void FillWindDataintoList(string XMLstr, List<Generator> generatorList)
        {
            XPathNavigator nav;
            nav = GetXMLnavigator(XMLstr);

            string windXML = nav.SelectSingleNode("/GenerationReport/Wind").OuterXml;

            XmlDocument windXMLDoc = new XmlDocument();
            windXMLDoc.LoadXml(windXML);

            #region xml example for windXML
            /*
               <Wind>
                    <WindGenerator>
                      <Name>Wind[Offshore]</Name>
                      <Generation>
                        <Day>
                          <Date>2017-01-01T00:00:00+00:00</Date>
                          <Energy>100.368</Energy>
                          <Price>20.148</Price>
                        </Day>
                        <Day>
                          <Date>2017-01-02T00:00:00+00:00</Date>
                          <Energy>90.843</Energy>
                          <Price>25.516</Price>
                        </Day>
                        <Day>
                          <Date>2017-01-03T00:00:00+00:00</Date>
                          <Energy>87.843</Energy>
                          <Price>22.015</Price>
                        </Day>
                      </Generation>
                      <Location>Offshore</Location>
                    </WindGenerator>
                    <WindGenerator>
                      <Name>Wind[Onshore]</Name>
                      <Generation>
                        <Day>
                          <Date>2017-01-01T00:00:00+00:00</Date>
                          <Energy>56.578</Energy>
                          <Price>29.542</Price>
                        </Day>
                        <Day>
                          <Date>2017-01-02T00:00:00+00:00</Date>
                          <Energy>48.540</Energy>
                          <Price>22.954</Price>
                        </Day>
                        <Day>
                          <Date>2017-01-03T00:00:00+00:00</Date>
                          <Energy>98.167</Energy>
                          <Price>24.059</Price>
                        </Day>
                      </Generation>
                      <Location>Onshore</Location>
                    </WindGenerator>
                </Wind>
             */

            #endregion xml example for windXML

            GeneratorForWind gnrtr;
            XmlNodeList xnList = windXMLDoc.SelectNodes("/Wind/WindGenerator");

            foreach (XmlNode xn in xnList)
            {
                gnrtr = new GeneratorForWind();

                gnrtr.Name = xn["Name"].InnerText;
                gnrtr.Location = xn["Location"].InnerText;
                gnrtr.GeneratorType = gnrtr.Location.ToLower() == "Offshore".ToLower() ? ENUM_GENERATOR_TYPE.Offshore_Wind : ENUM_GENERATOR_TYPE.Onshore_Wind;


                string generationXMLstr = xn["Generation"].OuterXml;
                gnrtr.GenerationData = GetGenerationDataAsList(generationXMLstr);

                generatorList.Add(gnrtr);

            }

        }




        public void FillGasDataintoList(string XMLstr, List<Generator> generatorList)
        {
            XPathNavigator nav;
            nav = GetXMLnavigator(XMLstr);

            string gasXML = nav.SelectSingleNode("/GenerationReport/Gas").OuterXml;

            XmlDocument windXMLDoc = new XmlDocument();
            windXMLDoc.LoadXml(gasXML);

            #region xml example for gasXML
            /*
                  <Gas>
                    <GasGenerator>
                      <Name>Gas[1]</Name>
                      <Generation>
                        <Day>
                          <Date>2017-01-01T00:00:00+00:00</Date>
                          <Energy>259.235</Energy>
                          <Price>15.837</Price>
                        </Day>
                        <Day>
                          <Date>2017-01-02T00:00:00+00:00</Date>
                          <Energy>235.975</Energy>
                          <Price>16.556</Price>
                        </Day>
                        <Day>
                          <Date>2017-01-03T00:00:00+00:00</Date>
                          <Energy>240.325</Energy>
                          <Price>17.551</Price>
                        </Day>
                      </Generation>
                      <EmissionsRating>0.038</EmissionsRating>
                    </GasGenerator>
                  </Gas>
             */

            #endregion xml example for windXML

            GeneratorForGas gnrtr;
            XmlNodeList xnList = windXMLDoc.SelectNodes("/Gas/GasGenerator");

            foreach (XmlNode xn in xnList)
            {
                gnrtr = new GeneratorForGas();

                gnrtr.Name = xn["Name"].InnerText;
                gnrtr.EmissionsRating = double.Parse(xn["EmissionsRating"].InnerText, System.Globalization.CultureInfo.InvariantCulture);
                gnrtr.GeneratorType = ENUM_GENERATOR_TYPE.Gas;


                string generationXMLstr = xn["Generation"].OuterXml;
                gnrtr.GenerationData = GetGenerationDataAsList(generationXMLstr);

                generatorList.Add(gnrtr);

            }

        }



        public void FillCoalDataintoList(string XMLstr, List<Generator> generatorList)
        {
            XPathNavigator nav;
            nav = GetXMLnavigator(XMLstr);

            string gasXML = nav.SelectSingleNode("/GenerationReport/Coal").OuterXml;

            XmlDocument windXMLDoc = new XmlDocument();
            windXMLDoc.LoadXml(gasXML);

            #region xml example for coalXML
            /*
                  <Coal>
                    <CoalGenerator>
                      <Name>Coal[1]</Name>
                      <Generation>
                        <Day>
                          <Date>2017-01-01T00:00:00+00:00</Date>
                          <Energy>350.487</Energy>
                          <Price>10.146</Price>
                        </Day>
                        <Day>
                          <Date>2017-01-02T00:00:00+00:00</Date>
                          <Energy>348.611</Energy>
                          <Price>11.815</Price>
                        </Day>
                        <Day>
                          <Date>2017-01-03T00:00:00+00:00</Date>
                          <Energy>0</Energy>
                          <Price>11.815</Price>
                        </Day>
                      </Generation>
                      <TotalHeatInput>11.815</TotalHeatInput>
                      <ActualNetGeneration>11.815</ActualNetGeneration>
                      <EmissionsRating>0.482</EmissionsRating>
                    </CoalGenerator>
                  </Coal>
             */

            #endregion xml example for windXML

            GeneratorForCoal gnrtr;
            XmlNodeList xnList = windXMLDoc.SelectNodes("/Coal/CoalGenerator");

            foreach (XmlNode xn in xnList)
            {
                gnrtr = new GeneratorForCoal();

                gnrtr.Name = xn["Name"].InnerText;
                gnrtr.TotalHeatInput = double.Parse(xn["TotalHeatInput"].InnerText, System.Globalization.CultureInfo.InvariantCulture);
                gnrtr.ActualNetGeneration = double.Parse(xn["ActualNetGeneration"].InnerText, System.Globalization.CultureInfo.InvariantCulture);
                gnrtr.EmissionsRating = double.Parse(xn["EmissionsRating"].InnerText, System.Globalization.CultureInfo.InvariantCulture);

                gnrtr.GeneratorType = ENUM_GENERATOR_TYPE.Coal;


                string generationXMLstr = xn["Generation"].OuterXml;
                gnrtr.GenerationData = GetGenerationDataAsList(generationXMLstr);

                generatorList.Add(gnrtr);

            }

        }



        public GenerationData GetGenerationDataAsList(string generationXMLstr) {

            GenerationData generationData = new GenerationData();

            XmlDocument generationXMLDoc = new XmlDocument();
            generationXMLDoc.LoadXml(generationXMLstr);


            #region example-for-generation-data

            /*
               <Generation>
                 <Day>
                   <Date>2017-01-01T00:00:00+00:00</Date>
                   <Energy>56.578</Energy>
                   <Price>29.542</Price>
                 </Day>
                 <Day>
                   <Date>2017-01-02T00:00:00+00:00</Date>
                   <Energy>48.540</Energy>
                   <Price>22.954</Price>
                 </Day>
                 <Day>
                   <Date>2017-01-03T00:00:00+00:00</Date>
                   <Energy>98.167</Energy>
                   <Price>24.059</Price>
                 </Day>
               </Generation>
             */

            #endregion example-for-generation-data
            
            DayData dayData;
            XmlNodeList xnList = generationXMLDoc.SelectNodes("/Generation/Day");

            foreach (XmlNode xn in xnList)
            {

                dayData = new DayData();

                dayData.DateStr = xn["Date"].InnerText;
                dayData.Energy = double.Parse( xn["Energy"].InnerText, System.Globalization.CultureInfo.InvariantCulture );
                dayData.Price = double.Parse(xn["Price"].InnerText, System.Globalization.CultureInfo.InvariantCulture);

                generationData.ListOfDayData.Add(dayData);

            }

            return generationData;
        }

      
    }
}
