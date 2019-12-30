using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using LiveCharts;
using LiveCharts.Wpf;
using System.Data.Entity;
using WpfApp2.Model;
using WpfApp2.Model.Excel;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        //public string[] Labels { get; set; }
        //public Func<double, string> YFormatter { get; set; }
        
        DispatcherTimer timer = new DispatcherTimer();
        private double temp;
        private SerialPort serialPort;
        private int seconds = 0;
        private int id = 1;
        public TemptureContext db = new TemptureContext();

        public MainWindow()
        {
            InitializeComponent();
            init();
            SeriesCollection = new SeriesCollection();
            Database.SetInitializer(new TemptureDbInitializer());
 
            //modifying the series collection will animate and update the chart
            SeriesCollection.Add(new LineSeries
            {
                Title = "Temperature",
                Values = new ChartValues<double>(),
                LineSmoothness = 0, //0: straight lines, 1: really smooth lines
                PointGeometrySize = 5,
                PointForeground = Brushes.Red
            });
            try
            {
                temp = Convert.ToDouble(serialPort.ReadLine().Replace('.', ','));
            }
            catch(InvalidOperationException)
            {
                temp = 0;
            }
            db.Temptures.Add(new Tempture { Second = seconds, Value = temp });
            db.SaveChanges();
            Excel.SaveToExcelDB(new Tempture { Id = id, Second = seconds, Value = temp});
            TemptureNow.Value = temp;

            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

            DataContext = this;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            SeriesCollection[0].Values.Add(temp);
            TemptureNow.Value = temp;
            try
            {
                temp = Convert.ToDouble(serialPort.ReadLine().Replace('.', ','));
            }
            catch (InvalidOperationException)
            {
                temp = 0;
            }
            seconds++;
            id++;
            db.Temptures.Add(new Tempture { Second = seconds, Value = temp });
            db.SaveChanges();
            Excel.SaveToExcelDB(new Tempture { Id = id, Second = seconds, Value = temp });
        }

        private void init()
        {
            try
            {
                serialPort = new SerialPort()
                {
                    BaudRate = 9600,
                    PortName = "COM6"
                };
                serialPort.Open();
            }
            catch (Exception)
            {
                //MessageBox.Show("Error!!!");
            }
        }
    }
}
