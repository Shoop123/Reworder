using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace Anti_Plagiarism
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    //Change form opacity for fade in on start

    public partial class MainWindow : Window
    {
        const int TRANSPARENT = 0;
        const int OPAQUE = 1;

        double txtInputStartHeight;

        Reword r = new Reword();

        public delegate void CallBack(double amount);
        public delegate void CallBack2(string text);
        public CallBack progressCallBack;
        public CallBack2 textCallBack;

        public MainWindow()
        {
            InitializeComponent();
            InitializeProgram();
        }

        private void InitializeProgram()
        {
            txtInputStartHeight = txtInput.Height;
            Modification.pb = this.pbChange.progress;
            Modification.tb = this.txtInput;
            Modification.sp = this.spGrammar;
            Modification.mw = this;

            progressCallBack = new CallBack(AddProgress);
            textCallBack = new CallBack2(AddNewText);

            txtInput.TextChanged += txtInput_TextChanged;

            txtInput.Focus();
        }

        private void frmAntiPlagiarizer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void NewView(bool decrement)
        {
            if (!decrement && txtInput.Height < txtInputStartHeight) return;
            else if (decrement && txtInput.Height == txtInputStartHeight) return;

            if(decrement)
                Modification.SizeHeight(txtInputStartHeight / 2, txtInputStartHeight, txtInput);
            else Modification.SizeHeight(txtInputStartHeight, txtInputStartHeight / 2, txtInput);

            double fromValue = TRANSPARENT;
            double toValue = OPAQUE;

            if(decrement)
            {
                fromValue = toValue;
                toValue = TRANSPARENT;
            }

            Modification.FadeGridElement(fromValue, toValue, grdSettings);
            Modification.FadeScrollElement(fromValue, toValue, scrlGrammar);
        }

        private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = new TextRange(txtInput.Document.ContentStart, txtInput.Document.ContentEnd).Text;
            if (text.Length >= 1 && !String.IsNullOrWhiteSpace(text)) NewView(false);
            else if (text.Length == 0 || String.IsNullOrWhiteSpace(text)) NewView(true);
        }

        public void AddProgress(double amount)
        {
            if (pbChange.progress.Value + amount <= pbChange.progress.Maximum)
                pbChange.progress.Value += amount;
            else
                pbChange.progress.Value = pbChange.progress.Maximum;
        }

        private void AddNewText(string text)
        {
            FlowDocument doc = new FlowDocument();

            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(new Run(text));

            doc.Blocks.Add(paragraph);

            txtInput.Document = doc;
            Modification.Enable();
        }

        private void txtInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (pbChange.progress.Value > 0)
                Modification.ChangeValue(pbChange.progress.Value, 0, pbChange.progress);
        }

        private void Fade(bool fadeOut)
        {
            if(fadeOut && this.Opacity == 1)
                Modification.FadeUIElement(OPAQUE, TRANSPARENT, this);
            else if(!fadeOut && this.Opacity == 0)Modification.FadeUIElement(TRANSPARENT, OPAQUE, this);
        }

        private void frmAntiPlagiarizer_Activated(object sender, EventArgs e)
        {
            Modification.shouldMinimize = false;
            Fade(false);
        }

        private void btnClose_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Modification.shouldClose = true;
            Fade(true);
        }

        private void btnMinimize_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Modification.shouldMinimize = true;
            Modification.FadeUIElement(OPAQUE, TRANSPARENT, this);
        }

        private void btnGrammar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            new Grammar(new TextRange(txtInput.Document.ContentStart, txtInput.Document.ContentEnd).Text).FindErrors();
        }

        private void btnChange_MouseUp(object sender, MouseButtonEventArgs e)
        {
            pbChange.progress.Value = 0;
            r.ChangeText(new TextRange(txtInput.Document.ContentStart, txtInput.Document.ContentEnd).Text);
        }
    }
}

