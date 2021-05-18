using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TopToDown_Shooter
{
    public class Enemy : ICreature
    {
        public void Paint(PaintEventArgs e, Point location)
            => e.Graphics.DrawImage(new Bitmap(Properties.Resources.monster, new Size(64, 64)), location);
        public int X { get; set; }
        public int Y { get; set; }

        internal void Move(Player player, Map map)
        {
            var step = FindNextStep(map, new Point(X, Y), new Point(player.X, player.Y));
            var point1 = step.First();
            var point = point1.First();
            map._Map[X, Y] = new Tile(new Empty(), new Point(X, Y));
            map._Map[point.X, point.Y] = new Tile(new Enemy(), new Point(point.X, point.Y));
        }

        private static IEnumerable<SinglyLinkedList<Point>> FindNextStep(Map map, Point start, Point chest)
        {
            var queue = new Queue<Point>();
            var visited = new HashSet<Point>();
            var routes = new Dictionary<Point, SinglyLinkedList<Point>>();
            visited.Add(start);
            queue.Enqueue(start);
            routes.Add(start, new SinglyLinkedList<Point>(start));
            var allRoutes = FindEveryRoute(queue, visited, routes, map);
            if (allRoutes.ContainsKey(chest))
            {
                routes[chest].Reverse();
                yield return routes[chest];
            }
        }

        private static Dictionary<Point, SinglyLinkedList<Point>> FindEveryRoute(Queue<Point> queue,
                                                                                 HashSet<Point> visited,
                                                                                 Dictionary<Point, SinglyLinkedList<Point>> routes,
                                                                                 Map level)
        {
            while (queue.Count != 0)
            {
                var point = queue.Dequeue();
                if (!level.Contains(point.X, point.Y)
                  || level._Map[point.X, point.Y].Creature is Wall)
                    continue;
                foreach (var direction in Walker.PossibleDirections)
                {
                    var nextPoint = new Point(point.X + direction.Width, point.Y + direction.Height);
                    if (visited.Contains(nextPoint)) continue;
                    queue.Enqueue(nextPoint);
                    visited.Add(nextPoint);
                    routes.Add(nextPoint, new SinglyLinkedList<Point>(nextPoint, routes[point]));
                }
            }
            return routes;
        }
    }
}
