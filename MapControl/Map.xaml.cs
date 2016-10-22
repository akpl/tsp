using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

        private void MarkersOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var item in args.NewItems)
                    {
                        var marker = (Marker) item;
                        AddMarker(marker.Latitude, marker.Longitude, marker.Title);
                    }
                    
                    break;
                case NotifyCollectionChangedAction.Remove:
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        public ObservableCollection<Marker> Markers
        {
            get { return (ObservableCollection<Marker>)GetValue(MarkersProperty); }
            set { SetValue(MarkersProperty, value); Markers.CollectionChanged += MarkersOnCollectionChanged; }
        }

        public static readonly DependencyProperty MarkersProperty =
            DependencyProperty.Register("Markers", typeof(ObservableCollection<Marker>), typeof(Map), new PropertyMetadata(null));


        public void AddMarker(double latitude, double longitude, string title)
        {
            MapRenderer.InvokeScript("addMarker", latitude, longitude, title);
        }
    }
}
