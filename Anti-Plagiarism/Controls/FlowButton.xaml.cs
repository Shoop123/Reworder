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

namespace Anti_Plagiarism.Controls
{
    /// <summary>
    /// Interaction logic for CustomButton.xaml
    /// </summary>
    public partial class FlowButton : UserControl
    {
        private SolidColorBrush _BackgroundBrush;

        public SolidColorBrush BackgroundBrush
        {
            get { return this._BackgroundBrush; }
            set
            {
                this._BackgroundBrush = value;
                this.Background = _BackgroundBrush;
                UpdateAnims();
            }
        }

        private Color _MouseEnterColor;

        public Color MouseEnterColor
        {
            get { return this._MouseEnterColor; }
            set
            {
                this._MouseEnterColor = value;
                UpdateAnims();
            }
        }

        private Color _MouseDownColor { get; set; }

        public Color MouseDownColor
        {
            get { return this._MouseDownColor; }
            set
            {
                this._MouseDownColor = value;
                UpdateAnims();
            }
        }

        private Color _FocusBorderColor;

        public Color FocusBorderColor
        {
            get { return this._FocusBorderColor; }
            set
            {
                this._FocusBorderColor = value;
                UpdateAnims();
            }
        }

        private ColorAnimation _MouseEnterAnim;

        private ColorAnimation _MouseLeaveAnim;

        private ColorAnimation _MouseDownAnim;

        private ColorAnimation _MouseUpAnim;

        private TimeSpan _Length = TimeSpan.FromMilliseconds(200);

        private TimeSpan _LengthOfClick = TimeSpan.FromMilliseconds(75);

        private String _Text;

        public String Text
        {
            get
            {
                return _Text;
            }
            set
            {
                _Text = value;
                lblText.Content = _Text;
            }
        }

        public FlowButton()
        {
            InitializeComponent();
            Init();
        }

        public FlowButton(SolidColorBrush mouseLeavebackground, Color mouseDownBackground, Color mouseEnterBackground, Color focusBorderColor)
            : this()
        {
            _MouseEnterColor = mouseEnterBackground;
            _MouseDownColor = mouseDownBackground;
            _BackgroundBrush = mouseLeavebackground;
            _FocusBorderColor = focusBorderColor;
        }

        private void UpdateAnims()
        {
            _MouseEnterAnim = new ColorAnimation(_MouseEnterColor, _Length);
            _MouseLeaveAnim = new ColorAnimation(_BackgroundBrush.Color, _Length);
            _MouseUpAnim = new ColorAnimation(_MouseEnterColor, _Length);
            _MouseDownAnim = new ColorAnimation(_MouseDownColor, _LengthOfClick);

            this.Background = _BackgroundBrush;
            this.BorderBrush = _BackgroundBrush;
        }

        private void Init()
        {
            _MouseDownColor = Colors.Gray;//FF808080
            _MouseEnterColor = Colors.DarkGray;
            _FocusBorderColor = Colors.LightSlateGray;
            _BackgroundBrush = new SolidColorBrush(Colors.LightGray);

            Text = String.Empty;

            UpdateAnims();
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            _BackgroundBrush.BeginAnimation(SolidColorBrush.ColorProperty, _MouseDownAnim);
            Focus();
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            _BackgroundBrush.BeginAnimation(SolidColorBrush.ColorProperty, _MouseEnterAnim);
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            _BackgroundBrush.BeginAnimation(SolidColorBrush.ColorProperty, _MouseLeaveAnim);
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _BackgroundBrush.BeginAnimation(SolidColorBrush.ColorProperty, _MouseUpAnim);
        }

        private void UserControl_LostFocus(object sender, RoutedEventArgs e)
        {
            this.BorderBrush = _BackgroundBrush;
        }

        private void UserControl_GotFocus(object sender, RoutedEventArgs e)
        {
            this.BorderBrush = new SolidColorBrush(_FocusBorderColor);
        }
    }
}
