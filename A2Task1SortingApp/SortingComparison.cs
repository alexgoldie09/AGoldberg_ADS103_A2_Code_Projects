﻿/*
 * SortingComparison.cs
 * ----------------------
 * This class compares the performance of two sorting algorithms: Insertion Sort and Merge Sort.
 *
 * Tasks:
 *   - The Main method calls the DriverProgram for two different input files (as suggested by the assignment brief).
 *   - DriverProgram reads input, clones the data into separate arrays using the Stack library, and calls the respective sort methods.
 *   - InsertionSort is implemented using Programiz (2020) method but adjusted.
 *   - MergeSort is implemented using Pankaj (2022) method but adjusted.
 *   - The results are printed to the console, including a preview of the sorted arrays and timing information.
 *
 * Extras:
 *   - Built using one class due to the minimal amount of functions that needed to be abstracted.
 *
 * References:
 *   Microsoft. (2025). Stack.ToArray Method (System.Collections.Generic). Microsoft.com. 
 *   https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1.toarray?view=net-9.0
 *   Microsoft. (2025). Stopwatch Class (System.Diagnostics). Microsoft.com.
 *   https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.stopwatch?view=net-9.0
 *   Microsoft. (2025). System.IO Namespace (System.IO). Microsoft.com.
 *   https://learn.microsoft.com/en-us/dotnet/api/system.io?view=net-9.0
 *   Pankaj. (2022, August 4). Merge Sort Algorithm - Java, C, and Python Implementation | DigitalOcean. Www.digitalocean.com. 
 *   https://www.digitalocean.com/community/tutorials/merge-sort-algorithm-java-c-python
 *   Programiz. (2020). Insertion Sort Algorithm. Programiz.com. 
 *   https://www.programiz.com/dsa/insertion-sort
 */

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

public class SortingComparison
{
    /*
    * Main() is the class run on the start of the program.
    * - Defines the relative paths to the input files as strings.
    * - Runs the driver program for each input file.
    */
    public static void Main(string[] args)
    {
        string pathOne = "a2_task1_input1.txt";
        string pathTwo = "a2_task1_input2.txt";

        Console.WriteLine("Sorting first file...");
        DriverProgram(pathOne);
        Console.WriteLine("\nSorting second file...");
        DriverProgram(pathTwo);
    }

    /* 
    * DriverProgram() is a helper method that executes the entire sorting
    * process for a given input file.
    * - 1.1 Opens the input file.
    * - 1.2 Read all lines from the file and copy into an array.
    *   + Uses helper method ReadDataFromFile()
    *   + If data array is empty, returns out of program.
    * - 1.3 Sort first array using InsertionSort() and measure the time.
    * - 1.4 Retrieve the elapsed time for Insertion Sort.
    * - 1.5 Following brief, reads file contents once again to another array.
    *   + For future optimisation, it is noted you could copy both contents at the start of the method.
    * - 1.6 Sort second array using MergeSort() and measure the time.
    * - 1.7 Compare both sort times and output result.
    */
    public static void DriverProgram(string path)
    {
        // Block out this section if want to use dummy
        int[] dataInsert = ReadDataFromFile(path);

        // Using a dummy array for debugging
        // int[] dataInsert = new int[] { 5, 2, 9, 1, 6, 3, 15, 3, 18, 8 };

        // Safety check
        if(dataInsert == null || dataInsert.Length == 0) 
        {
            Console.WriteLine("Original array is empty!"); 
            return; 
        }

        // Printing first array for debugging and testing
        Console.WriteLine("\nOriginal Array:");
        PrintTrimmedArray(dataInsert, 15);

        Stopwatch stopwatch = Stopwatch.StartNew();
        InsertionSort(dataInsert);
        stopwatch.Stop();

        // Get time and print milliseconds (rounded to 6 decimal places for more preciseness) using the TimeSpan object
        TimeSpan tsInsertionSort = stopwatch.Elapsed;

        // Printing Insertion sorted array for debugging
        Console.WriteLine("\nInsertion Sorted Array:");
        PrintTrimmedArray(dataInsert, 20);

        // Block out this section if want to use dummy
        int[] dataMerge = ReadDataFromFile(path);

        // Using a dummy array for debugging
        // int[] dataMerge = new int[] { 5, 2, 9, 1, 6, 3, 15, 3, 18, 8 };

        // Safety check
        if(dataMerge == null || dataMerge.Length == 0) 
        {
            Console.WriteLine("Original array is empty!"); 
            return; 
        }

        // Second print proves the data was handled correctly
        Console.WriteLine("\nOriginal Array:");
        PrintTrimmedArray(dataMerge, 15);

        stopwatch.Restart();
        MergeSort(dataMerge, 0, dataMerge.Length - 1);
        stopwatch.Stop();

        // Get time and print milliseconds (rounded to 6 decimal places for more preciseness) using the TimeSpan object
        TimeSpan tsMergeSort = stopwatch.Elapsed;

        // Printing merge sorted array for debugging
        Console.WriteLine("\nMerge Sorted Array:");
        PrintTrimmedArray(dataMerge, 20);

        Console.WriteLine($"\nInsertion Sort Time: {tsInsertionSort.TotalMilliseconds:F6} ms");
        Console.WriteLine($"Merge Sort Time: {tsMergeSort.TotalMilliseconds:F6} ms");
        Console.WriteLine(tsInsertionSort < tsMergeSort ? "Insertion Sort was faster." : "Merge Sort was faster.");
    }

    /* 
    * PrintTrimmedArray() outputs a preview of the array limited to a specified number of elements.
    * - Uses string library Join() and array library Take() to combine only 
    *   the first elements of an int array up until the limiter to form a preview string.
    */
    private static void PrintTrimmedArray(int[] array, int limit = 10)
    {
        string preview = string.Join(", ", array.Take(limit));
        Console.WriteLine(preview + (array.Length > limit ? ", ..." : ""));
    }

    /* 
    * ReadDataFromFile() returns an int array read from the file, or an empty array if the file is not found.
    * - Using IO namespace File.Exists(), check to make sure the path is available.
    * - Read all lines of the file.
    * - The first line as per brief should contain a count of numbers.
    * - The second line contains the data that is space separated.
    * - Take only the 'count' numbers, convert them to int, and form an array using Stack library ToArray().
    */
    private static int[] ReadDataFromFile(string path)
    {
        if (!File.Exists(path))
        {
            Console.WriteLine("File not found: " + path);
            return new int[0];
        }

        string[] lines = File.ReadAllLines(path);

        int count = int.Parse(lines[0]);

        int[] data = lines[1].Split(' ')
                            .Take(count)
                            .Select(int.Parse)
                            .ToArray();

        return data;
    }

    /*
    * InsertionSort() implements the iterative sorting algorithm.
    * - It builds a sorted portion of the array one element at a time,
    *   shifting larger elements forward to make space for the current value.
    * Time Complexity:
    * - Worst case: O(n^2) for reverse sorted data.
    */
    private static void InsertionSort(int[] array)
    {
        int size = array.Length;

        // Iterate from the second element to the end of the array
        for (int i = 1; i < size; i++)
        {
            // Store the current element (key) to be inserted
            int key = array[i];
            int j = i - 1;

            // Move elements in the sorted portion that are greater than the key one position ahead
            while (j >= 0 && array[j] > key)
            {
                array[j + 1] = array[j];
                j--;
            }

            // Insert the key in its correct position
            array[j + 1] = key;
        }
    }

    /*
    * MergeSort() implements the recursive divide-and-conquer algorithm.
    * - It splits the array into halves, recursively sorts them by 
    *   dividing into sub-arrays, and then merges the sorted halves.
    * Time Complexity: 
    * - O(n log n) in all cases.
    */
    private static void MergeSort(int[] array, int left, int right)
    {
        // If the subarray has one or no elements, it is already sorted.
        if (left < right)
        {
            // Find the mid point
            int mid = left + (right - left) / 2;

            // Sort first and second halves 
            MergeSort(array, left, mid);
            MergeSort(array, mid + 1, right);

            // Merge the sorted halves back together
            Merge(array, left, mid, right);
        }
    }

    /*
    * Merge() combines the two sorted subarrays into one sorted array.
    * - Determine the sizes of the two subarrays to be merged.
    * - Create temporary arrays to hold copies of the subarrays.
    * - Copy data into temporary arrays.
    * - Initialize indexes for the temporary arrays and the merged portion.
    * - Merge elements from L and R back into the main array in sorted order.
    * - If there are remaining elements in L, copy them into the main array.
    * - If there are remaining elements in R, copy them into the main array.
    */
    private static void Merge(int[] array, int left, int mid, int right)
    {
        int i, j; // Initial assignment for the first two loops

        int n1 = mid - left + 1; // Numbers of elements in the first subarray
        int n2 = right - mid; // Number of elements in the second subarray

        int[] L = new int[n1];
        int[] R = new int[n2];

        for (i = 0; i < n1; i++) { L[i] = array[left + i]; }
        for (j = 0; j < n2; j++) { R[j] = array[mid + 1 + j]; }

        i = 0; // Reset to initial index of first subarray 
        j = 0; // Reset to initial index of second subarray 
        int k = left; // Initial index of merged subarray 

        while (i < n1 && j < n2) 
        {
            // Compare current elements of L and R, and insert the smaller one
            array[k++] = (L[i] <= R[j]) ? L[i++] : R[j++]; 
        }

        // Left side remaining copy
        while (i < n1) { array[k++] = L[i++]; }

        // Right side remaining copy
        while (j < n2) { array[k++] = R[j++]; }
    }
}