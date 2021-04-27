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
                 "      ",
                 "      ",
                 "      "
            };
            KeyDown += new KeyEventHandler(DoTurn);
            Level = new Map(stringMap);
            Player = new Player { X = Level.Player[0], Y = Level.Player[1] };
        }

        private void DoTurn(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "A":
                    MovePlayer(new[] { -1, 0 });
                    break;
                case "D":
                    MovePlayer(new[] { 1, 0 });
                    break;
                case "S":
                    MovePlayer(new[] { 0, 1 });
                    break;
                case "W":
                    MovePlayer(new[] { 0, -1 });
                    break;
            }
        }

        private void MovePlayer(int[] xy)
        {
            if (Level.Contains(Player.X + xy[0], Player.Y + xy[1]) && Level._Map[Player.X + xy[0], Player.Y + xy[1]].Creature is Creature.Empty)
            {
                Level._Map[Player.X, Player.Y] = new Tile(Creature.Empty, Location = new Point(Player.X, Player.X));
                Level._Map[Player.X + xy[0], Player.Y + xy[1]] = new Tile(Creature.Player, Location = new Point(Player.X + xy[0], Player.Y + xy[1]));
                Player = new Player { X = Player.X + xy[0], Y = Player.Y + xy[1] };
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
