using Design.Presentation.ViewModels;
using Design.Presentation.Views.Material;
using Design.Presentation.Views.Section;
using Desing.Core.Sap;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<MaterialEditorVM> Materials { get; set; }
        public ObservableCollection<SectionEditorVM> Sections { get; set; }
        //Load Cases Window

        #endregion
        public MainWindow()
        {
            InitializeComponent();
            Materials = new ObservableCollection<MaterialEditorVM>();
            Sections= new ObservableCollection<SectionEditorVM>();
        }



        #region Btn_Events

        private void Btn_editBeam_Click(object sender, RoutedEventArgs e)
        {
            GeometryEditor= new GeometryEditor();
            GeometryEditor.ShowDialog();

        }
        #endregion

        private void btn_analyse_Click(object sender, RoutedEventArgs e)
        {
            var gemoemtryData = (GeometryEditorVM)GeometryEditor.DataContext;

        }

        private void btn_loadCases_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }

        private void Btn_Material_Click(object sender, RoutedEventArgs e)
        {
            MaterialDialouge md = new MaterialDialouge
            {
                DataContext = new MaterialDialougeVM().Materials = Materials
            };
            md.ShowDialog();
            
        }

        private void Btn_Section_Click(object sender, RoutedEventArgs e)
        {
            SectionDialouge sd = new SectionDialouge
            {
                DataContext = new SectionDialougeVM().Sections = Sections
            };
            sd.Show();
        }
    }
}
