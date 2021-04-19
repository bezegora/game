using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TopToDown_Shooter
{

    class Tile
    {
        Size size = new Size(64, 64);

        Creature Creature { get; set; }

        Point Location { get; set; }

        public void Paint(PaintEventArgs e)
        {
            if(Creature is Creature.Wall) e.Graphics.DrawImage(new Bitmap(Properties.Resources.wall, size), Location);
            if (Creature is Creature.Player) e.Graphics.DrawImage(new Bitmap(Properties.Resources.player, size), Location);
            if (Creature is Creature.Empty) e.Graphics.DrawImage(new Bitmap(Properties.Resources.tile, size), Location);
        }

        public Tile(Creature creature, Point location)
        {
            Location = new Point(location.X * size.Width, location.Y * size.Height);
            Creature = creature;
        }
    }
}
