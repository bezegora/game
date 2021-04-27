using System.Windows.Forms;

namespace TopToDown_Shooter
{
    public interface ICreature
    {
        void Move(KeyEventArgs e, Map level, int dx, int dy);
        void Paint(PaintEventArgs e);
    }
}