using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace library
{
    public class Cell
    {
        public bool isEmpty;
        public int row; 
        public int col;
        public int sideLength = 50;
        public int xOffset = 10;

        Image empty = Image.FromFile("Res/empty_tile.png");


        Cell()
        {
            isEmpty = true;
            row = 0;
            col = 0;
        }
        public Cell(int r, int c)
        {
            isEmpty = true;
            row = r;
            col = c;
        }

        public void Draw(Graphics graphics)
        {
            graphics.DrawImage(empty, (row * sideLength + xOffset), (col * sideLength));
        }
        public void Draw(Graphics graphics, Image img)
        {
            graphics.DrawImage(img, (row * sideLength + xOffset), (col * sideLength));
        }

        public Point GetCenter()
        {
            Point point = new Point();
            point.X = row * sideLength + 20;
            point.Y = col * sideLength + 20;
            return point;
        }
    }
}
