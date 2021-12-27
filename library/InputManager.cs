using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace library
{
     public class InputManager
    {
        public bool isPressed = false;
        public Point p = new Point();

        public InputManager()
        {
         isPressed = false;
         p = new Point(0, 0);
    }
    }
}
