using Microsoft.VisualBasic.FileIO;

namespace Maze
{
    public class Character
    {
        // позиция главного персонажа
        public ushort PosX { get; set; }
        public ushort PosY { get; set; }
        public LevelForm Parent { get; set; }

        public Character(LevelForm parent)
        {
            PosX = 0;
            PosY = 2;
            Parent = parent;
        }

        public void Clear()
        {
            Parent.Controls["pic" + PosY + "_" + PosX].BackgroundImage =
                Parent.maze.cells[PosY, PosX].Texture =
                Cell.Images[(int)(Parent.maze.cells[PosY, PosX].Type = CellType.HALL)];
        }

        public void MoveRight()
        {
            PosX++;
        }

        public void MoveLeft()
        {
            PosX--;
        }

        public void MoveUp()
        {
            PosY--;
        }

        public void MoveDown()
        {
            PosY++;
        }

        public void Show()
        {
            Parent.Step();
            if (PosX == Configuration.Columns - 1 && PosY == Configuration.Rows - 3)
            {
                Parent.GameOver("YOU WIN!!! - exit has been found");
                return;
            }
            if (Parent.Controls["pic" + PosY + "_" + PosX].BackgroundImage == Cell.Images[(int)CellType.MEDAL])
            {
                if (Parent.Medal())
                    return;
            }
            if (Parent.Controls["pic" + PosY + "_" + PosX].BackgroundImage == Cell.Images[(int)CellType.ENEMY]) 
            {
                if (Parent.Damag())
                    return;
            }
            Parent.Controls["pic" + PosY + "_" + PosX].BackgroundImage =
                Parent.maze.cells[PosY, PosX].Texture =
                Cell.Images[(int)(Parent.maze.cells[PosY, PosX].Type = CellType.HERO)];
        }
    }
}