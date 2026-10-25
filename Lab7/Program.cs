// Lab 7
// Student name:
// Student number:

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
    {
        // TODO: Implement Map.
        throw new NotImplementedException();
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
