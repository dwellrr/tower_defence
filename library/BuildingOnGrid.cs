using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace library
{
    public interface BuildingOnGrid
    {
        BuildingOnGrid Build(Cell targetCell);
        void Update(List<Entity> ent, List<Bullet> bullets);
        void Draw(Graphics g);
        int GetPrice();
        Cell GetCell();
        int GetHealth();
        void Attack(int dmg);

    }

    public class Road : BuildingOnGrid
    {   
        Image image = Image.FromFile("Res/road.png");
        public Cell cell;
        int price = 10;
        int health = 1;
        public BuildingOnGrid Build(Cell targetCell)
        {
            targetCell.isEmpty = false;
            this.cell = targetCell;
            return this;
        }
        public void Update(List<Entity> ent, List<Bullet> bullets)
        {

        }
        public void Draw(Graphics g)
        {
            this.cell.Draw(g, image);
        }
        public int GetPrice()
        {
            return price;
        }

        public Cell GetCell()
        {
            return this.cell;
        }
        public int GetHealth()
        {
            return this.health;
        }
        public void Attack(int dmg)
        {
            this.health -= dmg;
        }
    }

    public class Mother : BuildingOnGrid
    {
        Image image = Image.FromFile("Res/mother.png");
        public Cell cell;
        int price = 0;
        public int health = 100;
        public BuildingOnGrid Build(Cell targetCell)
        {
            targetCell.isEmpty = false;
            this.cell = targetCell;
            return this;
        }
        public void Update(List<Entity> ent, List<Bullet> bullets)
        {

        }
        public void Draw(Graphics g)
        {
            this.cell.Draw(g, image);
        }
        public int GetPrice()
        {
            return price;
        }
        public Cell GetCell()
        {
            return this.cell;
        }
        public int GetHealth()
        {
            return this.health;
        }
        public void Attack(int dmg)
        {
            this.health -= dmg;
        }
    }
}
