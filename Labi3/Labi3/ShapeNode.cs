//using System.ComponentModel;
//using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;


namespace Labi3.ViewModel
{
    public class ShapeNode : Node
    {
        static int _widthRect = 150;
        static int _heightRect = 25;

        public int Height { get; set; }
        public int Width { get; set; }
        public Brush Fill { get; set; }
        public Brush Stroke { get; set; }
        public Visibility Visibility { get; set; }

        public int GetX1 { get => Width + (int)X; }
        public int GetY1 { get => (int)Y; }

        public int GetX2 { get => Width + (int)X; }
        public int GetY2 { get => Height + (int)Y; }

        public int GetX3 { get => (int)X; }
        public int GetY3 { get => Height + (int)Y; }

        public Point[] GetPoints()
        {
            // Возвращем все точки этого нода
            return new Point[]
            {
                new Point(X, Y),
                new Point(GetX1, GetY1),
                new Point(GetX2, GetY2),
                new Point(GetX3, GetY3)
            };
        }
        public static Node CreateNode(int x, int y, int rotate, Brush brush)
        {
            // Создаем нод
            var node = new ShapeNode()
            {
                Width = _widthRect,
                Height = _heightRect,
                Fill = brush,
                Stroke = new SolidColorBrush(Colors.Black),
                X = x,
                Y = y
            };
            // Меняем его размеры если надо повернуть
            if (rotate > 20)
            {
                node.Width = _heightRect;
                node.Height = _widthRect;
            }

            return node;
        }
        public bool Isintersect(Point[] points)
        {
            var thisPoints = GetPoints();

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    int index = i;
                    if (i == 3) index = -1; // Проверяем 3 и 0 координаты первого массива

                    if (j == 3)
                    {
                        if (Intersects(thisPoints[i], thisPoints[index + 1], points[j], points[0]) != null) // Проверяем 3 и 0 координаты второго массива
                            return true;
                        continue;
                    }

                    if (Intersects(thisPoints[i], thisPoints[index + 1], points[j], points[j + 1]) != null)
                        return true;
                }
            return false;
        }
        public static Vector? Intersects(Point a1, Point a2, Point b1, Point b2)
        {
            // Нахождение пересечения, если оно найжено вернет точку пересечения,
            // если нет null
            Vector b = a2 - a1;
            Vector d = b2 - b1;
            var bDotDPerp = b.X * d.Y - b.Y * d.X;

            if (bDotDPerp == 0)
                return null;

            Vector c = b1 - a1;
            var t = (c.X * d.Y - c.Y * d.X) / bDotDPerp;
            if (t < 0 || t > 1)
            {
                return null;
            }

            var u = (c.X * b.Y - c.Y * b.X) / bDotDPerp;
            if (u < 0 || u > 1)
            {
                return null;
            }

            return new Vector(a1.X, a1.Y) + t * b;
        }
    }
}
