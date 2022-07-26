using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookshelf
{
    internal class MenuActionService
    {
        private List<MenuAction> menuAction;

        public void AddNewAction(int id, string name, string menuCategory)
        {
            MenuAction action = new MenuAction() { Id = id, Name = name, MenuCategory = menuCategory };
            menuAction.Add(action);
        }

        public List<MenuAction> GetMenuActionByMenuCategory(string menuCategory)
        {
            List<MenuAction> result = new List<MenuAction>();

            return result;
        }
    }
}
