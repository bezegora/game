using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TopToDown_Shooter
{
    public partial class Form1 : Form
    {
        readonly Map level;

        //Player Я;
        //List<Enemy>

        public Form1()
        {
            InitializeComponent();
            var timer = new Timer() { Interval = 100 };
            timer.Tick += (sender, e) =>
            {
                Invalidate();
            };
            Paint += new PaintEventHandler(OnPaint);
            var stringMap = new[]{
                 "  #",
                 " P#",
                 "###"
            };

            level = new Map(stringMap);
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            // логика рисования (тупа что когда отрисовывается)
            foreach (var tile in level._Map)
                tile.Paint(e);
        }
    }
}
