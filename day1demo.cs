using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

public class CodeExercises
{
    // Exercise 1: Calculate the Mean and Median of a Numeric Array
    public static (double mean, double median) CalculateStatistics(double[] x)
    {
        // Calculate the mean of the array
        double meanValue = x.Average();

        // Calculate the median by sorting the array and finding the middle value
        double medianValue = CalculateMedian(x);

        // Return the mean and median as a tuple
        return (meanValue, medianValue);
    }

    private static double CalculateMedian(double[] x)
    {
        // Sort the array and calculate the median
        var sortedX = x.OrderBy(n => n).ToArray();
        int mid = sortedX.Length / 2;
        return (sortedX.Length % 2 != 0) ? sortedX[mid] : (sortedX[mid] + sortedX[mid - 1]) / 2;
    }

    // Exercise 2: Generate a Sequence of Numbers from 1 to 10
    public static int[] GenerateSequence()
    {
        // Generate a sequence of numbers from 1 to 10
        return Enumerable.Range(1, 10).ToArray();
    }

    // Exercise 3: Apply a Function to Each Element of a List
    public static List<TResult> ApplyFunction<T, TResult>(List<T> lst, Func<T, TResult> func)
    {
        // Apply the function to each element of the list and return the transformed list
        return lst.ConvertAll(func);
    }

    // Exercise 4: Calculate the Variance of a Numeric Array
    public static double CalculateVariance(double[] x)
    {
        // Calculate the mean of the array
        double avg = x.Average();

        // Calculate the variance by summing the squared differences from the mean
        return x.Select(val => (val - avg) * (val - avg)).Average();
    }

    // Exercise 5: Calculate the Inverse of a Matrix
    public static double[] InvertMatrix(double[] mat)
    {
        // Check if the matrix is square; if not, throw an exception
        if (mat.GetLength(0) != mat.GetLength(1))
            throw new ArgumentException("Input must be a square matrix");

        // Implement or use a library method to invert the matrix and return the result
        return Invert(mat);
    }

    private static double[] Invert(double[] matrix)
    {
        // Placeholder for matrix inversion logic
        return new double[] { 1.0 }; // Replace with actual inversion logic
    }

    // Exercise 6: Refactor a Function with Input Validation
    public static double CalculateSum(double[] x)
    {
        // Validate that the input is not null or empty; if invalid, throw an exception
        if (x == null || x.Length == 0)
            throw new ArgumentException("Input must be a non-empty numeric array");

        // Calculate the sum of the array and return the result
        return x.Sum();
    }

    // Exercise 7: Create a Scatter Plot
    public static void PlotScatter(double[] x, double[] y)
    {
        // Create a chart and configure its properties
        Chart chart = new Chart();
        ChartArea chartArea = new ChartArea();
        chart.ChartAreas.Add(chartArea);

        // Add data points to the series from the input arrays
        Series series = new Series
        {
            Name = "Scatter",
            ChartType = SeriesChartType.Point
        };
        
        for (int i = 0; i < x.Length; i++)
        {
            series.Points.AddXY(x[i], y[i]);
        }

        // Add the series to the chart and display it in a form
        chart.Series.Add(series);
        chart.Dock = DockStyle.Fill;

        Form form = new Form();
        form.Controls.Add(chart);
        Application.Run(form);
    }
}
