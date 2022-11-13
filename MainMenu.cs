using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKY
{
    internal class MainMenu
    {
        public int CursorY = 200;
        private enum MenuOptions
        {
            CHOOSE_SONG,
            OPTIONS_MENU
        }

        private MenuOptions _options;

        public void SelectOption()
        {
            // alert main game that option changed
            ChangeMenu?.Invoke();
        }

        public void ChangeCursorPosition(string position)
        {
            switch (position)
            {
                case "up":
                    if (CursorY == 200)
                    {
                        break;
                    }
                    else
                    {
                        CursorY = 200;
                        ChangeCursorPositionUp?.Invoke();
                        break;
                    }
                case "down":
                    if (CursorY == 250)
                    {
                        break;
                    }
                    else
                    {
                        CursorY = 250;
                        ChangeCursorPositionDown?.Invoke();
                        break;
                    }
            }
           
        }

        public event Action ChangeMenu;
        public event Action ChangeCursorPositionUp;
        public event Action ChangeCursorPositionDown;
    }
}
