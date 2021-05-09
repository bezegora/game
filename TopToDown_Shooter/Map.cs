using System.Drawing;

namespace TopToDown_Shooter
{
    public class Map
    {
        public Tile[,] _Map { get; set; }
        public Player Player { get; set; }

        public Map(string[] stringMap)
        {
            var height = stringMap.Length;
            var width = stringMap[0].Length;
            _Map = new Tile[width, height];
            for (var y = 0; y < width; y++)
                for (var x = 0; x < height; x++)
                {
                    var c = stringMap[y][x];
                    switch (c)
                    {
                        case '#':
                            _Map[x, y] = new Tile(new Wall() { X = x, Y = y }, new Point(x, y)); // стена
                            break;
                        case 'P':
                            Player = new Player() { X = x, Y = y };
                            _Map[x, y] = new Tile(Player, new Point(x, y)); // игрок
                            break;
                        case 'M':
                            _Map[x, y] = new Tile(new Enemy(), new Point(x, y)); // монстр
                            break;
                        case 'B':
                            _Map[x, y] = new Tile(new Bullet(Direction.Down, x, y), new Point(x, y)); //пуля
                            break;
                        default:
                            _Map[x, y] = new Tile(new Empty() { X = x, Y = y }, new Point(x, y)); // пусто
                            break;
                    }
                }
        }

        public bool Contains(int x, int y) => x >= 0 && x < _Map.GetLength(0) && y >= 0 && y < _Map.GetLength(1);
    }
}
