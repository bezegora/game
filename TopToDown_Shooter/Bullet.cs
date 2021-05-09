using System.Drawing;
using System.Windows.Forms;

namespace TopToDown_Shooter
{
    public class Bullet : ICreature
    {
        public Direction Direction { get; private set; }
        public static int Velocity { get; } = 2;
        public int X { get; set; }
        public int Y { get; set; }
        public Bullet(Direction dir, int x, int y)
        {
            Direction = dir;
            X = x;
            Y = y;
        }

        public void Paint(PaintEventArgs e, Point location)
            => e.Graphics.DrawImage(new Bitmap(Properties.Resources.bullet, new Size(64, 64)), location);
    }
}