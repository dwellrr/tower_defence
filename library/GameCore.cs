using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Input;



namespace library
{
    public class GameCore
    {

        public Grid grid;
        public Size Resolution { get; set; }
        public EntityManager entityManager;
        public UI ui;
        public GameState state;
        public SaveLoader saveLoader;
        public int money;


        public void Load()
        {
            this.state = new InitialSate(this);

            grid = new Grid();
            entityManager = new EntityManager();
            ui = new UI();
            saveLoader = new SaveLoader(this);
            saveLoader.LoadSave();
            money = 125;
            
        }

        public void ChangeState(GameState NewState)
        {
            this.state = NewState;
        }
        public void Unload()
        {

        }
        public void Update(TimeSpan gameTime, InputManager inp)
        {
            double gameTimeElapsed = gameTime.TotalMilliseconds / 1000;
            ui.CheckPrices(money);
            entityManager.UpdateEntities();
            if (entityManager.mother.GetHealth() <= 0)
            {
                saveLoader.LoadSave();
                ChangeState(new InitialSate(this));
            }

            if ((entityManager.CheckIfNoEntites()) && (state.ToString() == "library.Night"))
            {
                ChangeState(new InitialSate(this));
                money += entityManager.killed * 5;
                entityManager.killed = 0;
            }

            if (inp.isPressed == true)
            {   
                inp.isPressed = false;
                if (((inp.p.X >= 10) && (inp.p.X <= 1460)) && ((inp.p.Y >= 0) && (inp.p.Y <= 1050)))
                {
                    inp.p.X -= 10;
                    state.clickOnGameField(grid.FindCellByCoords(inp.p));

        
                    
                }

                if (((inp.p.X >= 1475) && (inp.p.X <= 1875)) && ((inp.p.Y >= 10) && (inp.p.Y <= 260)))
                {
                    ui.UIClicked(inp.p);

                    state.clickOnUIButton();



                }

                if ((inp.p.X >= 1475 && inp.p.X <= 1575) && (inp.p.Y >= 310 && inp.p.Y <= 410))
                {
                    state.clickOnNightButton();
                }

            }
        } 
        
        public void Draw(Graphics graphics)
        {
            grid.Draw(graphics);
            entityManager.Draw(graphics);
            ui.Draw(graphics);
            
        }
    
    }
}
