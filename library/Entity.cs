using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;

namespace library
{
    public class Entity
    {
        protected System.Drawing.Point coords;
        public int health;
        protected Vector vector;
        protected double speed;
        bool collided = false;
        protected BuildingOnGrid target;
        protected System.Drawing.Point center;
        Vector buff = new Vector(0, 0);
        protected Image img;

        public Entity(BuildingOnGrid target, System.Drawing.Point _initialLocation)
        {
            coords = new System.Drawing.Point(0, 0);
            vector = new Vector();
            this.target = target;
            center = target.GetCell().GetCenter();
        }
        public Entity()
        {
            coords = new System.Drawing.Point(0, 0);
            vector = new Vector();
        }

        public void Update()
        {
            this.CalculateVector();
            collided = this.CheckCollisionWithMother();
            if (collided == true)
                Attack();
            this.UpdatePosition();
           
        }

        public void Attack()
        {
            target.Attack(3);
        }

        public void UpdatePosition()
        {
            coords.X += (int)(vector.X * speed);
            coords.Y += (int)(vector.Y * speed);
        }

        public System.Drawing.Point GetCoords()
        {
            return this.coords;
        }
        public Vector GetVector()
        {
            return this.vector;
        }

        public void Draw(Graphics g)
        {
            g.DrawImage(img, coords);
        }

        public void CalculateVector()
        {
            buff.X = center.X - coords.X;
            buff.Y = center.Y - coords.Y;
            vector.X = (buff.X)/(int)(buff.Length);
            vector.Y = (buff.Y) / (int)(buff.Length);

        }
        public bool CheckCollisionWithMother()
        {
            if (coords.X >= (center.X - 10) && coords.X <= (center.X + 10))
                vector.X = 0;
            if (coords.Y >= (center.Y - 10) && coords.Y <= (center.Y + 10))
                vector.Y = 0;
            if (vector.X == 0 && vector.Y == 0)
                return true;
            else
                return false;
        }

        public void Hurt(int dmg)
        {
            this.health -= dmg;
        }
    }

    public class LittleGuy: Entity
    {
        public LittleGuy(BuildingOnGrid target, System.Drawing.Point _initialLocation)
        {
            this.target = target;
            health = 5;
            speed = 5;
            img = Image.FromFile("Res/little_guy.png");
            coords = _initialLocation;
            center = target.GetCell().GetCenter();
        }
    }
}
