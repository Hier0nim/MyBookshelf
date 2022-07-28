﻿namespace MyBookshelf
{
    public class BookService
    {
        public List<Book> Books { get; set; }

        public BookService()
        {
            Books = new List<Book>();
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

        public void BookManagementDecisionTree(MenuActionService actionService)
        {
            bool isMenuRunning = true;
            while (isMenuRunning)
            {
                var keyInfo = MenuView(actionService, "BookManagement");

                switch (keyInfo.KeyChar)
                {
                    case '1':
                        AddNewBook();
                        break;
                    case '2':
                        var id = GetBookISBN();
                        RemoveBook(id);
                        break;
                    case '3':
                        id = GetBookISBN();
                        if(CheckIfBookExists(id))
                        {
                            Book bookToModify = GetExistingBook(id);
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



        private void AddNewBook()
        {
            Book book = new Book();

            Console.Clear();
            Console.WriteLine("Enter book categories separeted by comma.");
            var categoryString = Console.ReadLine();

            Console.WriteLine("Enter book title.");
            var title = Console.ReadLine();

            Console.WriteLine("Enter the ISBN of the book without hyphens(-)");
            var ISBN = Console.ReadLine();
            int id;
            while (int.TryParse(ISBN, out id) == false)
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

            string[] categories = categoryString.Split(',');
            foreach(var category in categories)
            {
                book.Categories.Add(category);
            }
            book.ISBN = id;
            book.Title = title;
            book.Description = description;
            book.IsRead = isRead;

            Console.Clear();
            Console.WriteLine("Are you sure you want to add this book? y/n");
            Console.WriteLine($"Categories: {categoryString}");
            Console.WriteLine($"ISBN: {ISBN}");
            Console.WriteLine($"Title: {title}");
            Console.WriteLine($"Description: {description}");
            Console.WriteLine($"isAlreadyRead: {isRead}");

            if(IfSure())
            {
                Books.Add(book);
                Console.WriteLine($"Book has not been added");
                System.Threading.Thread.Sleep(500);
            }
            else
            {
                Console.WriteLine($"Book has not been added");
                System.Threading.Thread.Sleep(500);

            }
        }

        public int GetBookISBN()
        {
            Console.Clear();
            Console.WriteLine("Enter the ISBN of the book without hyphens(-).");
            var ISBN = Console.ReadLine();
            int id;

            while (int.TryParse(ISBN, out id) == false)
            {
                Console.WriteLine("Uncorrect ISBN format!");
                Console.WriteLine("Enter the ISBN of the book without hyphens(-).");
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
                    string categories = string.Join(",", book.Categories);
                    Console.WriteLine($"Category: {categories}");
                    Console.WriteLine($"ISBN: {book.ISBN}");
                    Console.WriteLine($"Title: {book.Title}");
                    Console.WriteLine($"Description: {book.Description}");
                    Console.WriteLine($"isAlreadyRead: {book.IsRead}");
                    
                    if (IfSure())
                    {
                        bookToRemove = book;
                    }
                }
            }
            if (isFound == false)
                Console.WriteLine($"Book with id: {id} has not been found.");
            else
                Console.WriteLine($"Book with id: {id} has been removed.");
            Books.Remove(bookToRemove);
            System.Threading.Thread.Sleep(500);
        }


        public Book GetExistingBook(int id)
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

        public bool CheckIfBookExists(int id)
        {
            bool isFound = false;
            foreach (var book in Books)
            {
                if (book.ISBN == id)
                {
                    isFound = true;
                }
            }
            if (isFound == false)
            {
                Console.WriteLine($"Book with id: {id} has not been found.");
                System.Threading.Thread.Sleep(500);

            }
            return isFound;
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
            Console.WriteLine(newTitle);
            Console.WriteLine("? y/n");
            if (IfSure() == true)
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
            string categories = string.Join(",", book.Categories);
            Console.WriteLine($"Previous categories: {categories}");
            Console.WriteLine("Enter new categories separeted with comma.");
            string newCategories= Console.ReadLine();
            Console.Clear();
            Console.WriteLine($"Are tou sure tou want to change:");
            Console.WriteLine(categories);
            Console.WriteLine("for:");
            Console.WriteLine(newCategories);
            Console.WriteLine("? y/n");
            if (IfSure() == true)
            {
                string[] newCategoriesArray = newCategories.Split(',');
                book.Categories.Clear();
                foreach (var category in newCategoriesArray)
                {
                    book.Categories.Add(category);
                }
                Console.WriteLine("Category has been changed.");
            }
            else
            {
                Console.WriteLine("Category has NOT been changed.");
            }
            System.Threading.Thread.Sleep(500);
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
            if (IfSure() == true)
            {
                book.Description = newDescription;
            }
            else
            {
                Console.WriteLine("Description has NOT been changed.");
                System.Threading.Thread.Sleep(500);
            }
        }

        public bool IfSure()
        {
            bool isSure = false;
            var correct_Action = false;
            while (correct_Action == false)
            {
                var isSureKey = Console.ReadKey();
                switch (isSureKey.KeyChar)
                {
                    case 'y':
                        isSure = true;
                        correct_Action = true;
                        break;
                    case 'n':
                        isSure = false;
                        correct_Action = true;
                        break;
                    default:
                        Console.WriteLine("\n\rAction you entered does not exist.");
                        break;
                }
            }
            return isSure;
        }
    }
}
