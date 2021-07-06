using System;
using System.Collections.Generic;
using System.Text;

namespace BCCclassLibrary.Library
{
    public class CalculationOperations
    {

        public double GetDailyGenerationValue(double energy, double price, double valueFactor)
        {

            //Daily Generation Value = Energy x Price x ValueFactor
            return energy * price * valueFactor;
        }

        public double GetDailyEmissions(double energy, double emissionRating, double emissionFactor)
        {
            //Energy x EmissionRating x EmissionFactor
            return energy * emissionRating * emissionFactor;
        }

        public double GetActualHeatRate(double totalHeatInput, double actualNetGeneration)
        {
            //Actual Heat Rate = TotalHeatInput / ActualNetGeneration
            return totalHeatInput / actualNetGeneration;
        }


    }
}
