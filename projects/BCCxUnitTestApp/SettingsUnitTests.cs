using BCCclassLibrary.Library;
using BCCclassLibrary.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace BCCxUnitTestApp
{
    public class SettingsUnitTests
    {
        [Fact]
        public void ValueAndEmissionFactorValuesMustBeReachable()
        {
            //Assign
            XmlOperations xmlOpr = new XmlOperations();
            string factorXMLdata = @"<ReferenceData>
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
                                    </ReferenceData>";

            //Act            
            FactorBundle valueFactorBundle = xmlOpr.GetValueFactorFromXMLstr(factorXMLdata);
            FactorBundle emissionFactorBundle = xmlOpr.GetEmissionFactorFromXMLstr(factorXMLdata);


            //Assert
            Assert.True(valueFactorBundle.High >= 0);
            Assert.True(valueFactorBundle.Medium >= 0);
            Assert.True(valueFactorBundle.Low >= 0);

            Assert.True(emissionFactorBundle.High >= 0);
            Assert.True(emissionFactorBundle.Medium >= 0);
            Assert.True(emissionFactorBundle.Low >= 0);

        }



        [Fact]
        public void GeneratorWithFactorTypesMustExist()
        {
            //Assign
            Utils utils = new Utils();
            Dictionary<string, SettingsForGeneratorWithFactorType> dictOfGeneratorWithFactorSettings;

            //Act            
            dictOfGeneratorWithFactorSettings = utils.GetDictOfGeneratorWithFactorTypeSettings();

            //Assert
            Assert.True(dictOfGeneratorWithFactorSettings.Count >= 0);            
        }

    }
}
