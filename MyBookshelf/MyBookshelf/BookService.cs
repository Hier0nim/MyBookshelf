using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookshelf
{
    public class BookService
    {
        public List<Book> Books { get; set; }     
        
        public BookService()
        {
            Books = new List<Book>();
        }

        public void BookManagement(MenuActionService actionService)
        {
            bool isMenuRunning = true;
            while (isMenuRunning)
            {
                var keyInfo =  MenuView(actionService, "BookManagement");

                switch (keyInfo.KeyChar)
                {
                    case '1':
                        AddNewBook();
                        break;
                    case '2':
                        var id = RemoveBookView();
                        RemoveBook(id); 
                        break;
                    case '3':
                        id = ModifyBookView();
                        Book bookToModify = ModifyBookViewStep2(id);
                        if (bookToModify.ISBN != -1)
                        {
                            ModifyBook(actionService, bookToModify);
                        }
                        break;
                    case '4':
                        isMenuRunning = false;
                        break;
                    default:
                        Console.WriteLine("Action you entered does not exist");
                        break;
                }
            }
        }

        public ConsoleKeyInfo MenuView(MenuActionService actionService, string selectedMenu)
        {
            var BookManagementMenu = actionService.GetMenuActionByMenuCategory(selectedMenu);

            Console.Clear();
            Console.WriteLine("What would you like to do?");
            for (int i = 0; i < BookManagementMenu.Count; i++)
            {
                Console.WriteLine($"{BookManagementMenu[i].Id}. {BookManagementMenu[i].Name}");
            }

            var operations = Console.ReadKey();
            return operations;
        }

        private void AddNewBook()
        {
            Book book = new Book();

            Console.Clear();
            Console.WriteLine("Enter book category.");
            var category = Console.ReadLine();

            Console.WriteLine("Enter book title.");
            var title = Console.ReadLine();

            Console.WriteLine("Enter the ISBN of the book without hyphens(-)");
            var ISBN = Console.ReadLine();
            int id;
            while(int.TryParse(ISBN, out id) == false)
            {
                Console.WriteLine("Uncorrect ISBN format!");
                Console.WriteLine("Enter the ISBN of the book without hyphens(-)");
                ISBN = Console.ReadLine();
            }

            Console.WriteLine("Enter a brief description of the book.");
            var description = Console.ReadLine();

            bool isRead = false;
            bool correct_Action = false;
            while (correct_Action == false)
            {
                Console.WriteLine("Have you read this book? y/n");
                var key = Console.ReadKey();
                
                switch (key.KeyChar)
                {
                    case 'y':
                        isRead = true;
                        correct_Action = true;
                        break;
                    case 'n':
                        isRead = false;
                        correct_Action = true;
                        break;
                    default:
                        Console.WriteLine("\n\rAction you entered does not exist.");
                        break;
                }
            }
     
            book.Category = category;
            book.ISBN = id;
            book.Title = title;
            book.Description = description;
            book.IsRead = isRead;

            Console.Clear();
            Console.WriteLine("Are you sure you want to add this book? y/n");
            Console.WriteLine($"Category: {category}");
            Console.WriteLine($"ISBN: {ISBN}");
            Console.WriteLine($"Title: {title}");
            Console.WriteLine($"Description: {description}");
            Console.WriteLine($"isAlreadyRead: {isRead}");

            correct_Action = false;
            while (correct_Action == false)
            {
                var sureToAdd = Console.ReadKey();
                switch (sureToAdd.KeyChar)
                {
                    case 'y':
                        Books.Add(book);
                        correct_Action = true;
                        break;
                    case 'n':
                        correct_Action = true;
                        break;
                    default:
                        Console.WriteLine("\n\rAction you entered does not exist.");
                        break;
                }
            }      
        }

        public int RemoveBookView()
        {
            Console.Clear();
            Console.WriteLine("Enter the ISBN of the book you want to delete without hyphens(-).");
            var ISBN = Console.ReadLine();
            int id;
         
            while (int.TryParse(ISBN, out id) == false)
            {
                Console.WriteLine("Uncorrect ISBN format!");
                Console.WriteLine("Enter the ISBN of the book you want to delete without hyphens(-).");
                ISBN = Console.ReadLine();
            }

          return id;
        }

        public void RemoveBook(int id)
        {
            bool isFound = false;
            Book bookToRemove = new Book();
            foreach (var book in Books)
            {
                if (book.ISBN == id)
                {
                    isFound = true; 
                    Console.Clear();
                    Console.WriteLine("Are you sure you want to delete this book? y/n");
                    Console.WriteLine($"Category: {book.Category}");
                    Console.WriteLine($"ISBN: {book.ISBN}");
                    Console.WriteLine($"Title: {book.Title}");
                    Console.WriteLine($"Description: {book.Description}");
                    Console.WriteLine($"isAlreadyRead: {book.IsRead}");

                    var correct_Action = false;
                    while (correct_Action == false)
                    {
                        var sureToDelete = Console.ReadKey();
                        switch (sureToDelete.KeyChar)
                        {
                            case 'y':
                                bookToRemove = book;
                                correct_Action = true;
                                break;
                            case 'n':
                                correct_Action = true;
                                break;
                            default:
                                Console.WriteLine("\n\rAction you entered does not exist.");
                                break;
                        }
                    }
                    break;
                }
            }
            if(isFound == false)  
                Console.WriteLine($"Book with id: {id} has not been found.");
            else    
                Console.WriteLine($"Book with id: {id} has been removed.");
            Books.Remove(bookToRemove);
            System.Threading.Thread.Sleep(500);
        }

        public int ModifyBookView()
        {
            Console.Clear();
            Console.WriteLine("Enter the ISBN of the book you want to modify without hyphens(-).");
            var ISBN = Console.ReadLine();
            int id;

            while (int.TryParse(ISBN, out id) == false)
            {
                Console.WriteLine("Uncorrect ISBN format!");
                Console.WriteLine("Enter the ISBN of the book you want to delete without hyphens(-).");
                ISBN = Console.ReadLine();
            }

            return id;
        }

        public Book ModifyBookViewStep2(int id)
        {
            bool isFound = false;
            Book bookToModify = new Book();
            bookToModify.ISBN = -1;
            foreach (var book in Books)
            {
                if (book.ISBN == id)
                {
                    isFound = true;
                    bookToModify = book;
                }
            }
            if (isFound == false)
            {
                Console.WriteLine($"Book with id: {id} has not been found.");
                System.Threading.Thread.Sleep(500);
                
            }
            return bookToModify;
        }

        public void ModifyBook(MenuActionService actionService, Book book)
        {
            bool isMenuRunning = true;
            while (isMenuRunning)
            {
                var keyInfo = MenuView(actionService, "ModifyBook");

                switch (keyInfo.KeyChar)
                {
                    case '1':
                        ModifyBookTitle(book);
                        break;
                    case '2':
                        ModifyBookCategory(book);
                        break;
                    case '3':
                        ModifyBookDescription(book);
                        break;
                    case '4':
                        isMenuRunning = false;
                        break;
                    default:
                        Console.WriteLine("Action you entered does not exist");
                        break;
                }
            }
        }

        public void ModifyBookTitle(Book book)
        {
            Console.Clear();
            Console.WriteLine($"Previous title: {book.Title}");
            Console.WriteLine("Enter yourn new title.");
            string newTitle = Console.ReadLine();
            Console.Clear();
            Console.WriteLine($"Are tou sure tou want to change:");
            Console.WriteLine(book.Title);
            Console.WriteLine("for:");
            Console.WriteLine(newTitle );
            Console.WriteLine("? y/n");
            if(SureToModify() == true)
            {
                book.Title = newTitle;
            }
            else
            {
                Console.WriteLine("Title has NOT been changed.");
                System.Threading.Thread.Sleep(500);
            }

        }
        public void ModifyBookCategory(Book book)
        {
            Console.Clear();
            Console.WriteLine($"Previous category: {book.Category}");
            Console.WriteLine("Enter yourn new category.");
            string newCategory = Console.ReadLine();
            Console.Clear();
            Console.WriteLine($"Are tou sure tou want to change:");
            Console.WriteLine(book.Category);
            Console.WriteLine("for:");
            Console.WriteLine(newCategory);
            Console.WriteLine("? y/n");
            if (SureToModify() == true)
            {
                book.Category = newCategory;
            }
            else
            {
                Console.WriteLine("Category has NOT been changed.");
                System.Threading.Thread.Sleep(500);
            }
        }
        public void ModifyBookDescription(Book book)
        {
            Console.Clear();
            Console.WriteLine($"Previous desription: {book.Description}");
            Console.WriteLine("Enter yourn new description.");
            string newDescription = Console.ReadLine();
            Console.Clear();
            Console.WriteLine($"Are tou sure tou want to change:");
            Console.WriteLine(book.Description);
            Console.WriteLine("for:");
            Console.WriteLine(newDescription);
            Console.WriteLine("? y/n");
            if (SureToModify() == true)
            {
                book.Description = newDescription;
            }
            else
            {
                Console.WriteLine("Description has NOT been changed.");
                System.Threading.Thread.Sleep(500);
            }
        }

        public bool SureToModify()
        {
            bool sureToModify = false;
            var correct_Action = false;
            while (correct_Action == false)
            {
                var sureToDelete = Console.ReadKey();
                switch (sureToDelete.KeyChar)
                {
                    case 'y':
                        sureToModify = true;
                        correct_Action = true;
                        break;
                    case 'n':
                        sureToModify = false;
                        correct_Action = true;
                        break;
                    default:
                        Console.WriteLine("\n\rAction you entered does not exist.");
                        break;
                }
            }
            return sureToModify;
        }
    }
}
 