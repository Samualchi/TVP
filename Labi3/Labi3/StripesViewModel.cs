using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Labi3.ViewModel
{
    class StripesViewModel
    {
        //private Brush _fill;

        int _number;
        int _workWidth = 600;
        int _workHeight = 800;
        bool _gameOver = false;
        // Обеспечиваем "уникальный" рандом
        private static Random random = new Random();
        private List<Brush> brushes;

        public ObservableCollection<Node> Nodes { get; } = new ObservableCollection<Node>();

        public StripesViewModel(int n, int width, int height)
        {
            _number = n;
            _workWidth = width;
            _workHeight = height;
            CreateBrushes();
            PaintRectangle();
        }
        private void CreateBrushes()
        {
            // Создаем рандомайзер кистей
            brushes = new List<Brush>();
            for (int i = 0; i < 200; i++)
                brushes.Add(new SolidColorBrush(Color.FromRgb((byte)random.Next(1, 255), (byte)random.Next(1, 255), (byte)random.Next(1, 255))));
        }
        public void PaintRectangle()
        {
            // Создаем нод и поворачиваем его и добавляем
            for (int i = 0; i < _number; i++)
                Nodes.Add(ShapeNode.CreateNode(random.Next(_workWidth), random.Next(_workHeight), random.Next(40), brushes[random.Next(199)]));
        }
        public async void PaintRectangleAsync(int delay)
        {
            while (!_gameOver)
            {
                // Ожидаем
                await Task.Run(() =>
                {
                    Task.Delay(delay).Wait();
                });
                // Проверяем, что игра не окончена
                Check();
                // Добавлем новый нод, если игра не окончена
                if (!_gameOver)
                    Nodes.Add(ShapeNode.CreateNode(random.Next(_workWidth), random.Next(_workHeight), random.Next(40), brushes[random.Next(199)]));
            }
        }
        public void Click(Rectangle rect)
        {
            if (_gameOver) return;
            // "Помечаем" нажатый прямоугольник
            rect.Visibility = Visibility.Hidden;
            // и ищем его
            for (int i = 0; i < Nodes.Count; i++)
                if ((Nodes[i] as ShapeNode).Visibility == Visibility.Hidden)
                {
                    // Производим поиск пересечений с верхними прямоугольниками 
                    for (int j = i + 1; j < Nodes.Count; j++)
                    {
                        // Берем точки
                        var points = (Nodes[j] as ShapeNode).GetPoints();

                        if ((Nodes[i] as ShapeNode).Isintersect(points))
                        {
                            rect.Visibility = Visibility.Visible;
                            return;
                        }
                    }
                    // Удаляем не нужный прямоугольник
                    Nodes.RemoveAt(i);
                    break;
                }
        }
        private void Check()
        {
            if (Nodes.Count >= _number * 2)
            {
                Nodes.Add(new TextNode() { X = _workWidth / 2.5, Y = _workHeight / 2.5, Text = "You lose!" });
                _gameOver = true;
            }
            if (Nodes.Count == 0)
            {
                Nodes.Add(new TextNode() { X = _workWidth / 2.5, Y = _workHeight / 2.5, Text = "You win!" });
                _gameOver = true;
            }
        }
    }
}
