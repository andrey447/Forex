using System;
using System.IO;
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
using LiveCharts;
using LiveCharts.Wpf;
using Excel = Microsoft.Office.Interop.Excel;

namespace Forex
{

    public partial class MainWindow : Window
    {
        
        //private Excel.Application excelapp;
        //private Excel.Window excelWindow;

        public MainWindow()
        {
            InitializeComponent();
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Получить объект приложения Excel.
            Excel.Application excel_app = new Excel.Application();

            // Сделать Excel видимым (необязательно).
            excel_app.Visible = true;

            // Откройте рабочую книгу только для чтения.
            Excel.Workbook workbook = excel_app.Workbooks.Open(@"C:\Users\andre\OneDrive\Рабочий стол\File1.xlsx",
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing);

            // Получить первый рабочий лист.
            Excel.Worksheet sheet = (Excel.Worksheet)workbook.Sheets[1];

            // Указываем номер столбца (таблицы Excel) из которого будут считываться данные.
            int numCol = 2;

            Excel.Range usedColumn = sheet.UsedRange.Columns[numCol];
            Array myvalues = (Array)usedColumn.Cells.Value2;
            string[] strArray = myvalues.OfType<object>().Select(o => o.ToString()).ToArray();
            double[] dataArray = new double[strArray.Length];

            for (int i=0; i<strArray.Length; i++)
            {
                dataArray[i] = Convert.ToDouble(strArray[i]);
                MessageBox.Show(dataArray[i].ToString());
            }

            // Закройте книгу без сохранения изменений.
            workbook.Close(false, Type.Missing, Type.Missing);

            // Закройте сервер Excel.
            excel_app.Quit();

            string[] lines = File.ReadAllLines(@"C:\Users\andre\OneDrive\Рабочий стол\GBPUSD_M5.xlsx");
            foreach (string s in lines)
            {
                MessageBox.Show(s);
            }
        }
    }
}
