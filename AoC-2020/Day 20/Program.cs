using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_20
{
    class Tile
    {
        public string[] image;
        public int id;
        public string[] edges;
        public int size;
        public int rotation = 0;

        public Tile(string[] image, int id)
        {
            this.image = image;
            this.id = id;
            this.size = image.Length;
            CreateAllEdges();
        }

        private void CreateAllEdges()
        {
            this.edges = new[]
            {
                CreateEdge(0,0,1,0), // top
                CreateEdge(0,0,0,1), // left
                CreateEdge(0,size-1,0,1), // right
                CreateEdge(size-1,0,1,0), // bottom
                // Rotated and flipped
                CreateEdge(size-1,size-1,-1,0), // top
                CreateEdge(size-1,size-1,0,-1), // left
                CreateEdge(size-1,0,0,-1), // right
                CreateEdge(0,size-1,-1,0) // bottom
            };
        }

        public string GetTopRow() => edges[0];
        public string GetLeftRow() => edges[1];
        public string GetRightRow() => edges[2];
        public string GetBottomRow() => edges[3];



        /*xDirection == -1 for right to left, 0 for top to down, 1 for left to right, same but opposite for y
         * startIndexCol == 0 for normal, size-1 for rotated, same for y but for flipped*/
        private string CreateEdge(int startIndexRow, int startIndexCol, int xDirection, int yDirection)
        {
            string edge = "";
            for (int i = 0; i < size; i++)
            {
                edge += image[startIndexRow][startIndexCol];
                startIndexRow += yDirection;
                startIndexCol += xDirection;
            }
            return edge;
        }

    }
    class Program
    {
        static Dictionary<int, Tile> tiles;
        static Dictionary<int, Tile> tilesRemoved;
        static Tile[,] tileGrid = new Tile[12,12];
        static int tileSize;

        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt").ToList();
            GenerateTiles(input);
            FillCorners();
        }

        static void FillCorners()
        {
            long partOneAns = 1;
            foreach (var tile in tiles.Values)
            {
                var matchings = MatchingSides(tile);
                if (matchings.Item1 == 2)
                {
                    partOneAns *= tile.id;
                    if (matchings.Item2.Item3 != 0 && matchings.Item2.Item4 != 0) // top left
                    {
                       tileGrid[0, 0] = tile;
                       tileGrid[0, 1] = tiles[matchings.Item2.Item3];
                       tileGrid[1,0] = tiles[matchings.Item2.Item4];
                       tilesRemoved.Remove(tiles[matchings.Item2.Item3].id);
                       tilesRemoved.Remove(tiles[matchings.Item2.Item4].id);
                    }
                    if (matchings.Item2.Item2 != 0 && matchings.Item2.Item4 != 0) // top right
                    {
                       tileGrid[0, tileSize -1] = tile;
                        tileGrid[0, tileSize - 2] = tiles[matchings.Item2.Item2];
                        tileGrid[1, tileSize - 1] = tiles[matchings.Item2.Item4];
                        tilesRemoved.Remove(tiles[matchings.Item2.Item2].id);
                        tilesRemoved.Remove(tiles[matchings.Item2.Item4].id);
                    }
                    if (matchings.Item2.Item3 != 0 && matchings.Item2.Item1 != 0) // bottom left
                    { 
                        tileGrid[tileSize - 1, 0] = tile;
                        tileGrid[tileSize - 1, 1] = tiles[matchings.Item2.Item3];
                        tileGrid[tileSize - 2, 0] = tiles[matchings.Item2.Item1];
                        tilesRemoved.Remove(tiles[matchings.Item2.Item3].id);
                        tilesRemoved.Remove(tiles[matchings.Item2.Item1].id);
                    }
                    if (matchings.Item2.Item2 != 0 && matchings.Item2.Item1 != 0) // bottom right
                    { 
                        tileGrid[tileSize - 1, tileSize - 1] = tile;
                        tileGrid[tileSize - 1, tileSize - 2] = tiles[matchings.Item2.Item2];
                        tileGrid[tileSize - 2, tileSize - 1] = tiles[matchings.Item2.Item1];
                        tilesRemoved.Remove(tiles[matchings.Item2.Item2].id);
                        tilesRemoved.Remove(tiles[matchings.Item2.Item1].id);
                    }
                    tilesRemoved.Remove(tile.id);
                }
            }
            Console.WriteLine($"Part one answer: {partOneAns}");
        }

        static (int, (int, int, int, int)) MatchingSides(Tile tile)
        {
            int matches = 0, left = 0, right = 0, top = 0, bottom = 0;
            foreach (var adj in tiles.Values)
            {
                if (tile.id == adj.id) { continue; }
                foreach (var edge in adj.edges)
                {
                    if (edge.Equals(tile.GetTopRow())) { matches++; top = adj.id; }
                    else if (edge.Equals(tile.GetLeftRow())) { matches++; left = adj.id; }
                    else if (edge.Equals(tile.GetRightRow())) { matches++; right = adj.id; }
                    else if (edge.Equals(tile.GetBottomRow())) { matches++; bottom = adj.id; }
                }
            }
            return (matches, (top, left, right, bottom));
        }

        static void GenerateTiles(List<string> input)
        {
            tiles = new Dictionary<int, Tile>();
            tilesRemoved = new Dictionary<int, Tile>();
            int start = 0;
            int id = 0;
            int size = input[1].Length;
            for (int i = 0; i < input.Count; i++)
            {
                if (String.IsNullOrWhiteSpace(input[i]))
                {
                    tiles[id] = (new Tile(input.GetRange(start, size).ToArray(), id));
                    tilesRemoved[id] = (new Tile(input.GetRange(start, size).ToArray(), id));
                }
                if (input[i].Contains('T'))
                {
                    id = int.Parse(input[i].Split(" ")[1].Trim(':'));
                    start = i + 1;
                }
            }
            tileSize = (int)Math.Sqrt(tiles.Count());
            tileGrid = new Tile[tileSize, tileSize];
        }
    }
}
