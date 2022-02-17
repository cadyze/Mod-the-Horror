using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Mod_the_Horror
{
    /// <summary>
    /// Interaction logic for CreateEventWindow.xaml
    /// </summary>
    public partial class CreateEventWindow : Window
    {
        public CreateEventWindow()
        {
            InitializeComponent();
        }

        private void comboBox_selectedOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string? selectedOption = (string)(((ComboBoxItem)(comboBox_selectedOption.SelectedItem)).Content);
            Trace.WriteLine(selectedOption);
            if (selectedOption != null && !selectedOption.Equals("")) {
                switch (selectedOption) {
                    case "Option 1":
                        aOptions.Visibility = Visibility.Visible;
                        bOptions.Visibility = Visibility.Hidden;
                        cOptions.Visibility = Visibility.Hidden;
                        break;
                    case "Option 2":
                        aOptions.Visibility = Visibility.Hidden;
                        bOptions.Visibility = Visibility.Visible;
                        cOptions.Visibility = Visibility.Hidden;
                        break;
                    case "Option 3":
                        aOptions.Visibility = Visibility.Hidden;
                        bOptions.Visibility = Visibility.Hidden;
                        cOptions.Visibility = Visibility.Visible;
                        break;
                }
            }
        }
    }
}
