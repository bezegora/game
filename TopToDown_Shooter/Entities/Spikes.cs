using System.Drawing;
using System.Windows.Forms;

namespace TopToDown_Shooter
{
    public class Spikes : IEntity
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Spikes(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static void PlaceSpikes(Direction dir, Player player, Map level)
        {
            var x = dir is Direction.Right ? 1 : dir is Direction.Left ? -1 : 0;
            var y = dir is Direction.Down ? 1 : dir is Direction.Up ? -1 : 0;
            var bul = new Spikes(player.X + x, player.Y + y);
            if (level.Contains(bul.X, bul.Y) && !(level.Tiles[bul.X, bul.Y].Creature is Wall))
                level.Tiles[bul.X, bul.Y] = new Tile(bul, new Point(bul.X, bul.Y));
        }


        public void Paint(PaintEventArgs e, Point location)
            => e.Graphics.DrawImage(new Bitmap(Properties.Resources.spikes, new Size(64, 64)), location);
    }
}