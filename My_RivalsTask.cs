using System;
using System.Collections.Generic;
using System.Drawing;

namespace Rivals
{
    public class My_RivalsTask
    {/*функция, разделяющая карту между игроками.
    Нужно определить, до каких из клеток карты каждый игрок
    сможет дойти быстрее, вне зависимости от тактики остальных.
    Ходят игроки по очереди, начиная с первого.*/
        public static IEnumerable<OwnedLocation> AssignOwners(Map map)
        {
            var queue = new Queue<Tuple<Point, int, int>>();
            var visited = new HashSet<Point>();
            for (int i = 0; i < map.Players.Length; i++)
            {
                queue.Enqueue(Tuple.Create(new Point(map.Players[i].X,
                                                     map.Players[i].Y), i, 0));
            }

            while (queue.Count != 0)
            {
                var dequeued = queue.Dequeue();
                var point = dequeued.Item1;
                if (point.X < 0 || point.X >= map.Maze.GetLength(0)
                    || point.Y < 0 || point.Y >= map.Maze.GetLength(1)) continue;
                if (map.Maze[point.X, point.Y] == MapCell.Wall) continue;
                if (visited.Contains(point)) continue;
                visited.Add(point);
                yield return new OwnedLocation(dequeued.Item2,
                                               new Point(point.X, point.Y), dequeued.Item3);
                for (var dy = -1; dy <= 1; dy++)
                    for (var dx = -1; dx <= 1; dx++)
                        if (dx != 0 && dy != 0) continue;
                        else queue.Enqueue(Tuple.Create(new Point
                        {
                            X = point.X + dx,
                            Y = point.Y + dy
                        },
                            dequeued.Item2, dequeued.Item3 + 1));
            }
        }
    }
}
