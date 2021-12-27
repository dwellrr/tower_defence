using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace library
{
    public class Grid
    {
        public Cell[][] cells = new Cell[29][];

        public Grid() {

            for (int i = 0; i < 29; i++)
            {
                cells[i] = new Cell[21];
                for(int j = 0; j <21; j++)
                {
                    cells[i][j] = new Cell(i, j);
                }

            }
        }

        public Cell FindCellByCoords(Point p)
        {
            double n, m;
            int i, j;
            n = (double)p.X / 50;
            m = (double)p.Y / 50;
            i = Convert.ToInt32(Math.Floor(n));
            j = Convert.ToInt32(Math.Floor(m));
            return cells[i][j];
        }


        public void Draw(Graphics g)
        {
            for(int i = 0; i < 29; i++)
            {
                for(int j = 0; j < 21; j++)
                {
                    cells[i][j].Draw(g);
                }
            }
        }
    }
}
