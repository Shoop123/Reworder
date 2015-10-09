using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Anti_Plagiarism
{
    class Modification
    {
        static double acceleration = 0.5;

        static Duration time = TimeSpan.FromMilliseconds(500);

        public static ProgressBar pb = null;

        public static RichTextBox tb = null;

        public static MainWindow mw = null;

        public static bool shouldClose = false;

        public static bool shouldMinimize = false;

        public static double currentToValue = 0.0;

        public static StackPanel sp = null;

        public delegate void EnableCallBack(bool isIndeterminate = false);
        public static EnableCallBack enableCallBack;

        public static string ToLower(string word)
        {
            StringBuilder newWord = new StringBuilder();

            for (int i = 0; i < word.Length; i++)
            {
                newWord.Append(char.ToLower(word[i]));
            }

            return newWord.ToString();
        }

        public static void Disable(bool isIndeterminate = false)
        {
            //pb.IsIndeterminate = isIndeterminate;
            mw.btnGrammar.IsEnabled = false;
            mw.btnChange.IsEnabled = false;
            tb.IsEnabled = false;
        }

        public static void Enable(bool isIndeterminate = false)
        {
            //pb.IsIndeterminate = isIndeterminate;
            mw.btnGrammar.IsEnabled = true;
            mw.btnChange.IsEnabled = true;
            tb.IsEnabled = true;
        }

        public static void FadeGridElement(double fromValue, double toValue, Grid control)
        {
            DoubleAnimation da = new DoubleAnimation(fromValue, toValue, time);
            da.DecelerationRatio = acceleration;
            control.BeginAnimation(Grid.OpacityProperty, da);
        }

        public static void FadeScrollElement(double fromValue, double toValue, Control control)
        {
            DoubleAnimation da = new DoubleAnimation(fromValue, toValue, time);
            da.DecelerationRatio = acceleration;
            control.BeginAnimation(Control.OpacityProperty, da);
        }

        public static void FadeUIElement(double fromValue, double toValue, UIElement control)
        {
            DoubleAnimation da = new DoubleAnimation(fromValue, toValue, time);
            da.DecelerationRatio = acceleration;
            da.Completed += UIElementFadeCompleted;
            control.BeginAnimation(UIElement.OpacityProperty, da);
        }

        public static void MoveUIElement(StackPanel control, double displacement)
        {
            DoubleAnimationUsingPath da = new DoubleAnimationUsingPath();
            da.Duration = time;
            da.DecelerationRatio = acceleration;

            PathGeometry pg = new PathGeometry();
            PathFigure pf = new PathFigure();
            pf.StartPoint = new Point(control.Margin.Left, control.Margin.Top);
            PolyBezierSegment pbs = new PolyBezierSegment();

            for(int i = 0; i < displacement; i++)
                pbs.Points.Add(new Point(control.Margin.Left, control.Margin.Top - i));

            pf.Segments.Add(pbs);
            pg.Figures.Add(pf);

            da.PathGeometry = pg;
            da.Source = PathAnimationSource.X;

            Storyboard sb = new Storyboard();
            sb.Children.Add(da);
            sb.Begin(control);
        }

        private static void UIElementFadeCompleted(object sender, EventArgs e)
        {
            if (shouldClose) mw.Close();
            else if (shouldMinimize) mw.WindowState = WindowState.Minimized;
        }

        public static void SizeHeight(double fromValue, double toValue, Control control)
        {
            DoubleAnimation da = new DoubleAnimation(fromValue, toValue, time);
            da.DecelerationRatio = acceleration;
            control.BeginAnimation(Control.HeightProperty, da);
        }

        public static void ChangeValue(double fromValue, double toValue, ProgressBar control)
        {
            DoubleAnimation da = new DoubleAnimation(fromValue, toValue, time);
            da.DecelerationRatio = acceleration;
            da.FillBehavior = FillBehavior.Stop;
            currentToValue = toValue;
            da.Completed += ValueChanged;
            control.BeginAnimation(ProgressBar.ValueProperty, da);
        }

        private static void ValueChanged(object sender, EventArgs e)
        {
            pb.Value = 0.0;
        }

        public static void Progress(double i)
        {
            
            pb.Dispatcher.Invoke(mw.progressCallBack, new object[] { i });
        }

        public static void SetNewText(string text)
        {
            tb.Dispatcher.Invoke(mw.textCallBack, text);
        }
    }
}
