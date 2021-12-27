using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public abstract class GameState
    {
        protected GameCore game;

        public GameState(GameCore game)
        {
            this.game = game;
        }

        public abstract void clickOnGameField(Cell cell);
        public abstract void clickOnUIButton();
        public abstract void clickOnNightButton();
    }

    class InitialSate : GameState
    {
        public InitialSate(GameCore game) : base(game)
        {
            this.game = game;
        }

        public override void clickOnGameField(Cell cell)
        {
            //nothing
        }
        public override void clickOnUIButton()
        {
            //enterBuildState
            game.ChangeState(new BuildingState(game));
        }
        public override void clickOnNightButton()
        {
            //enterNightState
            game.ChangeState(new Night(game));
        }

    }

    class BuildingState : GameState
    {

        public BuildingState(GameCore game) : base(game)
        {
            this.game = game;
        }

        public override void clickOnGameField(Cell cell)
        {
            //Build
            if (cell.isEmpty == true)
            {
                switch(game.ui.command)
                {
                    case "reg1":
                        game.entityManager.AddBuilding(cell, new RegCreator());
                        game.money -= 25;
                        break;
                    case "triple1":
                        game.entityManager.AddBuilding(cell, new TripleCreator());
                        game.money -= 40;
                        break;
                    case "bomb1":
                        game.entityManager.AddBuilding(cell, new BombCreator());
                        game.money -= 50;
                        break;
                    case "road":
                        game.entityManager.AddRoad(cell);
                        game.money -= 10;
                        break;

                }
                game.ui.command = "none";
                
                //EnterInitialState
                game.ChangeState(new InitialSate(game));
            }
        }
        public override void clickOnUIButton()
        {
            //either stay in the same stste with a different buildisng or
            //change state back to initial state in case the same button is clicked
        }
        public override void clickOnNightButton()
        {
            //DisableTheButton
        }

    }

    class Night : GameState
    {
        public Night(GameCore game) : base(game)
        {
            this.game = game;
            game.entityManager.CreateWave(game.grid.cells[0][10]);
            game.entityManager.CreateWave(game.grid.cells[15][0]);
            game.entityManager.CreateWave(game.grid.cells[28][10]);
            game.entityManager.CreateWave(game.grid.cells[15][20]);
        }

        public override void clickOnGameField(Cell cell)
        {
            //Nothing
        }
        public override void clickOnUIButton()
        {
            //Disabled
        }
        public override void clickOnNightButton()
        {
            //nothing... since the night passes itself, you can't willingly switch back anytime, unlike switching to night. i'll think of a way to switch
        }

    }
}
