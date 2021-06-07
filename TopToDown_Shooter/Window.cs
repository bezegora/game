using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static TopToDown_Shooter.Spikes;
using static TopToDown_Shooter.Enemy;
using static TopToDown_Shooter.Player;

namespace TopToDown_Shooter
{
    public partial class Window : Form
    {
        public Map Level;
        public static bool isGameOver;
        readonly Player Player;
        public static List<Enemy> Enemies = new List<Enemy>();
        public static int Score;
        private static int CoolDown = 3;
        private static int SpawnTime = 4;
        private static new int Height;
        public int HighScore = 0;
        private static readonly List<Keys> ShootKeys = new List<Keys> { Keys.Up, Keys.Down, Keys.Right, Keys.Left };
        private static readonly List<Keys> MoveKeys = new List<Keys> { Keys.A, Keys.W, Keys.S, Keys.D };

        public Window()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint, true);
            UpdateStyles();
            Paint += new PaintEventHandler(OnPaint);
            var stringMap = new[]{
                 "                   # ",
                 " P      #           #",
                 "##  #   ### ##      #",
                 "  # #   #       # M  ",
                 " M#             ##   ",
                 "     #  #  ##   #    ",
                 "     #  #       #    "
            };
            KeyDown += new KeyEventHandler(DoTurn);
            Level = new Map(stringMap);
            Player = new Player { X = Level.Player.X, Y = Level.Player.Y };
            Size = new Size(stringMap[0].Length * 64 + 16, stringMap.Length * 64 + 38 + 32);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Height = Level.Tiles.GetLength(1) * 64;
            isGameOver = false;
            Score = 0;
        }

        private void DoTurn(object sender, KeyEventArgs e)
        {
            if (!isGameOver && ((ShootKeys.Contains(e.KeyCode) && CoolDown == 3) || MoveKeys.Contains(e.KeyCode)))
            {
                switch (e.KeyCode)
                {
                    case Keys.A:
                        MovePlayer(Direction.Left, Level, Player);
                        break;
                    case Keys.D:
                        MovePlayer(Direction.Right, Level, Player);
                        break;
                    case Keys.S:
                        MovePlayer(Direction.Down, Level, Player);
                        break;
                    case Keys.W:
                        MovePlayer(Direction.Up, Level, Player);
                        break;
                }
                if (CoolDown == 3)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Up:
                            PlaceSpikes(Direction.Up, Player, Level);
                            CoolDown = 0;
                            break;
                        case Keys.Down:
                            PlaceSpikes(Direction.Down, Player, Level);
                            CoolDown = 0;
                            break;
                        case Keys.Right:
                            PlaceSpikes(Direction.Right, Player, Level);
                            CoolDown = 0;
                            break;
                        case Keys.Left:
                            PlaceSpikes(Direction.Left, Player, Level);
                            CoolDown = 0;
                            break;
                    }
                }
                if (CoolDown != 3)
                    CoolDown++;
                var i = Enemies.Count - 1;
                while (Enemies.Count > 0 && i >= 0)
                    Enemies[i--].Move(Player, Level);
                if (SpawnTime == 0)
                {
                    SpawnEnemy(Level, Player);
                    SpawnTime = 4;
                }
                else SpawnTime--;
                Invalidate();
            }
            if (isGameOver && Score > HighScore)
                HighScore = Score;
            if (isGameOver && e.KeyCode == Keys.R)
            {
                Hide();
                Enemies.Clear();
                var a = new Window() { HighScore = HighScore };
                a.ShowDialog();
            }
            if (e.KeyCode == Keys.Escape)
                Application.Exit();
        }

        private void OnLoad(object sender, EventArgs e) => Invalidate();

        private void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Properties.Resources.paper, new PointF(0, 0));
            foreach (var tile in Level.Tiles)
                tile.Paint(e);
            DrawLowerTab(e, Level);
            if (isGameOver)
                DrawEndScrean(e);
        }

        private void DrawEndScrean(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.White),
                                                     new Rectangle(0, 0, 320, 128));
            e.Graphics.DrawString("GameOver",
                                  new Font(FontFamily.GenericMonospace, 48, FontStyle.Bold),
                                  new SolidBrush(Color.Red),
                                  -10,
                                  0);
            e.Graphics.DrawString($"Highest Score: {HighScore}",
                                  new Font(FontFamily.GenericMonospace, 24, FontStyle.Bold),
                                  new SolidBrush(Color.Black),
                                  0,
                                  64);
        }

        private static void DrawLowerTab(PaintEventArgs e, Map level)
        {
            e.Graphics.DrawLine(new Pen(new SolidBrush(Color.Black), 5),
                                0,
                                Height,
                                level.Tiles.GetLength(0) * 64,
                                Height);
            e.Graphics.DrawString($"Score: {Score}",
                                  new Font(FontFamily.GenericMonospace, 24, FontStyle.Bold),
                                  new SolidBrush(Color.Black),
                                  0,
                                  Height);
            e.Graphics.DrawString("Spikes:",
                                  new Font(FontFamily.GenericMonospace, 24, FontStyle.Bold),
                                  new SolidBrush(Color.Black),
                                  192,
                                  Height);
            e.Graphics.FillRectangle(new SolidBrush(Color.Green),
                                     new Rectangle(352, Height, 60 * CoolDown, 38));
            e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black), 5),
                                     new Rectangle(352, Height, 180, 38));
            e.Graphics.DrawString($"Moves until next enemy appears {SpawnTime}",
                                  new Font(FontFamily.GenericMonospace, 24, SpawnTime <= 2 ? FontStyle.Bold : FontStyle.Regular),
                                  new SolidBrush(SpawnTime <= 2 ? Color.Red : Color.Black),
                                  352 + 180 + 32,
                                  Height);
        }
    }
}
