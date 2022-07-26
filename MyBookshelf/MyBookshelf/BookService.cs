using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookshelf
{
    internal class BookService
    {
        public List<Book> Books { get; set; }     
        
        public BookService()
        {
            Books = new List<Book>();
        }

        public ConsoleKeyInfo BookManagementView(MenuActionService actionService)
        {
            var BookManagementMenu = actionService.GetMenuActionByMenuCategory("BookManagement");

            Console.Clear();
            Console.WriteLine("What would you like to do?");
            for (int i = 0; i < BookManagementMenu.Count; i++)
            {
                Console.WriteLine($"{BookManagementMenu[i].Id}. {BookManagementMenu[i].Name}");
            }

            var operations = Console.ReadKey();
            return operations;
        }
    }
}
