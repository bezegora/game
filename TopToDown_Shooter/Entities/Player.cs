using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TopToDown_Shooter
{
    public class Player : IEntity
    {
        public int X { get; set; }
        public int Y { get; set; }

        public static void MovePlayer(Direction dir, Map level, Player player)
        {
            var x = dir is Direction.Right ? 1 : dir is Direction.Left ? -1 : 0;
            var y = dir is Direction.Down ? 1 : dir is Direction.Up ? -1 : 0;

            if (level.Contains(player.X + x, player.Y + y) && (level.Tiles[player.X + x, player.Y + y].Creature is Empty))
            {
                level.Tiles[player.X, player.Y] = new Tile(new Empty(), new Point(player.X, player.Y));
                level.Tiles[player.X + x, player.Y + y] = new Tile(player, new Point(player.X + x, player.Y + y));
                player.X += x;
                player.Y += y;
            }
            if (Window.Enemies.Where(enemy => enemy.X == player.X && enemy.Y == player.Y).Any())
                Window.isGameOver = true;
        }


        public void Paint(PaintEventArgs e, Point location)
            => e.Graphics.DrawImage(new Bitmap(Properties.Resources.player, new Size(64, 64)), location);
    }
}
