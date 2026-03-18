using System;
using System.Drawing;

namespace Breakout
{
    //*************************************************************
    //Enums
    //*************************************************************
    public enum MenuStates 
    {
        MainMenu,    
        Instructions,
        Controls,
        ModeSelect,
        Start,       
        Playing,
        Paused,
        GameOver
    };
    internal class Menu
    {
        //*************************************************************
        //Fields
        //*************************************************************
        private MenuStates mMenuState;

        //*************************************************************
        //Constructors
        //*************************************************************
        public Menu()
        {
            mMenuState = MenuStates.MainMenu;
        }

        //*************************************************************
        //Properties
        //*************************************************************
        public MenuStates MenuState     
        {
            get { return mMenuState; }
            set { mMenuState = value; }
        }

        //*************************************************************
        //Methods
        //*************************************************************
        public void Draw(Graphics g)
        {
            //handled from designer form
            //made labels in panels and controlled it in form1.cs
        }

    }
}
