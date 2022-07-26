using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookshelf
{
    internal class MenuActionService
    {
        private List<MenuAction> menuActions = new List<MenuAction>();

        public void AddNewAction(int id, string name, string menuCategory)
        {
            MenuAction menuAction = new MenuAction() { Id = id, Name = name, MenuCategory = menuCategory };
            menuActions.Add(menuAction);
        }

        public List<MenuAction> GetMenuActionByMenuCategory(string menuCategory)
        {
            List<MenuAction> result = new List<MenuAction>();
            foreach (var menuAction in menuActions)
            {
                if(menuAction.MenuCategory == menuCategory)
                {
                    result.Add(menuAction); 
                }
            }
            

            return result;
        }
    } 
}
