using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TopToDown_Shooter
{
    public partial class Window : Form
    {
        readonly Map Level;

        Player Player;
        //List<Enemy>

        public Window()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint, true);
            UpdateStyles();
            var timer = new Timer() { Interval = 100 };
            timer.Tick += (sender, e) =>
            {
                Invalidate();
            };
            Paint += new PaintEventHandler(OnPaint);
            var stringMap = new[]{
                 "     #",
                 " P   #",
                 "##   #",
                 " MB   ",
                 "      ",
                 "      "
            };
            KeyDown += new KeyEventHandler(DoTurn);
            Level = new Map(stringMap);
            Player = new Player { X = Level.Player.X, Y = Level.Player.Y };
        }

        private void DoTurn(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    MovePlayer(Direction.Left);
                    break;
                case Keys.D:
                    MovePlayer(Direction.Right);
                    break;
                case Keys.S:
                    MovePlayer(Direction.Down);
                    break;
                case Keys.W:
                    MovePlayer(Direction.Up);
                    break;
            }
        }

        private void MovePlayer(Direction dir)
        {
            var x = dir is Direction.Right ? 1 : dir is Direction.Left ? -1 : 0;
            var y = dir is Direction.Down ? 1 : dir is Direction.Up ? -1 : 0;

            if (Level.Contains(Player.X + x, Player.Y + y) && Level._Map[Player.X + x, Player.Y + y].Creature is Wall)
            {
                Level._Map[Player.X, Player.Y] = new Tile(new Empty(), Location = new Point(Player.X, Player.Y));
                Level._Map[Player.X + x, Player.Y + y] = new Tile(Player, Location = new Point(Player.X + x, Player.Y + y));
                Player = new Player { X = Player.X + x, Y = Player.Y + y };
                Invalidate();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            // логика рисования (тупа что когда отрисовывается)
            foreach (var tile in Level._Map)
                tile.Paint(e);
        }
    }
}
