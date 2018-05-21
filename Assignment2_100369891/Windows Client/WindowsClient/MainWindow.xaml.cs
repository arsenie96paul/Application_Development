using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace WindowsClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {   
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            HttpWebRequest events = WebRequest.Create("http://localhost:1148/Calendars") as HttpWebRequest;
            WebResponse response = events.GetResponse(); 
             
            Stream streams = response.GetResponseStream();
            StreamReader streamsreader = new StreamReader(streams, Encoding.UTF8);

            
           // var list = new List<string>();

            string allevents = streamsreader.ReadToEnd();

            //list.Add(streamsreader.ReadToEnd());

            var newvar = JsonConvert.DeserializeObject<JArray>(allevents);

            TableGrid.ItemsSource = newvar;             


        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


    }
}
