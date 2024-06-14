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
using System.Windows.Shapes;

namespace TourPlanner.UI.Views
{
    /// <summary>
    /// Interaktionslogik für MapWindow.xaml
    /// </summary>
    public partial class MapWindow : Window
    {
        private double _scale = 1.0;
        private const double _scaleRate = 1.1;

        public MapWindow()
        {
            InitializeComponent();
            mapImage.MouseWheel += MapImage_MouseWheel;
        }
        private void MapImage_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                _scale *= _scaleRate;
            }
            else
            {
                _scale /= _scaleRate;
            }

            mapImage.LayoutTransform = new ScaleTransform(_scale, _scale);
            e.Handled = true;
        }
    }
}
