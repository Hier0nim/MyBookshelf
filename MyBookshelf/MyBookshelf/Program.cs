namespace MyBookshelf
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MenuActionService actionService = new MenuActionService();
            BookService bookService = new BookService();
            actionService = Initialize(actionService);

            Console.WriteLine("Welcome to MyBookshelf app!");
            bool isAppRunning = true;
            while (isAppRunning)
            {
                var operation = bookService.MenuView(actionService, "MainMenu");

                switch (operation.KeyChar)
                {
                    case '1':
                        bookService.BookManagementDecisionTree(actionService);
                        break;
                    case '2':
                        bookService.ConditionOfBookshelfDecisionTree(actionService);
                        break;
                    case '3':
                        bookService.ChangeIsReadStatus();
                        break;
                    case '4':
                        isAppRunning = false;
                        break;
                    default:
                        Console.WriteLine("Action you entered does not exist");
                        break;
                }
            }
        }

        private static MenuActionService Initialize(MenuActionService actionService)
        {
            actionService.AddNewAction(1, "Book Management", "MainMenu");
            actionService.AddNewAction(2, "Condition of bookshelf", "MainMenu");
            actionService.AddNewAction(3, "Book has been read", "MainMenu");
            actionService.AddNewAction(4, "Exit", "MainMenu");


            actionService.AddNewAction(1, "Add book", "BookManagement");
            actionService.AddNewAction(2, "Delete book", "BookManagement");
            actionService.AddNewAction(3, "Modify book", "BookManagement");
            actionService.AddNewAction(4, "Go back", "BookManagement");

            actionService.AddNewAction(1, "By ISBN", "BookshelfCondition");
            actionService.AddNewAction(2, "By date", "BookshelfCondition");
            actionService.AddNewAction(3, "By categories", "BookshelfCondition");
            actionService.AddNewAction(4, "Only selected category", "BookshelfCondition");
            actionService.AddNewAction(5, "Go back", "BookshelfCondition");

            actionService.AddNewAction(1, "Edit title", "ModifyBook");
            actionService.AddNewAction(2, "Edit Category", "ModifyBook");
            actionService.AddNewAction(3, "Edit Description", "ModifyBook");
            actionService.AddNewAction(4, "Go back", "ModifyBook");
            return actionService;
        }
    }

}

// Greeting
// Action selection
// 1. Book Management
//  1(a). Addition of a book
//      - Selection of book category (more than 1)
//      - Details
//  1(b). Deletion of a book
//      - id or name
//  1(c). Modification of a book
//      - zmiana szczegolow
// 2. Condition of bookshelf
//  2(a) Everything
//  2(a) By categories
//  2(c) Selected category
//      - selection of category
// 3. Book has been read
//      - Selection of existing book.