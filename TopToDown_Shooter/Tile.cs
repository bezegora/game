using System.Drawing;
using System.Windows.Forms;

namespace TopToDown_Shooter
{

    public class Tile
    {
        static Size size = new Size(64, 64);

        public IEntity Creature { get; set; }

        public Point Location { get; set; }

        public void Paint(PaintEventArgs e) => Creature.Paint(e, Location); 
        /*e.Graphics.DrawImage(new Bitmap(Properties.Resources.tile, size), Location)*/

        public Tile(IEntity creature, Point location)
        {
            Location = new Point(location.X * size.Width, location.Y * size.Height);
            Creature = creature;
        }
    }
}
