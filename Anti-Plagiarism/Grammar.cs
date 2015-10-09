using AfterTheDeadline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Anti_Plagiarism
{
    class Grammar
    {
        private const string POINT = "• ";

        private static string Data { get; set; }

        private delegate void ErrorCallBack(List<Error> errors);
        private ErrorCallBack errorCallBack;

        public Grammar(string data)
        {
            Data = data;
            errorCallBack = new ErrorCallBack(GenerateLists);
            Modification.enableCallBack = new Modification.EnableCallBack(Modification.Enable);
        }

        public void FindErrors()
        {
            Modification.Disable(true);
            Modification.sp.Children.Clear();
            ThreadPool.QueueUserWorkItem(Find);
        }

        private void Find(object o)
        {
            try
            {
                AfterTheDeadlineService.InitService("anti_plagiarizer", DateTime.Now.Ticks.ToString());

                List<Error> grammarErrors = AfterTheDeadlineService.CheckGrammar(Data).ToList();

                Modification.mw.Dispatcher.Invoke(errorCallBack, grammarErrors);
            }
            catch (Exception e)
            {
                MessageBox.Show("The following error occured during the grammar check:\n" + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Modification.mw.Dispatcher.Invoke(Modification.enableCallBack, false);
            }
        }

        private void GenerateLists(List<Error> errors)
        {
            List<StackPanel> panels = new List<StackPanel>();

            int startIndex = 0;

            if(errors != null)
                foreach (Error e in errors)
                {
                    if (e.Suggestions != null)
                    {
                        StackPanel sp = new StackPanel();
                        sp.Opacity = 0;

                        Label lblError = new Label();
                        lblError.Content = e.Description + "\nSuggestions:";
                        lblError.FontWeight = FontWeights.Bold;
                        sp.Children.Add(lblError);

                        IndexCounter info = Fetch(e.String, Data, startIndex);
                        startIndex = info.End;

                        foreach (string s in e.Suggestions)
                        {
                            TextWorker tw = new TextWorker(info.Start, info.End);
                            ToolTip tt = new ToolTip();
                            tt.Content = "Start: " + info.Start + "\nEnd: " + info.End;
                            tw.MouseEnter += tw_MouseEnter;
                            tw.MouseLeave += tw_MouseLeave;
                            tw.MouseDoubleClick += tw_MouseDoubleClick;
                            tw.ToolTip = tt;
                            tw.Content = POINT + s;
                            sp.Children.Add(tw);
                        }

                        panels.Add(sp);
                    }
                }

            foreach (StackPanel panel in panels)
            {
                Modification.sp.Children.Add(panel);
                Modification.FadeUIElement(0, 1, panel);
            }

            Update();

            Modification.Enable();
        }

        private void tw_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TextWorker tw = sender as TextWorker;

            string newText = new TextRange(Modification.tb.Document.ContentStart, Modification.tb.Document.ContentEnd).Text;
            string textToInsert = ((string)tw.Content).Remove(0, 2);

            newText =  newText.Remove(tw.startIndex, (tw.endIndex - tw.startIndex));
            newText = newText.Insert(tw.startIndex, textToInsert);

            FlowDocument doc = new FlowDocument();

            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(new Run(newText));

            doc.Blocks.Add(paragraph);

            Modification.tb.Document = doc;

            Data = newText;

            FindErrors();

            Modification.sp.Children.Remove((UIElement)tw.Parent);

            Update();
        }

        private void tw_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            SetBackground(sender, false);
        }

        private void tw_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            SetBackground(sender, true);
        }

        private void SetBackground(object sender, bool on)
        {
            Label lbl = sender as Label;
            if (on == true)
                lbl.Foreground = new SolidColorBrush(Color.FromRgb(0, 80, 239));
            else
                lbl.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        }

        private IndexCounter Fetch(string error, string data, int startIndex)
        {
            int start = data.IndexOf(error, startIndex);
            int end = start + error.Length;
            return new IndexCounter(start, end, startIndex + error.Length);
        }

        private void Update()
        {
            Modification.mw.UpdateLayout();
            double totalHeight = 0.0;

            foreach (StackPanel sp in Modification.sp.Children) totalHeight += sp.ActualHeight;

            Modification.sp.Height = totalHeight;
        }
    }
}
