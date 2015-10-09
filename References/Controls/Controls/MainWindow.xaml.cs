using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Controls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //btnClose.BackgroundBrush = new SolidColorBrush(Color.FromRgb(150, 0, 0));
            //btnClose.MouseEnterColor = Color.FromRgb(229, 20, 0);
            //btnClose.MouseDownColor = Color.FromRgb(175, 0, 0);
            //btnClose.FocusBorderColor = Color.FromRgb(255, 0, 0);

            //btnMin.BackgroundBrush = new SolidColorBrush(Color.FromRgb(0, 0, 190));
            //btnMin.MouseEnterColor = Color.FromRgb(0, 0, 239);
            //btnMin.MouseDownColor = Color.FromRgb(0, 0, 139);
            //btnMin.FocusBorderColor = Color.FromRgb(0, 0, 255);

            pro.progress.Value = 0;

            DoubleAnimation da = new DoubleAnimation();
            da.AutoReverse = true;
            da.From = 0;
            da.To = 100;
            da.Duration = TimeSpan.FromSeconds(5);
            da.RepeatBehavior = RepeatBehavior.Forever;

            pro.progress.BeginAnimation(ProgressBar.ValueProperty, da);
        }

        private void btn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void btnMin_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //DragMove();
        }
    }
}
