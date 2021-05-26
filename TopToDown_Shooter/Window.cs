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
        public readonly Map Level;

        Player Player;
        public static List<Enemy> Enemies = new List<Enemy> { };

        public Window()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint, true);
            UpdateStyles();
            var timer = new Timer() { Interval = 100 };
            timer.Tick += (sender, e) => Invalidate();
            Paint += new PaintEventHandler(OnPaint);
            var stringMap = new[]{
                 "     #",
                 " P   #",
                 "##   #",
                 " M    ",
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
                case Keys.Up:
                    ShootBullet(Direction.Up);
                    break;
                case Keys.Down:
                    ShootBullet(Direction.Down);
                    break;
                case Keys.Right:
                    ShootBullet(Direction.Right);
                    break;
                case Keys.Left:
                    ShootBullet(Direction.Left);
                    break;
            }
            var i = Enemies.Count - 1;
            while (Enemies.Count > 0 && i >= 0)
            {
                var enemy = Enemies[i--];
                enemy.Move(Player, Level);
                Invalidate();
            }
            //Bullet.Move();
        }

        private void ShootBullet(Direction dir)
        {
            var x = dir is Direction.Right ? 1 : dir is Direction.Left ? -1 : 0;
            var y = dir is Direction.Down ? 1 : dir is Direction.Up ? -1 : 0;
            var bul = new Bullet(dir, Player.X + x, Player.Y + y);
            if (Level.Contains(bul.X, bul.Y) && !(Level._Map[bul.X, bul.Y].Creature is Wall))
            {
                Level._Map[bul.X, bul.Y] = new Tile(bul, new Point(bul.X, bul.Y));
                Invalidate();
            }
        }

        private void MovePlayer(Direction dir)
        {
            var x = dir is Direction.Right ? 1 : dir is Direction.Left ? -1 : 0;
            var y = dir is Direction.Down ? 1 : dir is Direction.Up ? -1 : 0;

            if (Level.Contains(Player.X + x, Player.Y + y) && (Level._Map[Player.X + x, Player.Y + y].Creature is Empty))
            {
                Level._Map[Player.X, Player.Y] = new Tile(new Empty(), Location = new Point(Player.X, Player.Y));
                Level._Map[Player.X + x, Player.Y + y] = new Tile(Player, Location = new Point(Player.X + x, Player.Y + y));
                Player = new Player { X = Player.X + x, Y = Player.Y + y };
                Invalidate();
            }
        }

        private void Form1_Load(object sender, EventArgs e) => Invalidate();

        private void OnPaint(object sender, PaintEventArgs e)
        {
            // логика рисования (тупа что когда отрисовывается)
            foreach (var tile in Level._Map)
                tile.Paint(e);
        }
    }
}
