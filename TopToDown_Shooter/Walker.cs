using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TopToDown_Shooter
{
    public class Walker
    {
        private static readonly Dictionary<Direction, Size> directionToOffset = new Dictionary<Direction, Size>
        {
            {Direction.Up, new Size(0, -1)},
            {Direction.Down, new Size(0, 1)},
            {Direction.Left, new Size(-1, 0)},
            {Direction.Right, new Size(1, 0)}
        };

        private static readonly Dictionary<Size, Direction> offsetToDirection = new Dictionary<Size, Direction>
        {
            {new Size(0, -1), Direction.Up},
            {new Size(0, 1), Direction.Down},
            {new Size(-1, 0), Direction.Left},
            {new Size(1, 0), Direction.Right}
        };

        public static readonly IReadOnlyList<Size> PossibleDirections = offsetToDirection.Keys.ToList();


        public Point Position { get; }
        public Point? PointOfCollision { get; }

        public Walker(Point position)
        {
            Position = position;
            PointOfCollision = null;
        }

        private Walker(Point position, Point pointOfCollision)
        {
            Position = position;
            PointOfCollision = pointOfCollision;
        }

        public Walker WalkInDirection(Map map, Direction direction)
        {
            var newPoint = Position + directionToOffset[direction];
            if (!map.Contains(newPoint.X, newPoint.Y))
                return new Walker(Position, Position);
            return map._Map[newPoint.X, newPoint.Y].Creature is Wall ? new Walker(newPoint, newPoint) : new Walker(newPoint);
        }

        public static Direction ConvertOffsetToDirection(Size offset) => offsetToDirection[offset];
    }
}