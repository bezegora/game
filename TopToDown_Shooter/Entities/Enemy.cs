using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TopToDown_Shooter
{
    public class Enemy : IEntity
    {
        public void Paint(PaintEventArgs e, Point location)
            => e.Graphics.DrawImage(new Bitmap(Image, new Size(64, 64)), location);
        public int X { get; set; }
        public int Y { get; set; }
        private Image Image { get; set; }

        public Enemy(int x, int y)
        {
            X = x;
            Y = y;
            var rand = new Random().Next(1, 4);
            switch (rand)
            {
                case 1:
                    Image = Properties.Resources.monster1;
                    break;
                case 2:
                    Image = Properties.Resources.monster2;
                    break;
                case 3:
                    Image = Properties.Resources.monster3;
                    break;
            }
        }

        internal void Move(Player player, Map map)
        {
            var step = FindNextStep(map, new Point(X, Y), new Point(player.X, player.Y));
            while (step.Length > 2)
                step = step.Previous;
            var point = step.Value;
            if (map.Tiles[point.X, point.Y].Creature is Spikes)
            {
                map.Tiles[X, Y] = new Tile(new Empty(), new Point(X, Y));
                map.Tiles[point.X, point.Y] = new Tile(new Empty(), new Point(point.X, point.Y));
                Window.Enemies.Remove(this);
                Window.Score++;
            }
            else if (!(map.Tiles[point.X, point.Y].Creature is Enemy))
            {
                map.Tiles[X, Y] = new Tile(new Empty(), new Point(X, Y));
                map.Tiles[point.X, point.Y] = new Tile(this, new Point(point.X, point.Y));
                X = point.X;
                Y = point.Y;
            }
            if (X == player.X && Y == player.Y)
                Window.isGameOver = true;
        }

        public static void SpawnEnemy(Map map, Player player)
        {
            var rand = new Random();
            var x = rand.Next(0, map.Tiles.GetLength(0));
            var y = rand.Next(0, map.Tiles.GetLength(1));
            while (Math.Abs(player.X - x) <= 2 && Math.Abs(player.Y - y) <= 2 || map.Tiles[x, y].Creature is Wall || map.Tiles[x, y].Creature is Spikes)
            {
                x = rand.Next(0, map.Tiles.GetLength(0));
                y = rand.Next(0, map.Tiles.GetLength(1));
            }
            var enemy = new Enemy(x, y);
            map.Tiles[x, y] = new Tile(enemy, new Point(x, y));
            Window.Enemies.Add(enemy);
        }

        private static SinglyLinkedList<Point> FindNextStep(Map map, Point start, Point finish)
        {
            var queue = new Queue<Point>();
            var visited = new HashSet<Point>();
            var routes = new Dictionary<Point, SinglyLinkedList<Point>>();
            visited.Add(start);
            queue.Enqueue(start);
            routes.Add(start, new SinglyLinkedList<Point>(start));
            var allRoutes = FindEveryRoute(queue, visited, routes, map);
            if (allRoutes.ContainsKey(finish))
            {
                routes[finish].Reverse();
                return routes[finish];
            }
            return new SinglyLinkedList<Point>(new Point());
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
                  || level.Tiles[point.X, point.Y].Creature is Wall)
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
