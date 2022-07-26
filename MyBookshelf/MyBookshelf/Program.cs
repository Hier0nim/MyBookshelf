namespace MyBookshelf
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MenuActionService actionService = new MenuActionService();
            actionService = Initialize(actionService);

            Console.WriteLine("Welcome to MyBookshelf app!");
            Console.WriteLine("What do you want to do?");
            var mainMenu = actionService.GetMenuActionByMenuCategory("MainMenu");
            for(int i = 0; i < mainMenu.Count; i++)
            {
                Console.WriteLine($"{mainMenu[i].Id}. {mainMenu[i].Name}");
            }

            var operation = Console.ReadKey(); 
            switch(operation.KeyChar)
            {
                case '1':

                    break;
                case '2':
                    break;
                case '3':
                    break;
                default:
                    Console.WriteLine("Action does not exists");
                    break;
            }



        }

        private static MenuActionService Initialize(MenuActionService actionService)
        {
            actionService.AddNewAction(1, "Book Management", "MainMenu");
            actionService.AddNewAction(2, "Condition of bookshelf", "MainMenu");
            actionService.AddNewAction(3, "Book has been read", "MainMenu");


            actionService.AddNewAction(1, "Add book", "BookManagement");
            actionService.AddNewAction(2, "Delete book", "BookManagement");
            actionService.AddNewAction(3, "Modify book", "BookManagement");
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