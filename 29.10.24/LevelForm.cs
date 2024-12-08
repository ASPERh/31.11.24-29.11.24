using System.Windows.Forms;

namespace Maze
{
    public partial class LevelForm : Form
    {
        public Maze maze; // ������ �� ������ ����� ������������� � ���������
        public Character Hero;
        private int steps = 0;
        private int medals = 0;
        private int health = 100;
        private int medalsTolal = 0;
        public LevelForm()
        {
            InitializeComponent();
            FormSettings();
            StartGameProcess();
        }

        public void FormSettings()
        {
            Text = Configuration.Title;
            BackColor = Configuration.Background;

            // ������� ���������� ������� �����
            ClientSize = new Size(
                Configuration.Columns * Configuration.PictureSide , 
                Configuration.Rows * Configuration.PictureSide + statusStrip1.Height);

            StartPosition = FormStartPosition.CenterScreen;
        }

        public void StartGameProcess()
        {
            Hero = new Character(this);
            maze = new Maze(this);
            maze.Generate();
            maze.Show();
        }
        public void Step()
        {
            steps++;
            toolStripStatusLabel2.Text = "Steps-"+ steps.ToString();
        }
        public bool Medal()
        {
            medals++;
            toolStripStatusLabel3.Text = "Medals-" + medals.ToString();
            if (medals == medalsTolal)
            {
                GameOver("YOU WIN!!! - all medals found");
                return true;
            }
            return false;
        }
        public void MedalTolal()
        {
            medalsTolal++;

        }
        public bool Damag()
        {
            health-=Configuration.Damag;
            toolStripStatusLabel1.Text = "Health-" + health.ToString() +"%";
            if (health <=0)
            {
                GameOver("YOU Died");
                return true;
            }
            return false;
        }
        public void GameOver(string message)
        {
            MessageBox.Show(message);
            this.Close();
        }

        private void KeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                // �������� �� ��, �������� �� ������ ������
                if (maze.cells[Hero.PosY, Hero.PosX + 1].Type != CellType.WALL)
                {
                    Hero.Clear();
                    Hero.MoveRight();
                    Hero.Show();
                }
            }
            else if (e.KeyCode == Keys.Left && Hero.PosX != 0)
            {
                // �������� �� ��, �������� �� ������ �����
                if (maze.cells[Hero.PosY, Hero.PosX - 1].Type != CellType.WALL)
                {
                    Hero.Clear();
                    Hero.MoveLeft();
                    Hero.Show();
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                // �������� �� ��, �������� �� ������ ����
                if (maze.cells[Hero.PosY - 1, Hero.PosX].Type != CellType.WALL)
                {
                    Hero.Clear();
                    Hero.MoveUp();
                    Hero.Show();
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                // �������� �� ��, �������� �� ������ ����
                if (maze.cells[Hero.PosY + 1, Hero.PosX].Type != CellType.WALL)
                {
                    Hero.Clear();
                    Hero.MoveDown();
                    Hero.Show();
                }
            }
        }
    }
}