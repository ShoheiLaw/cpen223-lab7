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
    {
        // TODO: Implement Filter.
        throw new NotImplementedException();
    }

    public static double Reduce(
        IReadOnlyList<double> values,
        double initial,
        Func<double, double, double> combine)
    {
        // TODO: Implement Reduce.
        throw new NotImplementedException();
    }

    public static List<double> CleanAndCalibrate(
        IReadOnlyList<double> rawSamples,
        double minimum,
        double maximum,
        double offset,
        double gain)
    {
        // TODO: Implement using Filter and Map (no loops).
        throw new NotImplementedException();
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
