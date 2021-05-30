using System.Drawing;
using System.Windows.Forms;

namespace TopToDown_Shooter
{
    public interface IEntity
    {
        void Paint(PaintEventArgs e, Point location);
        int X { get; set; }
        int Y { get; set; }
    }
}