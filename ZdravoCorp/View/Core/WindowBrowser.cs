using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ZdravoCorp.View.Core
{
    public class WindowBrowser
    {
        private List<UserControl> _navigable;
        private int _position;

        public WindowBrowser()
        {
            Navigable = new List<UserControl>();
            Position = Navigable.Count;
        }

        public List<UserControl> Navigable { get => _navigable; set => _navigable = value; }
        public int Position { get => _position; set => _position = value; }

        public void AddWindow(UserControl window)
        {
            if(window == null)
            {
                return;
            }
            else
            {
                if(Position == Navigable.Count)
                {
                    Navigable.Add(window);
                    Position = Navigable.Count;
                }
                else
                {
                    Navigable.RemoveRange(Position, Navigable.Count - Position);
                    Navigable.Add(window);
                    Position = Navigable.Count;
                }
            }
        }

        public UserControl BackWindow()
        {
            if(Position == 1)
            {
                return Navigable[0];
            }
            else
            {
                Position--;
                return Navigable[Position - 1];
            }
        }

        public UserControl ForwardWindow()
        {
            if (Position == Navigable.Count)
            {
                return Navigable[Position - 1];
            }
            else
            {
                Position++;
                return Navigable[Position - 1];
            }
        }

        public UserControl CurrentWindow()
        {
            return Navigable[Position - 1];
        }
    }
}
