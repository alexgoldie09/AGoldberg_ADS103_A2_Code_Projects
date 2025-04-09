/*
 * Library.cs
 * -----------
 * This class simulates a simple library system that allows a user to borrow and return books.
 * Variables:
 * - hasQuit is used as a flag to end the program.
 * - libraryBooks is the dictionary to story the book by iD and title.
 * - borrowBookIDs is the dynamic list to track books that are borrowed.
 *
 * Tasks:
 * - Displays a menu to the user for various actions:
 *   1) List all books currently on loan.
 *   2) Return a borrowed book.
 *   3) List all books in the library.
 *   4) Borrow a new book.
 *   5) Exit the program.
 * - Continuously loops the menu until the user chooses to exit.
 * - Provides methods to list borrowed books, return a book, 
 *   list all books, and borrow a book.
 *
 * References:
 * Microsoft. (2025). Dictionary<TKey,TValue> Class (System.Collections.Generic). Microsoft.com.
 * https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2?view=net-9.0
 * Microsoft. (2025). List<T> Class (System.Collections.Generic). Microsoft.com.
 * https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1?view=net-9.0
 */

using System;
using System.Collections.Generic;

public class Library
{
    private bool hasQuit = false;
    private Dictionary<int, string> libraryBooks = new Dictionary<int, string>();
    // Step 2.2
    private List<int> borrowedBookIDs = new List<int>();

    /*
     * MenuDisplay() shows the main menu and processes user input to perform library operations.
     * - Initializes the library.
     * - Enters a loop that repeatedly displays the menu.
     *   + The user is prompted until they exit.
     * - This method is called by another script to use.
     */
    public void MenuDisplay()
    {
        // Step 2.1
        InitializeLibrary();

        while(!hasQuit)
        {
            // Step 3
            Console.Clear();
            Console.WriteLine("What would you like to do:");
            Console.WriteLine("1) List all books you have on loan");
            Console.WriteLine("2) Return a book");
            Console.WriteLine("3) List all books in the library");
            Console.WriteLine("4) Borrow a book");
            Console.WriteLine("5) Exit");

            int input = GetValidInput();

            Console.Clear();
            MenuSwitch(input);

            if (!hasQuit)
            {
                Console.WriteLine("\nPress Enter to return to the main menu...");
                Console.ReadLine();
            }
        }
    }

    /*
     * InitializeLibrary() populates the libraryBooks dictionary with a set of predefined books.
     * - Each book is added with a unique ID and a corresponding title, 
     *   similar to the brief.
     */
    private void InitializeLibrary()
    {
        libraryBooks[100] = "Animal Farm";
        libraryBooks[101] = "Don Quixote";
        libraryBooks[102] = "To Kill a Mockingbird";
        libraryBooks[103] = "Moby Dick";
        libraryBooks[104] = "The Great Gatsby";
        libraryBooks[105] = "Pride and Prejudice";
        libraryBooks[106] = "Harry Potter and the Sorcerer's Stone";
        libraryBooks[107] = "The Wizard of Oz";
        libraryBooks[108] = "Charlie and the Chocolate Factory";
        libraryBooks[109] = "Lord of the Flies";
    }

    /*
     * MenuSwitch() executes a library operation based on the user's menu choice.
     */
    private void MenuSwitch(int input)
    {
        switch (input)
        {
            case 1:
                ListBooksOnLoan();
                break;
            case 2:
                ReturnBook();
                break;
            case 3:
                ListAllBooks();
                break;
            case 4:
                BorrowBook();
                break;
            case 5:
                hasQuit = true;
                Console.WriteLine("Exiting program. Goodbye!");
                break;
        }
    }

    /*
     * GetValidInput() returns an int between 1 and 5.
     * - User is prompted until correct input is given.
     */
    private int GetValidInput()
    {
        int choice;
        while(true)
        {
            Console.Write("\nEnter choice (1–5): ");
            string? input = Console.ReadLine();

            if (int.TryParse(input, out choice) && choice >= 1 && choice <= 5)
                return choice;

            Console.WriteLine("Invalid input. Please enter a number from 1 to 5.");
        }
    }

    /*
     * ListBooksOnLoan() displays the list of books that the user has currently borrowed.
     * - If no books are on loan, notifies the user.
     * - Otherwise, iterates through the borrowedBookIDs list and prints the details of each borrowed book.
     */
    private void ListBooksOnLoan()
    {
        Console.WriteLine("Listing all books you have on loan...");
        if (borrowedBookIDs.Count == 0)
        {
            Console.WriteLine("You have no books on loan.");
            return;
        }

        Console.WriteLine("Books you have on loan:");
        foreach (int id in borrowedBookIDs)
        {
            if (libraryBooks.TryGetValue(id, out string? title))
                Console.WriteLine($"{id}: {title}");
        }
    }

    /*
     * ReturnBook() allows the user to return a borrowed book.
     * - If no books are currently borrowed, informs the user.
     * - Displays a list of borrowed books and prompts the user to enter the ID of the book to return.
     * - Validates the input and, if valid, removes the book from the borrowed list.
     */
    private void ReturnBook()
    {
        Console.WriteLine("Returning a book...");
         if (borrowedBookIDs.Count == 0)
        {
            Console.WriteLine("You have no books to return.");
            return;
        }
        Console.WriteLine("Books you have on loan:");
        foreach (int id in borrowedBookIDs)
        {
            if (libraryBooks.TryGetValue(id, out string? title))
                Console.WriteLine($"{id}: {title}");
        }

        Console.WriteLine("\nEnter the ID of the book you want to return:");

        string? input = Console.ReadLine();
        if (int.TryParse(input, out int bookId) && borrowedBookIDs.Contains(bookId))
        {
            borrowedBookIDs.Remove(bookId);
            Console.WriteLine("Book returned.");
        }
        else
        {
            Console.WriteLine("Invalid ID or book not borrowed.");
        }
    }

    /*
     * ListAllBooks() displays a list of all books available in the library.
     * - Iterates through the libraryBooks dictionary and prints each book's ID and title.
     */
    private void ListAllBooks()
    {
        Console.WriteLine("Listing all books in the library...");
        Console.WriteLine("Books in the library:");
        foreach (var book in libraryBooks)
        {
            Console.WriteLine($"{book.Key}: {book.Value}");
        }
    }

    /*
     * BorrowBook() allows the user to borrow a book from the library.
     * - Displays a list of books that are available to borrow (i.e., not already borrowed).
     * - Prompts the user to enter the ID of the book they wish to borrow.
     * - Validates the input and, if valid, adds the book ID to the borrowedBookIDs list.
     */
    private void BorrowBook()
    {
        Console.WriteLine("Borrowing a book...");
        Console.WriteLine("Available books to borrow:");
        foreach (var book in libraryBooks)
        {
            if (!borrowedBookIDs.Contains(book.Key))
                Console.WriteLine($"{book.Key}: {book.Value}");
        }

        Console.WriteLine("\nEnter the ID of the book you want to borrow:");
        string? input = Console.ReadLine();
        if (int.TryParse(input, out int bookId) && libraryBooks.ContainsKey(bookId) && !borrowedBookIDs.Contains(bookId))
        {
            borrowedBookIDs.Add(bookId);
            Console.WriteLine("Book borrowed!");
        }
        else
        {
            Console.WriteLine("Invalid ID or book already borrowed.");
        }
    }
}