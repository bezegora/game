using System.Drawing;
using System.Windows.Forms;

namespace TopToDown_Shooter
{
    public class Player : IEntity
    {
        public int X { get; set; }
        public int Y { get; set; }

        public void Paint(PaintEventArgs e, Point location) 
            => e.Graphics.DrawImage(new Bitmap(Properties.Resources.player, new Size(64, 64)), location);
    }
}
