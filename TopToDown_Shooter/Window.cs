using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TopToDown_Shooter
{
    public partial class Window : Form
    {
        public Map Level;
        public static bool isGameOver;
        readonly Player Player;
        public static List<Enemy> Enemies = new List<Enemy>();
        public static int Score = 0;
        private static int CoolDown = 3;
        private static int SpawnTime = 5;
        private static new int Height;

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
                 "                 # ",
                 " P                #",
                 "##                #",
                 " M#           #    ",
                 "  #           # M  ",
                 "              #    ",
                 "              #    "
            };
            KeyDown += new KeyEventHandler(DoTurn);
            Level = new Map(stringMap);
            Player = new Player { X = Level.Player.X, Y = Level.Player.Y };
            Size = new Size(stringMap[0].Length * 64 + 16, stringMap.Length * 64 + 38 + 32);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Height = Level.Tiles.GetLength(1) * 64;
        }

        private void DoTurn(object sender, KeyEventArgs e)
        {
            if (!isGameOver)
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
                if (CoolDown == 3)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Up:
                            PlaceSpikes(Direction.Up);
                            CoolDown = 0;
                            break;
                        case Keys.Down:
                            PlaceSpikes(Direction.Down);
                            CoolDown = 0;
                            break;
                        case Keys.Right:
                            PlaceSpikes(Direction.Right);
                            CoolDown = 0;
                            break;
                        case Keys.Left:
                            PlaceSpikes(Direction.Left);
                            CoolDown = 0;
                            break;
                    }
                }
                if (CoolDown != 3)
                    CoolDown++;
                var i = Enemies.Count - 1;
                while (Enemies.Count > 0 && i >= 0)
                {
                    var enemy = Enemies[i--];
                    enemy.Move(Player, Level);
                    Invalidate();
                }
                if (SpawnTime == 0)
                {
                    Enemy.SpawnEnemy(Level, Player);
                    SpawnTime = 5;
                    Invalidate();
                }
                else SpawnTime--;
            }
        }

        private void PlaceSpikes(Direction dir)
        {
            var x = dir is Direction.Right ? 1 : dir is Direction.Left ? -1 : 0;
            var y = dir is Direction.Down ? 1 : dir is Direction.Up ? -1 : 0;
            var bul = new Spikes(dir, Player.X + x, Player.Y + y);
            if (Level.Contains(bul.X, bul.Y) && !(Level.Tiles[bul.X, bul.Y].Creature is Wall))
            {
                Level.Tiles[bul.X, bul.Y] = new Tile(bul, new Point(bul.X, bul.Y));
                Invalidate();
            }
        }

        private void MovePlayer(Direction dir)
        {
            var x = dir is Direction.Right ? 1 : dir is Direction.Left ? -1 : 0;
            var y = dir is Direction.Down ? 1 : dir is Direction.Up ? -1 : 0;

            if (Level.Contains(Player.X + x, Player.Y + y) && (Level.Tiles[Player.X + x, Player.Y + y].Creature is Empty))
            {
                Level.Tiles[Player.X, Player.Y] = new Tile(new Empty(), Location = new Point(Player.X, Player.Y));
                Level.Tiles[Player.X + x, Player.Y + y] = new Tile(Player, Location = new Point(Player.X + x, Player.Y + y));
                Player.X += x;
                Player.Y += y;
                Invalidate();
            }
            if (Enemies.Where(enemy => enemy.X == Player.X && enemy.Y == Player.Y).Any())
            {
                isGameOver = true;
                Invalidate();
            }
        }

        private void OnLoad(object sender, EventArgs e) => Invalidate();

        private void OnPaint(object sender, PaintEventArgs e)
        {
            // логика рисования (тупа что когда отрисовывается)
            foreach (var tile in Level.Tiles)
                tile.Paint(e);
            DrawLowerTab(e, Level);
            if (isGameOver)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, 320, 64));
                e.Graphics.DrawString("GameOver", new Font(FontFamily.GenericMonospace, 48, FontStyle.Bold), new SolidBrush(Color.Red), -10, 0);
            }
        }

        private static void DrawLowerTab(PaintEventArgs e, Map level)
        {
            e.Graphics.DrawLine(new Pen(new SolidBrush(Color.Black), 5), 0, Height, level.Tiles.GetLength(0)*64, Height);
            e.Graphics.DrawString($"Score: {Score}", new Font(FontFamily.GenericMonospace, 24, FontStyle.Bold), new SolidBrush(Color.Black), 0, Height);
            e.Graphics.DrawString("Spikes:", new Font(FontFamily.GenericMonospace, 24, FontStyle.Bold), new SolidBrush(Color.Black), 192, Height);
            e.Graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(352, Height, 60 * CoolDown, 38));
            e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black), 5), new Rectangle(352, Height, 180, 38));
            e.Graphics.DrawString($"Moves until next enemy appears {SpawnTime}", new Font(FontFamily.GenericMonospace, 24), new SolidBrush(SpawnTime <= 2 ? Color.Red : Color.Black), 352 + 180 + 32, Height);
        }
    }
}
