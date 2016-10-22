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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MapControl
{
    public partial class Map : UserControl
    {
        public Map()
        {
            InitializeComponent();
            var sourceStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("MapControl.MapBrowser.html");
            MapRenderer.NavigateToStream(sourceStream);
        }

        public void AddMarker(double latitude, double longitude, string title)
        {
            MapRenderer.InvokeScript("addMarker", latitude, longitude, title);
        }
    }
}
