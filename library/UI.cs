using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace library
{
    public class UI
    {
        public string command;
        class UIButton
        {
            public int side;
            public Point coords;
            public Image image;
            public bool isEnabled;
            public void Draw(Graphics g)
            {
                g.DrawImage(image, coords);
            }

            public UIButton()
            {
                side = 100;
            }
        }

        UIButton reg1;
        UIButton triple1;
        UIButton bomb1;
        UIButton road;
        UIButton night;

        public UI()
        {
            command = "none";
            reg1 = new UIButton();
            triple1 = new UIButton();
            bomb1 = new UIButton();
            road = new UIButton();
            night = new UIButton();
            reg1.coords = new Point(1475, 10);
            reg1.image = Image.FromFile("Res/UI_reg1.png");
            triple1.coords = new Point(1625, 10);
            triple1.image = Image.FromFile("Res/UI_triple1.png");
            bomb1.coords = new Point(1775, 10);
            bomb1.image = Image.FromFile("Res/UI_bomb1.png");
            road.coords = new Point(1475, 160);
            road.image = Image.FromFile("Res/UI_road.png");
            night.coords = new Point(1475, 310);
            night.image = Image.FromFile("Res/UI_bomb1.png");
        }

        public void Draw(Graphics g)
        {
            reg1.Draw(g);
            triple1.Draw(g);
            bomb1.Draw(g);
            road.Draw(g);
            night.Draw(g);
        }

        public void UIClicked(Point mousePoint)
        {
            if((mousePoint.X >= 1475 && mousePoint.X <= 1575) && (mousePoint.Y >= 10 && mousePoint.Y <= 110) && reg1.isEnabled == true)
            {
                command = "reg1";
            }
            else if ((mousePoint.X >= 1625 && mousePoint.X <= 1725) && (mousePoint.Y >= 10 && mousePoint.Y <= 110) && triple1.isEnabled == true)
            {
                command = "triple1";
            }
            else if ((mousePoint.X >= 1775 && mousePoint.X <= 1875) && (mousePoint.Y >= 10 && mousePoint.Y <= 110) && bomb1.isEnabled == true)
            {
                command = "bomb1";
            }
            else if ((mousePoint.X >= 1475 && mousePoint.X <= 1575) && (mousePoint.Y >= 160 && mousePoint.Y <= 260) && road.isEnabled == true)
            {
                command = "road";
            }
         
        }

        public void CheckPrices(int money)
        {
            if(money < 50)
            {
                bomb1.isEnabled = false;
            }
            else
            {
                bomb1.isEnabled = true;
            }
            if (money < 40)
            {
                triple1.isEnabled = false;
            }
            else
            {
                triple1.isEnabled = true;
            }
            if (money < 25)
            {
                reg1.isEnabled = false;
            }
            else
            {
                reg1.isEnabled = true;
            }
            if (money < 10)
            {
                road.isEnabled = false;
            }
            else
            {
                road.isEnabled = true;
            }
        }
    }
}
