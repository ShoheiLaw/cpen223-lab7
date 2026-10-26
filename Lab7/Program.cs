// Lab 7
// Student name: Shohei Law
// Student number: 12133013

using System;
using System.Collections.Generic;

Console.WriteLine("CPEN223 Lab 7");

//Testing: Write some test cases to test well all methods you are to implement
//         This is to demonstrates what test cases you have considered
//TODO 
//end Testing code

//Do not change the program skeleton: keep the class name, method names,
//parameters, and return types exactly as given.
//You may add private helper methods.

public static class EmgAnalyzer
{
    public static List<double> Map(
        IReadOnlyList<double> values,
        Func<double, double> transform)
        ///in Map, the method takes in a readonly list of values and an operation
        /// The operation is meant to be applied to every element in that list.
        /// For implementation, we can initialize a new list and add the computed values to the new list
        /// Then we can return the list of newly computed values.
    {
        //first check for null of values or transform
        if (values == null||transform == null){
            throw new ArgumentException("Both the input lists and transformations must be valid inputs.");
        }

        List<double> mapped = new(); //initialize the new list
        if(values.Count == 0){ //special case for when the input list is empty, return an empty mapped list
            return mapped;
        }

        foreach (double n in values){ //loop to compute new values and add them to the new list
            mapped.Add(transform(n));
        }

        return mapped; 
    }

    public static List<double> Filter(
        IReadOnlyList<double> values,
        Func<double, bool> keep)
        ///This method takes in a read only list of values and a function that contains a filter constraint
        /// Calling this method should run through the entire list of values and only keep values that meet the constraints
        /// To implement, similar to map, we can create a new list to store all the values that fit,
        /// and write a foreach loop to cycle through all values. 
        /// then return the list of filtered values
    {
        //first check for valid inputs
        if(values == null || keep == null){
            throw new ArgumentException("Values and Keep cannot be null elements.");
        }

        //initialize list
        List<double> filtered = new();

        foreach (double v in values){ //loop to check each element,
            if(keep(v)){ //if the value is within the range of keep, then add it to filtered list
                filtered.Add(v);
            }
        }
        return filtered;
    }

    public static double Reduce(
        IReadOnlyList<double> values,
        double initial,
        Func<double, double, double> combine)
        /// The Reduce method takes in a list of values, an initial condition, and operation to combine the values
        /// Again, writing a foreach loop to cycle through all elements and combine them into a single value
        /// Then return the final computed value
    {
        //As usual, check for null values
        if(values == null || combine == null){
            throw new ArgumentException("Values and combine cannot be null elements.");
        }

        double finalValue = initial;

        foreach (double v in values){
            finalValue = combine(finalValue, v);
        }

        return finalValue;
    }

    public static List<double> CleanAndCalibrate(
        IReadOnlyList<double> rawSamples,
        double minimum,
        double maximum,
        double offset,
        double gain)
        ///in this method, we will use the previously created methods to clean and calibrate values
        /// First filter through all the usable and un-usable values.
        /// then calibrate
        /// the calibration is (raw-offset)*gain
        /// Finally return the calibrated values. 
    {
        // first check for valid inputs
        if (rawSamples == null){
            throw new ArgumentException("Raw sample data must be a valid input.");
        }
        if (double.IsInfinity(minimum)||double.IsInfinity(maximum)||double.IsInfinity(offset)||double.IsInfinity(gain)){
            throw new ArgumentException("Input parameters cannot be infinite values.");
        }
        if(minimum>maximum){
            throw new ArgumentException("Maximum value must be greater than minimum.");
        }
        if (gain<=0){
            throw new ArgumentException("Gain must be a positive value.");
        }

        List<double> finalized = new();

        //check if values are greater than min and smaller than max using filter
        finalized = Filter(rawSamples, x => x >= minimum);
        finalized = Filter(finalized, x => x <= maximum);

        //clean the filtered values using map
        finalized = Map(finalized, x => (x-offset)*gain);

        return finalized;
    }

    public static double RootMeanSquare(
        IReadOnlyList<double> samples)
    {
        // TODO: Implement using Map and Reduce (no loops).
        throw new NotImplementedException();
    }

    public static double FractionAboveThreshold(
        IReadOnlyList<double> samples,
        double threshold)
    {
        // TODO: Implement using Reduce (no loops).
        throw new NotImplementedException();
    }
}
