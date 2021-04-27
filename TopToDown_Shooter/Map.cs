using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopToDown_Shooter
{
    public class Map
    {
        public Tile[,] _Map { get; set; }
        public int[] Player { get; set; }

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
                            _Map[x, y] = new Tile(Creature.Wall, new Point(x, y)); // стена
                            break;
                        case 'P':
                            _Map[x, y] = new Tile(Creature.Player, new Point(x, y)); // игрок
                            Player = new int[2] { x, y };
                            break;
                        default:
                            _Map[x, y] = new Tile(Creature.Empty, new Point(x, y)); // пусто
                            break;
                    }
                }
        }

        public bool Contains(int x, int y) => x >= 0 && x < _Map.GetLength(0) && y >= 0 && y < _Map.GetLength(1);
    }
}
