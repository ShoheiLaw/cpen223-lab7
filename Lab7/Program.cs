// Lab 7
// Student name: Shohei Law
// Student number: 12133013

using System;
using System.Collections.Generic;

Console.WriteLine("CPEN223 Lab 7");

static int passed = 0, failed = 0;

static void CheckList(string name, List<double> actual, params double[] expected)
{
    bool ok = actual != null && actual.Count == expected.Length;
    for (int i = 0; ok && i < expected.Length; i++)
        if (Math.Abs(actual[i] - expected[i]) > 1e-9) ok = false;
    Report(name, ok);
}

static void CheckValue(string name, double actual, double expected)
{
    Report(name, Math.Abs(actual - expected) < 1e-6);
}

static void CheckThrows(string name, Action action)
{
    try { action(); Report(name, false); }
    catch (ArgumentException e) when (e.GetType() == typeof(ArgumentException)) { Report(name, true); }
    catch { Report(name, false); }
}

static void Report(string name, bool ok)
{
    if (ok) passed++; else { failed++; Console.WriteLine("FAILED: " + name); }
}

static void Main()
{
    CheckList("Map squares", EmgAnalyzer.Map(new List<double>() { 1, 2, 3 }, x => x * x), 1, 4, 9);
    CheckList("Map empty", EmgAnalyzer.Map(new List<double>(), x => x * 2));
    CheckThrows("Map null list", () => EmgAnalyzer.Map(null, x => x));

    CheckList("Filter positives", EmgAnalyzer.Filter(new List<double>() { -2, 0, 3, 5 }, x => x > 0), 3, 5);
    CheckList("Filter none match", EmgAnalyzer.Filter(new List<double>() { 1, 2, 3 }, x => x > 5));
    CheckThrows("Filter null keep", () => EmgAnalyzer.Filter(new List<double>() { 1 }, null));

    CheckValue("Reduce sum", EmgAnalyzer.Reduce(new List<double>() { 1, 2, 3, 4 }, 0, (s, x) => s + x), 10);
    CheckValue("Reduce product", EmgAnalyzer.Reduce(new List<double>() { 2, 3, 4 }, 1.0, (r, x) => r * x), 24);
    CheckValue("Reduce empty", EmgAnalyzer.Reduce(new List<double>(), 7.0, (s, x) => s + x), 7);

    CheckList("Clean doc example",
        EmgAnalyzer.CleanAndCalibrate(new List<double>() { 0.0, 2.0, double.NaN, 3.0, 100.0 }, 1, 5, 1, 2), 2, 4);
    CheckList("Clean inclusive bounds",
        EmgAnalyzer.CleanAndCalibrate(new List<double>() { 1.0, 5.0 }, 1, 5, 0, 1), 1, 5);
    CheckThrows("Clean NaN offset",
        () => EmgAnalyzer.CleanAndCalibrate(new List<double>() { 2.0 }, 1, 5, double.NaN, 1));
    CheckThrows("Clean min > max",
        () => EmgAnalyzer.CleanAndCalibrate(new List<double>() { 2.0 }, 5, 1, 0, 1));
    CheckThrows("Clean gain <= 0",
        () => EmgAnalyzer.CleanAndCalibrate(new List<double>() { 2.0 }, 1, 5, 0, 0));

    CheckValue("RMS doc example", EmgAnalyzer.RootMeanSquare(new List<double>() { 3.0, 4.0 }), 3.5355339);
    CheckThrows("RMS empty", () => EmgAnalyzer.RootMeanSquare(new List<double>()));

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
        /// This method computes the root mean square of the list of values
        /// As per the instructions, we must use map to compute the squares and reduce to compute the sum.
        /// sidenote, use filter to check if any values are nan or infinite
    {
        //check for throw arguments for null, empty, or if any values are NaN or infinity
        if (samples == null || samples.Count == 0){
            throw new ArgumentException("Input list of samples cannot be null or empty");
        }
        //for NaN or infinity, since we can't write a loop to check each element in samples, use filter to check if the count is the same after filtering

        List<double> testFinite = Filter(samples, x => !double.IsInfinity(x));
        testFinite = Filter(testFinite, x => !double.IsNaN(x));
        if (!(testFinite.Count == samples.Count)){
            throw new ArgumentException("Samples cannot contain infinite or NaN values.");
        }

        List<double> squaredValues = Map(samples, x => x*x);
        double summedSquares = Reduce(squaredValues, 0.0, (result, x) => result + x);
        double finalValue = Math.Sqrt(summedSquares/samples.Count);

        return finalValue;
    }

    public static double FractionAboveThreshold(
        IReadOnlyList<double> samples,
        double threshold)
        ///Method goal: 
        /// A value is valid if its absolute value is equal to or above the threshold
        /// find the fraction of the numbers of valid values over the count of all values. (# of valid values)/(# of values)
        /// implementation: use reduce to find the number of valid values and use count for the number of values, then compute the fraction
        /// Finally return the fraction
    {
        //Throw arguments, first if samples is null or empty
        if (samples == null || samples.Count == 0){
            throw new ArgumentException("List of samples cannot be null or empty");
        }
        //check if any values in samples are inifinite or NaN similar to last method
        List<double> testFinite = Filter(samples, x => !double.IsInfinity(x));
        testFinite = Filter(testFinite, x => !double.IsNaN(x));
        if (!(testFinite.Count == samples.Count)){
            throw new ArgumentException("Samples cannot contain infinite or NaN values.");
        }
        //last, check if threshold is finite and positive
        if(double.IsInfinity(threshold)||threshold < 0||double.IsNaN(threshold)){
            throw new ArgumentException("Threshold value cannot be infinite or negative.");
        }

        double tValues = Reduce(samples, 0.0, (result, x) => Math.Abs(x) > threshold ? result+1.0: result +0.0);
        double sampleCount = samples.Count;
        double finalValue = tValues/sampleCount;

        return finalValue;
    }
}
