using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopToDown_Shooter
{
    class Map
    {
        public Tile[,] _Map { get; set; }

        public Map(string[] stringMap)
        {
            var height = stringMap.Length;
            var width = stringMap[0].Length;
            _Map = new Tile[width, height];
            for (var y = 0; y < width; y++)
                for (var x = 0; x < height; x++)
                {
                    var c = stringMap[x][y];
                    switch (c)
                    {
                        case '#':
                            _Map[x, y] = new Tile(Creature.Wall, new Point(x, y)); // стена
                            break;
                        case 'P':
                            _Map[x, y] = new Tile(Creature.Player, new Point(x, y)); // игрок
                            break;
                        case ' ':
                            _Map[x, y] = new Tile(Creature.Empty, new Point(x, y)); // пусто
                            break;
                    }
                }
        }
    }
}
