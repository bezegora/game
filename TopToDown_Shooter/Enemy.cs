﻿using System.Drawing;
using System.Windows.Forms;

namespace TopToDown_Shooter
{
    public class Enemy : ICreature
    {
        public void Paint(PaintEventArgs e, Point location)
            => e.Graphics.DrawImage(new Bitmap(Properties.Resources.monster, new Size(64, 64)), location);
        public int X { get; set; }
        public int Y { get; set; }
    }
}
