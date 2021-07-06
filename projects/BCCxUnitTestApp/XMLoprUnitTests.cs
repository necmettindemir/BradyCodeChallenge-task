using BCCclassLibrary.Library;
using BCCclassLibrary.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace BCCxUnitTestApp
{
    public class XMLoprUnitTests
    {


        [Fact]
        public void GeneratorWithFactorTypesMustExist()
        {
            //Assign
            GenerationData generationData;
            XmlOperations xmlOpr = new XmlOperations();
            string xmlStr =  @"<Generation>
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
                               </Generation>";

            //Act            
            generationData = xmlOpr.GetGenerationDataAsList(xmlStr);

            //Assert
            Assert.True(generationData.ListOfDayData.Count == 3);
        }


    }
}
