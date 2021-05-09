using System.Drawing;
using System.Windows.Forms;

namespace TopToDown_Shooter
{
    class Wall : ICreature
    {
        public void Paint(PaintEventArgs e, Point location)
            => e.Graphics.DrawImage(new Bitmap(Properties.Resources.wall, new Size(64, 64)), location);
        public int X { get; set; }
        public int Y { get; set; }
    }
}
