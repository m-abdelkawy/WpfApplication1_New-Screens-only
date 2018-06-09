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
using SAP2000v20;
using Design.Presentation.ViewModels;
using Design.Presentation.Model;
using Desing.Core.Sap;
using System.Data;

namespace Design.Presentation
{
    /// <summary>
    /// Interaction logic for LoadCases.xaml
    /// </summary>
    public partial class LoadCases : Window
    {
        //LoadPattern
        public List<string> loadPatternName = new List<string>();
        public List<double> SelfWtMultiplier = new List<double>();
        public List<eLoadPatternType> patternType = new List<eLoadPatternType>();


        public List<SapLoadPattern> loadPatterns = new List<SapLoadPattern>();
        public eCNameType ec = eCNameType.LoadCase;
        public LoadCases()
        {
            DataContext = new LoadCasesVM();
            InitializeComponent();

            #region DataBindingTrial
            //LoadCaseGridData load1 = new LoadCaseGridData();
            //load1.LoadcaseName = "Dead";
            //load1.Id = 5;

            //loadCaseGridData.Items.Add(load1);
            #endregion
        }

        static int AddLoadCaseBtnClicked = 0;
        DataTable dt = new DataTable();
        List<LoadCaseGridData> l = new List<LoadCaseGridData>();

        private void AddLoadCaseBtn_Click(object sender, RoutedEventArgs e)
        {
            loadCaseGridData.SelectAllCells();
            loadCaseGridData.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, loadCaseGridData);
            loadCaseGridData.UnselectAllCells();
            String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            string[] Lines = result.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            string[] Fields;
            Fields = Lines[0].Split(new char[] { ',' });
            int Cols = Fields.GetLength(0);

            //1st row must be column names; force lower case to ensure matching later on.
            for (int i = 0; i < Cols; i++)
                dt.Columns.Add(Fields[i].ToUpper(), typeof(string));
            DataRow Row;
            for (int i = 1; i < Lines.GetLength(0) - 1; i++)
            {
                Fields = Lines[i].Split(new char[] { ',' });
                Row = dt.NewRow();
                for (int f = 0; f < Cols; f++)
                {
                    Row[f] = Fields[f];
                }
                dt.Rows.Add(Row);
                loadPatternName.Add(dt.Rows[AddLoadCaseBtnClicked].ItemArray[1].ToString());



                //SelfWtMultiplier.Add(Convert.ToDouble(selfWttxtBox.Text));
                //patternType.Add((eLoadPatternType)loadTypeComboBox.SelectedValue);

                //distLoadShapes.Add(new List<Rectangle>());
            }

            //loadPatternName.Add(dgv.LoadcaseName);
            AddLoadCaseBtnClicked += 1;



            //static int loadCaseBtnClicked = 0;
            //private void loadCaseBtn_Click(object sender, RoutedEventArgs e)
            //{
            //    loadPatternName.Add(loadCasetxtBox.Text);
            //    SelfWtMultiplier.Add(Convert.ToDouble(selfWttxtBox.Text));
            //    patternType.Add((eLoadPatternType)loadTypeComboBox.SelectedValue);

            //    distLoadShapes.Add(new List<Rectangle>());
            //    loadCaseBtnClicked += 1;
            //}

            //private void loadTypeComboBox_Loaded(object sender, RoutedEventArgs e)
            //{
            //    loadTypeComboBox.ItemsSource = Enum.GetValues(typeof(eLoadPatternType)).Cast<eLoadPatternType>();
            //}

            //private void loadCaseComboBox_Loaded(object sender, RoutedEventArgs e)
            //{
            //    var loadCaseComboBox = sender as ComboBox;
            //    loadCaseComboBox.ItemsSource = loadPatternName;
            //    loadCaseComboBox.SelectedIndex = 0;
            //}

            //private void loadCaseComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
            //{
            //    var loadCaseComboBox = sender as ComboBox;
            //    string value = loadCaseComboBox.SelectedIndex.ToString();

            //    var loadComboBoxSelectedItem = e.AddedItems[0] as string;

            //    //loadCaseName = loadComboBoxSelectedItem;
            //    //this.Title = value;
            //}

            //private void loadComboBtn_Click(object sender, RoutedEventArgs e)
            //{

            //}
        }
    }
}
