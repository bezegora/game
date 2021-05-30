using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TopToDown_Shooter
{
    public class Spikes : IEntity
    {
        public Direction Direction { get; private set; }
        public static int Velocity { get; } = 2;
        public static List<Spikes> AllBullets { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Spikes(Direction dir, int x, int y)
        {
            Direction = dir;
            X = x;
            Y = y;
            //AllBullets.Add(this);
        }

        public void Paint(PaintEventArgs e, Point location)
            => e.Graphics.DrawImage(new Bitmap(Properties.Resources.spikes, new Size(64, 64)), location);

        internal static void Move()
        {
            foreach (var bullet in AllBullets)
            {
                var x = bullet.Direction is Direction.Right ? 1 : bullet.Direction is Direction.Left ? -1 : 0;
                var y = bullet.Direction is Direction.Down ? 1 : bullet.Direction is Direction.Up ? -1 : 0;
                 //= new Bullet(bullet.Direction, bullet.X + x, bullet.Y + y);
            }
        }
    }
}