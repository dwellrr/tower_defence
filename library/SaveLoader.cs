using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace library
{
    public class SaveLoader
    {
        GameCore game;
        StreamReader reader = new StreamReader("Save.txt");
        public string line;

        public SaveLoader(GameCore g)
        {
            game = g;
        }

        string GetBuild(string a)
        {
            string res = "";
            foreach(char c in a)
            {
                if (c != ' ')
                    res += c;
                else
                    break;
            }
            return res;
        }

        Point GetPoint(string a)
        {
            int x = -1;
            int y = -1;
            string buf = "";
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == '[')
                {
                    i++;
                    while (a[i] != ',')
                    {
                        buf += a[i];
                            i++;
                    }
                    x = Int32.Parse(buf);
                    i++;
                    buf = "";
                    while (a[i] != ']')
                    {
                        buf += a[i];
                        i++;
                    }
                    y = Int32.Parse(buf);
                }
            }
            return new Point(x, y);
        }

        void BuildFromEntry(string entry, Point p)
        {
            switch (entry)
            {
                case "reg1":
                    game.entityManager.AddBuilding(game.grid.cells[p.X][p.Y], new RegCreator());
                    break;
                case "tri1":
                    game.entityManager.AddBuilding(game.grid.cells[p.X][p.Y], new TripleCreator());
                    break;
                case "bom1":
                    game.entityManager.AddBuilding(game.grid.cells[p.X][p.Y], new BombCreator());
                    break;
                case "road":
                    game.entityManager.AddRoad(game.grid.cells[p.X][p.Y]);
                    break;
                case "mother":
                    game.entityManager.AddMother(game.grid.cells[p.X][p.Y]);
                    break;
            }
        }

        public void LoadSave()
        {
            game.entityManager.DeleteAll();
            game.grid = new Grid();
            game.money = 125;
            StreamReader reader = new StreamReader("Save.txt");
            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                BuildFromEntry(GetBuild(line), GetPoint(line));
            }
           

        }

    }
}
