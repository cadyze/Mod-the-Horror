using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
        private string fileName = "";
        private string directoryPath = "";
        private string spriteDirectoryPath = "";
        private string eventImagePath = "";
        public void UpdateCurrentItoName(string path)
        {
            fileName = System.IO.Path.GetFileName(path);
            //string? directoryPath = System.IO.Path.GetDirectoryName(path);
            //if (directoryPath != null) UpdateCurrentDirectory(directoryPath);
        }

        public void UpdateEventImagePath(string newPath)
        {
            eventImagePath = newPath;
            string? spriteLocation = System.IO.Path.GetDirectoryName(newPath);
            if (spriteDirectoryPath.Equals("") && spriteLocation != null) UpdateCurrentSpriteDirectory(System.IO.Path.Combine(directoryPath, spriteLocation));
            if (!newPath.Equals("")) FileManager.UpdateImage(img_event, System.IO.Path.Combine(directoryPath, eventImagePath));
        }

        public void UpdateCurrentSpriteDirectory(string newPath) {
            spriteDirectoryPath = newPath;
        }
        public void UpdateCurrentDirectory(string newPath)
        {
            directoryPath = newPath;
        }

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

        private void txtBox_numOptions_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int testNum = int.Parse(txtBox_numOptions.Text);
                if (testNum < 1) txtBox_numOptions.Text = "1";
                if (testNum > 3) txtBox_numOptions.Text = "3";
            }
            catch (FormatException) 
            {
                txtBox_numOptions.Text = "1";
            }
        }
        private void NumberBoxChanged(object sender, TextChangedEventArgs e)
        {
            TextBox numberBox = (TextBox)sender;
            int numberTyped = 0;
            try
            {
                numberTyped = int.Parse(numberBox.Text, NumberStyles.AllowLeadingSign);
                if (numberTyped > 99) numberBox.Text = "99";
                else if (numberTyped < -99) numberBox.Text = "-99";
                else numberBox.Text = $"{numberTyped}";
            }
            catch (FormatException)
            {
                numberBox.Text = "1";
            }

        }

        private void OnSaveClicked(object sender, RoutedEventArgs e) {
            List<EventOption> eventOptions = new List<EventOption>();

            EventOption eoA = new EventOption(txtBox_optionA.Text, txtBox_testStatA.Text,
                txtBox_successTextA.Text, txtBox_successPrizeA.Text, int.Parse(txtBox_winAmtA.Text),
                txtBox_failTextA.Text, txtBox_failPrizeA.Text, int.Parse(txtBox_failAmtA.Text));
            EventOption eoB = new EventOption(txtBox_optionB.Text, txtBox_testStatB.Text,
                txtBox_successTextB.Text, txtBox_successPrizeB.Text, int.Parse(txtBox_winAmtB.Text),
                txtBox_failTextB.Text, txtBox_failPrizeB.Text, int.Parse(txtBox_failAmtB.Text));
            EventOption eoC = new EventOption(txtBox_optionC.Text, txtBox_testStatC.Text,
                txtBox_successTextC.Text, txtBox_successPrizeC.Text, int.Parse(txtBox_winAmtC.Text),
                txtBox_failTextC.Text, txtBox_failPrizeC.Text, int.Parse(txtBox_failAmtC.Text));
            eventOptions.Add(eoA);
            eventOptions.Add(eoB);
            eventOptions.Add(eoC);

            string? sprEventName = System.IO.Path.GetFileName(eventImagePath);

            ItoWriter.WriteEvent(directoryPath, fileName, txtBox_name.Text, txtBox_author.Text, txtBox_location.Text,
                txtBox_contact.Text, txtBox_flavor.Text, int.Parse(txtBox_numOptions.Text), System.IO.Path.GetFileName(spriteDirectoryPath), sprEventName,
                txtBox_description.Text, eventOptions);

            FileManager.SaveImage(img_event, System.IO.Path.Combine(spriteDirectoryPath, sprEventName));
        }

        private void ChangeEventSprite_Click(object sender, RoutedEventArgs e)
        {

            string potentialPath = FileManager.ImportSprite();
            if (!potentialPath.Equals(""))
            {
                //_hasDirtyData = true;
                eventImagePath = potentialPath;
                FileManager.UpdateImage(img_event, eventImagePath);
            }
        }
    }
}
