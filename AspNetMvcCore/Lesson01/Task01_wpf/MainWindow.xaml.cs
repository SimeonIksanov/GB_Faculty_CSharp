using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Lesson01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int maxFiboItems = 1;
        private double timeoutInSec;
        private Thread? fiboCalculationThread;
        public MainWindow()
        {
            InitializeComponent();
            Slider.Value = maxFiboItems;
            Slider2.Value = timeoutInSec/1000;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            maxFiboItems = (int)Slider.Value;
            listboxFiboCount.Text = maxFiboItems.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (fiboCalculationThread != null)
            {
                fiboCalculationThread.Interrupt();
                fiboCalculationThread.Join();
            }
            listBoxFiboResults.Items.Clear();
            fiboCalculationThread = new Thread(
                () => FillListBoxWithFibo(maxFiboItems, timeoutInSec));
            fiboCalculationThread.IsBackground = true;
            fiboCalculationThread.Start();
        }
        
        private void FillListBoxWithFibo(int maxItems, double timeout)
        {
            int counter = 0;
            try
            {
                foreach (int fibo in GetFibonachi(maxItems))
                {
                    int fiboItem = fibo;
                    Application.Current.Dispatcher.BeginInvoke(
                        () => listBoxFiboResults.Items.Add(fiboItem));

                    if (++counter >= maxItems) { break; }
                    Thread.Sleep((int)(timeout*1000));
                }
            }
            catch (ThreadInterruptedException)
            { }
        }
        private IEnumerable<int> GetFibonachi(int maxItems)
        {
            int a = 1, b = 1, c;
            yield return a;
            yield return b;

            while (true)
            {
                c = a + b;
                a = b;
                b = c;
                yield return c;
            }
        }

        private void Slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            timeoutInSec = Math.Round(Slider2.Value,1);
            listboxTimeout.Text = timeoutInSec.ToString();
        }
    }
}
