using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Drawing;

namespace library
{
    public class EntityManager
    {
        List<Entity> entities;
        List<BuildingOnGrid> buildings;
        List<BuildingOnGrid> roads;
        List<Bullet> bullets;
        public BuildingOnGrid mother;
        public int killed = 0;

        public EntityManager()
        {
            entities = new List<Entity>();
            buildings = new List<BuildingOnGrid>();
            roads = new List<BuildingOnGrid>();
            bullets = new List<Bullet>();
            mother = new Mother();
        }

        public void AddBuilding(Cell TargetCell, Creator creator)
        {
            TargetCell.isEmpty = false;
            buildings.Add(creator.Build(TargetCell));
        }
        public void AddRoad(Cell TargetCell)
        {
            TargetCell.isEmpty = false;
            roads.Add(new Road().Build(TargetCell));
        }
        public void AddMother(Cell TargetCell)
        {
            TargetCell.isEmpty = false;
            mother = new Mother().Build(TargetCell);
        }
        public void AddEntity(Cell TargetCell)
        {
            entities.Add(new LittleGuy(mother, TargetCell.GetCenter()));
        }
        public void AddBullet(Point from, System.Windows.Vector direction)
        {
            bullets.Add(new RegularBullet(from, direction));
        }
        public void AddBomb(Point from, System.Windows.Vector direction)
        {
            bullets.Add(new Bomb(from, direction));
        }
        async public void CreateWave (Cell TargetCell)
        {
            Random rng = new Random();
            int q = rng.Next(3, 7);
            for (int i = 0; i < q; i++)
            {
                AddEntity(TargetCell);
                await Task.Delay(1000);
            }
            
        }

        public void DeleteAll()
        {
            for (int i = 0; i < buildings.Count; i++)
            {
                buildings = new List<BuildingOnGrid>();
            }
            for (int i = 0; i < roads.Count; i++)
            {
                roads = new List<BuildingOnGrid>();
            }
            for (int i = 0; i < entities.Count; i++)
            {
                entities = new List<Entity>();
            }
            mother = null;
        }

        public void UpdateEntities()
        {
            List<Bullet> newBullets = new List<Bullet>();
            List<Entity> newEntities = new List<Entity>();
            foreach (LittleGuy e in entities)
            {
                e.Update();
                if(e.health > 0)
                {
                    newEntities.Add(e);
                }
                else
                {
                    killed++;
                }
   
            }
            foreach(BuildingOnGrid b in buildings)
            {
                b.Update(this.entities, this.bullets);
            }
            foreach(Bullet b in bullets)
            {
                b.Update(entities);
                if (b._disposed != true)
                    newBullets.Add(b);
            }
            this.bullets = newBullets;
            this.entities = newEntities;
        }

        public bool CheckIfNoEntites()
        {
            bool isEmpty = !entities.Any();
            return isEmpty;
        }
        public void Draw(Graphics g)
        {
            for(int i = 0; i < buildings.Count; i++)
            {
                buildings[i].Draw(g);
            }
            foreach(BuildingOnGrid r in roads)
            {
                r.Draw(g);
            }
            mother.Draw(g);
            foreach(Entity e in entities)
            {
                e.Draw(g);
            }
            foreach(Bullet b in bullets)
            {
                b.Draw(g);
            }
        }
    }
}
