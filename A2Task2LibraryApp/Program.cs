/*
 * Program.cs
 * -----------
 * This class is the main application.
 *
 * Tasks:
 * - It instantiates the Library class and starts the interactive menu system
 *   which allows the user to borrow, return, and list books in the library.
 *
 * Extras:
 * - A seperate class was made for Library to demonstrate the need for abstraction and encapsulation.
 */

public class Program
{
    public static void Main(string[] args)
    {
        Library libraryController = new Library();

        libraryController.MenuDisplay();
    }
}