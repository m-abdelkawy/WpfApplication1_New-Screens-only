using Design.Presentation.ViewModels;
using Desing.Core.Sap;
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

namespace Design.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Properties
        public GeometryEditor GeometryEditor { get; set; }
        public SectionProperties SectioProperties { get; set; }

        //Load Cases Window
        public LoadCases LoadCases { get; set; }
        #endregion
        public MainWindow()
        {
            InitializeComponent();
        }



        #region Btn_Events

        private void Btn_editBeam_Click(object sender, RoutedEventArgs e)
        {
            GeometryEditor= new GeometryEditor();
            GeometryEditor.Show();
          
        }
        #endregion

        private void btn_analyse_Click(object sender, RoutedEventArgs e)
        {
            var gemoemtryData = (GeometryEditorVM)GeometryEditor.DataContext;
        }

        private void btn_loadCases_Click(object sender, RoutedEventArgs e)
        {
            LoadCases = new LoadCases();
            LoadCases.Shows();
        }
    }
}
