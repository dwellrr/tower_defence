using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace library
{
    public abstract class Creator
    {
        public abstract ITower FactoryMethod();
        public BuildingOnGrid Build(Cell targetCell)
        {

            var product = FactoryMethod();
            return product.Build(targetCell);

        }
    }
    class RegCreator : Creator
    {
        public override ITower FactoryMethod()
        {
            return new RegTower();
        }
    }

    class TripleCreator : Creator
    {
        public override ITower FactoryMethod()
        {
            return new TripleTower();
        }
    }

    class BombCreator : Creator
    {
        public override ITower FactoryMethod()
        {
            return new BombTower();
        }
    }

    public interface ITower : BuildingOnGrid
    {
        void Fire(Point targetPoint, List<Bullet> bullets);

    }

    class RegTower : ITower
    {
        //do the thing
        public int health = 25;
        int price = 25;
        Image image = Image.FromFile("Res/reglvl1.png");
        Entity lockedTargetEntity;

        public int power = 5;
        public double speed = 1;
        int counter = 0;

        public Cell cell { get; set; }


        public void Fire(Point targetPoint, List<Bullet> bullets)
        {
            System.Windows.Vector vector = new System.Windows.Vector();
            System.Windows.Vector buff = new System.Windows.Vector();
            buff.X = targetPoint.X - this.cell.GetCenter().X;
            buff.Y = targetPoint.Y - this.cell.GetCenter().Y;
            vector.X = (buff.X) / (int)(buff.Length);
            vector.Y = (buff.Y) / (int)(buff.Length);

            bullets.Add(new RegularBullet(this.cell.GetCenter(), vector));


        }
        public BuildingOnGrid Build(Cell targetCell)
        {
            targetCell.isEmpty = false;
            this.cell = targetCell;
            return this;
        }
        public Entity FindTargrt(List<Entity> ent)
        {
            foreach (Entity e in ent)
            {
                Point buffcent = e.GetCoords();
                if (this.cell.GetCenter().X >= (buffcent.X - 200) && (this.cell.GetCenter().X <= (buffcent.X + 200)))
                {
                    if (this.cell.GetCenter().Y >= (buffcent.Y - 200) && (this.cell.GetCenter().Y <= (buffcent.Y + 200)))
                    {
                        lockedTargetEntity = e;
                        return e;

                    }
                }
            }
            return null;
        }
        public void Update(List<Entity> ent, List<Bullet> bullets)
        {
            if (!ent.Contains(lockedTargetEntity))
                lockedTargetEntity = null;
            if(lockedTargetEntity != null)
            {
         
             Point buffcent = lockedTargetEntity.GetCoords();
                     if (this.cell.GetCenter().X >= (buffcent.X - 200) && (this.cell.GetCenter().X <= (buffcent.X + 200)))
                {
                         if (this.cell.GetCenter().Y >= (buffcent.Y - 200) && (this.cell.GetCenter().Y <= (buffcent.Y + 200)))
                     {
                          buffcent = lockedTargetEntity.GetCoords();
                          Point coords = new Point();
                          coords = buffcent;
                          coords.X += (int)(lockedTargetEntity.GetVector().X * speed + 1);
                          coords.Y += (int)(lockedTargetEntity.GetVector().Y * speed + 1);
                        if(counter % 3 == 0)
                          this.Fire(coords, bullets);

                        counter++;
                        if (counter > 10)
                            counter = 0;
                    }
                }
         
            }
            else
            {
                lockedTargetEntity = FindTargrt(ent);
            }

            
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

    class TripleTower : ITower
    {
        //do the thing
        public int health = 50;
        int price = 40;
        Image image = Image.FromFile("Res/trilvl1.png");

        public int power = 5;
        public double speed = 1;

        public Cell cell { get; set; }
        Entity lockedTargetEntity;
        int counter = 0;


        public void Fire(Point targetPoint, List<Bullet> bullets)
        {
            System.Windows.Vector vector = new System.Windows.Vector();
            System.Windows.Vector buff = new System.Windows.Vector();
            buff.X = targetPoint.X - this.cell.GetCenter().X;
            buff.Y = targetPoint.Y - this.cell.GetCenter().Y;
            vector.X = (buff.X) / (int)(buff.Length);
            vector.Y = (buff.Y) / (int)(buff.Length);

            bullets.Add(new RegularBullet(this.cell.GetCenter(), vector));
        }
        public BuildingOnGrid Build(Cell targetCell)
        {
            targetCell.isEmpty = false;
            this.cell = targetCell;
            return this;
        }

        public Entity FindTargrt(List<Entity> ent)
        {
            foreach (Entity e in ent)
            {
                Point buffcent = e.GetCoords();
                if (this.cell.GetCenter().X >= (buffcent.X - 200) && (this.cell.GetCenter().X <= (buffcent.X + 200)))
                {
                    if (this.cell.GetCenter().Y >= (buffcent.Y - 200) && (this.cell.GetCenter().Y <= (buffcent.Y + 200)))
                    {
                        lockedTargetEntity = e;
                        return e;

                    }
                }
            }
            return null;
        }
        public void Update(List<Entity> ent, List<Bullet> bullets)
        {
            if (!ent.Contains(lockedTargetEntity))
                lockedTargetEntity = null;
            if (lockedTargetEntity != null)
            {

                Point buffcent = lockedTargetEntity.GetCoords();
                if (this.cell.GetCenter().X >= (buffcent.X - 200) && (this.cell.GetCenter().X <= (buffcent.X + 200)))
                {
                    if (this.cell.GetCenter().Y >= (buffcent.Y - 200) && (this.cell.GetCenter().Y <= (buffcent.Y + 200)))
                    {
                        buffcent = lockedTargetEntity.GetCoords();
                        Point coords = new Point();
                        coords = buffcent;
                        coords.X += (int)(lockedTargetEntity.GetVector().X * speed + 1);
                        coords.Y += (int)(lockedTargetEntity.GetVector().Y * speed + 1);

                        if (counter % 3 == 0)
                        {
                        this.Fire(coords, bullets);
                        coords.X += 60;
                        coords.Y += 60;
                        this.Fire(coords, bullets);
                        coords.X -= 120;
                        coords.Y -= 120;
                        this.Fire(coords, bullets);
                        }

                        counter++;
                        if (counter > 10)
                            counter = 0;
                        

                    }
                }

            }
            else
            {
                lockedTargetEntity = FindTargrt(ent);
            }

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

    class BombTower : ITower
    {
        //do the thing
        int price = 50;
        public int health = 50;
        Image image = Image.FromFile("Res/bomlvl1.png");

        public int power = 5;
        public double speed = 1;
        Entity lockedTargetEntity;
        int counter = 0;

        public Cell cell { get; set; }

        public Entity FindTargrt(List<Entity> ent)
        {
            foreach (Entity e in ent)
            {
                Point buffcent = e.GetCoords();
                if (this.cell.GetCenter().X >= (buffcent.X - 200) && (this.cell.GetCenter().X <= (buffcent.X + 200)))
                {
                    if (this.cell.GetCenter().Y >= (buffcent.Y - 200) && (this.cell.GetCenter().Y <= (buffcent.Y + 200)))
                    {
                        lockedTargetEntity = e;
                        return e;

                    }
                }
            }
            return null;
        }


        public void Fire(Point targetPoint, List<Bullet> bullets)
        {
            System.Windows.Vector vector = new System.Windows.Vector();
            System.Windows.Vector buff = new System.Windows.Vector();
            buff.X = targetPoint.X - this.cell.GetCenter().X;
            buff.Y = targetPoint.Y - this.cell.GetCenter().Y;
            vector.X = (buff.X) / (int)(buff.Length);
            vector.Y = (buff.Y) / (int)(buff.Length);

            bullets.Add(new Bomb(this.cell.GetCenter(), vector));


        }
        public BuildingOnGrid Build(Cell targetCell)
        {
            targetCell.isEmpty = false;
            this.cell = targetCell;
            return this;
        }
        public void Update(List<Entity> ent, List<Bullet> bullets)
        {
            if (!ent.Contains(lockedTargetEntity))
                lockedTargetEntity = null;
            if (lockedTargetEntity != null)
            {

                Point buffcent = lockedTargetEntity.GetCoords();
                if (this.cell.GetCenter().X >= (buffcent.X - 200) && (this.cell.GetCenter().X <= (buffcent.X + 200)))
                {
                    if (this.cell.GetCenter().Y >= (buffcent.Y - 200) && (this.cell.GetCenter().Y <= (buffcent.Y + 200)))
                    {
                        buffcent = lockedTargetEntity.GetCoords();
                        Point coords = new Point();
                        coords = buffcent;
                        coords.X += (int)(lockedTargetEntity.GetVector().X * speed + 1);
                        coords.Y += (int)(lockedTargetEntity.GetVector().Y * speed + 1);
                        if (counter % 10 == 0)
                            this.Fire(coords, bullets);

                        counter++;
                        if (counter > 10)
                            counter = 1;
                    }
                }

            }
            else
            {
                lockedTargetEntity = FindTargrt(ent);
            }


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
