using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TopToDown_Shooter
{
    public class Walker
    {
        private static readonly Dictionary<Size, Direction> offsetToDirection = new Dictionary<Size, Direction>
        {
            {new Size(0, -1), Direction.Up},
            {new Size(0, 1), Direction.Down},
            {new Size(-1, 0), Direction.Left},
            {new Size(1, 0), Direction.Right}
        };

        public static readonly IReadOnlyList<Size> PossibleDirections = offsetToDirection.Keys.ToList();
    }
}