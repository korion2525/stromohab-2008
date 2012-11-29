using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms.Integration;

namespace Stromohab_WPF_UserInterface
{
    /// <summary>
    /// Interaction logic for DiagnosticsWindow.xaml
    /// </summary>
    public partial class DiagnosticsWindow : Window
    {
        public DiagnosticsWindow()
        {
            InitializeComponent();
        

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowsFormsHost host = new WindowsFormsHost();

            ZedGraphDiagnosticsControl.DiagnosticsControlZedGraph graph = new ZedGraphDiagnosticsControl.DiagnosticsControlZedGraph();

            host.Child = graph;
            gridLayout.Children.Add(host);
            this.Width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;

            graph.Disposed += new EventHandler(graph_Disposed);

        }

        void graph_Disposed(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
