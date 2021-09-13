using Labi3.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Labi3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StripesViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            // Инициилизруем класс
            viewModel = new StripesViewModel(15, 600, 400);
            // Добавлем его в DataContext
            DataContext = viewModel;
        }

        private void Click(object s, MouseEventArgs e)
        {
            viewModel.Click(s as Rectangle);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Создаем прямоугольники
            viewModel.PaintRectangleAsync(700);
        }
    }
}

