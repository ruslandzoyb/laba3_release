using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;



namespace labaratorna3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random r = new Random();
       
        public MainWindow()
        {
            InitializeComponent();
            
            
            
            var col = panel.Children.OfType<ProgressBar>();
            
            foreach (var item in col)
            {
               // item.Value = 3;
                item.Height = r.Next(100, 170);
                item.Width = r.Next(50,100);
                 
            }
            first = (progressbar.Width * progressbar.Height) / 100;
            second = (progressbar2.Width * progressbar2.Height) / 100;
            third = (progressbar3.Width * progressbar3.Height) / 100;
            fourh = (progressbar4.Width * progressbar4.Height) / 100;
            //MessageBox.Show(Thread.CurrentThread.ManagedThreadId.ToString());


        }
        BackgroundWorker[] workers = new BackgroundWorker[4];
        static object locker = new object();
        static double totalSquare = 0;
        double first, second,third,fourh;
        
        double[] squares = new double[5] { 0, 0, 0, 0, 0 };
        public void DoWork(object sender, DoWorkEventArgs e)
        {
           // MessageBox.Show(Thread.CurrentThread.ManagedThreadId.ToString());
            //MessageBox.Show(Thread.CurrentThread.ManagedThreadId.ToString());
            var n = (BackgroundWorker)sender;
           

            for (int i = 0; i < 100; i++)
            {
                             


                if (n.CancellationPending)
                {

                    e.Cancel = true;
                    return;
                }

                
                Thread.Sleep(100);


                n.ReportProgress(i);
                                                       
                                                             

            }
            

        }
       


        public void Change(object sender, ProgressChangedEventArgs e)
        {

            progressbar.Value = e.ProgressPercentage;


        }
        public void Change2(object sender, ProgressChangedEventArgs e)
        {
            progressbar2.Value = e.ProgressPercentage;

        }
        public void Change3(object sender, ProgressChangedEventArgs e)
        {

            progressbar3.Value = e.ProgressPercentage;


        }
        public void Change4(object sender, ProgressChangedEventArgs e)
        {
            progressbar4.Value = e.ProgressPercentage;

        }
        public void ThreadName(BackgroundWorker n)
        {
           
        }
        private void RadioButton2_Checked(object sender, RoutedEventArgs e)
        {
            workers[1] = new BackgroundWorker();


            Set(workers[1], 2);
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            workers[0] = new BackgroundWorker();
            radioButton.IsEnabled = false;

            Set(workers[0], 1);
        }

        private void RadioButton4_Checked(object sender, RoutedEventArgs e)
        {
            workers[2] = new BackgroundWorker();


            Set(workers[2], 3);
        }

        private void RadioButton6_Checked(object sender, RoutedEventArgs e)
        {
            workers[3] = new BackgroundWorker();


            Set(workers[3], 4);
        }


        private void Set(BackgroundWorker worker, int counter)
        {

            worker.DoWork += DoWork;
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            // worker.WorkerSupportsCancellation = true;
            switch (counter)
            {
                case 1:
                    worker.ProgressChanged += Change;
                    break;
                case 2:
                    worker.ProgressChanged += Change2;
                    break;
                case 3:
                    worker.ProgressChanged += Change3;
                    break;
                case 4:
                    worker.ProgressChanged += Change4;
                    break;


                default:
                    break;
            }

            worker.RunWorkerAsync();
        }

        private void Progressbar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            squares[0] += first;
            textBox.Text = squares[0].ToString();
            TotalSquare(first );
        }

        private void Progressbar4_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            squares[3] += fourh;
            textBox3.Text = squares[3].ToString();

            TotalSquare(fourh);
        }

        private void RadioButton3_Checked(object sender, RoutedEventArgs e)
        {
            workers[1].CancelAsync();
        }

        private void RadioButton1_Checked(object sender, RoutedEventArgs e)
        {
            workers[0].CancelAsync();
        }

        private void RadioButton5_Checked(object sender, RoutedEventArgs e)
        {
            workers[2].CancelAsync();
        }

        private void RadioButton7_Checked(object sender, RoutedEventArgs e)
        {
            workers[3].CancelAsync();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private void Progressbar2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            squares[1] += second ;
            textBox1.Text = squares[1].ToString();

            TotalSquare(second );
        }
        private void TotalSquare(double par)
        {
            squares[4] += par;
            textmain.Text = squares[4].ToString();
        }

        private void Progressbar3_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            squares[2] += third ;
            textBox2.Text = squares[2].ToString();

            TotalSquare(third) ;
        }

    }
}
