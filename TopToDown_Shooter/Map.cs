using System.Drawing;

namespace TopToDown_Shooter
{
    public class Map
    {
        public Tile[,] Tiles { get; set; }
        public Player Player { get; set; }

        public Map(string[] stringMap)
        {
            var height = stringMap.Length;
            var width = stringMap[0].Length;
            Tiles = new Tile[width, height];
            for (var y = 0; y < height; y++)
                for (var x = 0; x < width; x++)
                {
                    var c = stringMap[y][x];
                    switch (c)
                    {
                        case '#':
                            Tiles[x, y] = new Tile(new Wall() { X = x, Y = y }, new Point(x, y)); // стена
                            break;
                        case 'P':
                            Player = new Player() { X = x, Y = y };
                            Tiles[x, y] = new Tile(Player, new Point(x, y)); // игрок
                            break;
                        case 'M':
                            var enemy = new Tile(new Enemy(x, y), new Point(x, y));
                            Tiles[x, y] = enemy;
                            Window.Enemies.Add((Enemy)enemy.Creature);// монстр
                            break;
                        case 'B':
                            Tiles[x, y] = new Tile(new Spikes(x, y), new Point(x, y)); //пуля
                            break;
                        default:
                            Tiles[x, y] = new Tile(new Empty() { X = x, Y = y }, new Point(x, y)); // пусто
                            break;
                    }
                }
        }

        public bool Contains(int x, int y) => x >= 0 && x < Tiles.GetLength(0) && y >= 0 && y < Tiles.GetLength(1);
    }
}
