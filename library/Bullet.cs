using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public abstract class Bullet : IDisposable
    {
        protected System.Windows.Vector direction;
        protected Point coords;
        protected Image img;
        protected int speed;
        public bool _disposed = false;
        public void CheckCollisions(List<Entity> ent)
        {
            foreach (Entity e in ent)
            {
                Point buffcent = e.GetCoords();
                if (this.coords.X >= (buffcent.X - 10) && (this.coords.X <= (buffcent.X + 20)))
                {
                    if (this.coords.Y >= (buffcent.Y - 10) && (this.coords.Y <= (buffcent.Y + 20)))
                    {
                        this.BlowUp(buffcent, ent);
                    }
                }
            }
        }
        protected abstract void BlowUp(Point point, List<Entity> ent); //ma mane go' is to blow up and then act like a don kno nobodeh EKEKEKKE
        public void Update(List <Entity> ent)
        {
            coords.X += (int)(direction.X * speed);
            coords.Y += (int)(direction.Y * speed);
            this.CheckCollisions(ent);
        }

        public void Draw(Graphics g)
        {
            g.DrawImage(img, coords);
        }

        public void Dispose()
        {
            Dispose(true);
            this.img = null;
            this.coords.X = 0;
            this.coords.Y = 0;
            this.speed = 0;
            GC.SuppressFinalize(this);
            
        }
        ~Bullet()
        {
            Dispose(false);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
            }
        }
    }

    public class RegularBullet : Bullet
    {
        public RegularBullet(Point from, System.Windows.Vector direction)
        {
            speed = 30;
            img = Image.FromFile("Res/regular_bullet.png");
            this.coords = from;
            this.direction = direction;
        }


            override protected void BlowUp(Point point, List<Entity> ent)
        {
            foreach (Entity e in ent)
            {
                if (point.X >= (e.GetCoords().X - 30) && (point.X <= (e.GetCoords().X + 30)))
                {
                    if (point.Y >= (e.GetCoords().Y - 30) && (point.Y <= (e.GetCoords().Y + 30)))
                    {
                        e.Hurt(3);
                        
                    }
                }
            }
            this.Dispose();

        }

  
    }


    public class Bomb : Bullet
    {
        public Bomb(Point from, System.Windows.Vector direction)
        {
            speed = 30;
            img = Image.FromFile("Res/regular_bullet.png");
            this.coords = from;
            this.direction = direction;
        }


        override protected void BlowUp(Point point, List<Entity> ent)
        {
            foreach (Entity e in ent)
            {
                if (point.X >= (e.GetCoords().X - 70) && (point.X <= (e.GetCoords().X + 70)))
                {
                    if (point.Y >= (e.GetCoords().Y - 70) && (point.Y <= (e.GetCoords().Y + 70)))
                    {
                        e.Hurt(10);

                    }
                }
            }
            this.Dispose();

        }


    }
}


