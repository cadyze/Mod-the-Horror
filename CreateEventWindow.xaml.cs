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
        private Dictionary<string, string> eventStatDecoder = new Dictionary<string, string>()
        {
            { "funds1", "AT LEAST 1 FUND"},
            { "funds2", "AT LEAST 2 FUNDS"}
        };

        public void ReadItoInformation(string[] lines) {
            foreach (string line in lines)
            {
                if (line.Contains("name=")) txtBox_name.Text = ItoWriter.ExtractInfo(line);
                if (line.Contains("location=")) UIManager.UpdateComboBox(comboBox_location, ItoWriter.ExtractInfo(line));
                if (line.Contains("author=")) txtBox_author.Text = ItoWriter.ExtractInfo(line);
                if (line.Contains("contact=")) txtBox_contact.Text = ItoWriter.ExtractInfo(line);
                if (line.Contains("flavor=")) txtBox_flavor.Text = ItoWriter.ExtractInfo(line);
                if (line.Contains("options=")) txtBox_numOptions.Text = ItoWriter.ExtractInfo(line);
                if (line.Contains("image=")) UpdateEventImagePath(ItoWriter.ExtractInfo(line));
                if (line.Contains("about=")) txtBox_description.Text = ItoWriter.ExtractInfo(line);

                if (line.Contains("optiona=")) txtBox_optionA.Text = ItoWriter.ExtractInfo(line);
                if (line.Contains("testa=")) UIManager.UpdateComboBox(comboBox_statTestA, ItoWriter.ExtractInfo(line), eventStatDecoder);
                if (line.Contains("successa=")) txtBox_successTextA.Text = ItoWriter.ExtractInfo(line);
                if (line.Contains("winprizea=")) UIManager.UpdateComboBox(comboBox_successPrizeA, ItoWriter.ExtractInfo(line));
                if (line.Contains("winnumbera=")) UpdatePrizeAmount(comboBox_successPrizeA, comboBox_winItemA, txtBox_winAmtA, ItoWriter.ExtractInfo(line));
                if (line.Contains("failurea=")) txtBox_failTextA.Text = ItoWriter.ExtractInfo(line);
                if (line.Contains("failprizea=")) UIManager.UpdateComboBox(comboBox_failPrizeA, ItoWriter.ExtractInfo(line));
                if (line.Contains("failnumbera=")) UpdatePrizeAmount(comboBox_failPrizeA, comboBox_failItemA, txtBox_failAmtA, ItoWriter.ExtractInfo(line));

                if (line.Contains("optionb=")) txtBox_optionB.Text = ItoWriter.ExtractInfo(line);
                if (line.Contains("testb=")) UIManager.UpdateComboBox(comboBox_statTestB, ItoWriter.ExtractInfo(line), eventStatDecoder);
                if (line.Contains("successb=")) txtBox_successTextB.Text = ItoWriter.ExtractInfo(line);
                if (line.Contains("winprizeb=")) UIManager.UpdateComboBox(comboBox_successPrizeB, ItoWriter.ExtractInfo(line));
                if (line.Contains("winnumberb=")) UpdatePrizeAmount(comboBox_successPrizeB, comboBox_winItemB, txtBox_winAmtB, ItoWriter.ExtractInfo(line));
                if (line.Contains("failureb=")) txtBox_failTextB.Text = ItoWriter.ExtractInfo(line);
                if (line.Contains("failprizeb=")) UIManager.UpdateComboBox(comboBox_failPrizeB, ItoWriter.ExtractInfo(line));
                if (line.Contains("failnumberb=")) UpdatePrizeAmount(comboBox_failPrizeB, comboBox_failItemB, txtBox_failAmtB, ItoWriter.ExtractInfo(line));

                if (line.Contains("optionc=")) txtBox_optionC.Text = ItoWriter.ExtractInfo(line);
                if (line.Contains("testc=")) UIManager.UpdateComboBox(comboBox_statTestC, ItoWriter.ExtractInfo(line), eventStatDecoder);
                if (line.Contains("successc=")) txtBox_successTextC.Text = ItoWriter.ExtractInfo(line);
                if (line.Contains("winprizec=")) UIManager.UpdateComboBox(comboBox_successPrizeC, ItoWriter.ExtractInfo(line));
                if (line.Contains("winnumberc=")) UpdatePrizeAmount(comboBox_successPrizeC, comboBox_winItemC, txtBox_winAmtC, ItoWriter.ExtractInfo(line));
                if (line.Contains("failurec=")) txtBox_failTextC.Text = ItoWriter.ExtractInfo(line);
                if (line.Contains("failprizec=")) UIManager.UpdateComboBox(comboBox_failPrizeC, ItoWriter.ExtractInfo(line));
                if (line.Contains("failnumberc=")) UpdatePrizeAmount(comboBox_failPrizeC, comboBox_failItemC, txtBox_failAmtC, ItoWriter.ExtractInfo(line));
            }
        }

        public void UpdatePrizeAmount(ComboBox prizeBox, ComboBox itemBox, TextBox amountText, string prizeValue) {
            switch (((string)prizeBox.SelectedItem).ToLower())
            {
                case "stamina":
                case "reason":
                case "doom":
                case "experience":
                case "funds":
                case "ending_trigger":
                    amountText.Text = prizeValue;
                    break;
                case "item":
                case "itempool":
                    UIManager.UpdateComboBox(itemBox, prizeValue);
                    break;
                default:
                    amountText.Text = "";
                    break;
            }
        }
        public void UpdateCurrentItoName(string path)
        {
            fileName = System.IO.Path.GetFileName(path);
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

            //Initialize combo boxes
            UIManager.InitializeComboBox(comboBox_location, @"GameInformation\EventLocations.txt");
            ComboBox[] prizes = { comboBox_failPrizeA, comboBox_successPrizeA, comboBox_failPrizeB, comboBox_successPrizeB, comboBox_failPrizeC, comboBox_successPrizeC };
            ComboBox[] itemBoxes = { comboBox_failItemA, comboBox_winItemA, comboBox_failItemB, comboBox_winItemB, comboBox_failItemC, comboBox_winItemC };
            ComboBox[] statTests = { comboBox_statTestA, comboBox_statTestB, comboBox_statTestC };
            UIManager.InitializeComboBox(statTests, @"GameInformation\EventStatTests.txt", eventStatDecoder);
            UIManager.InitializeComboBox(prizes, @"GameInformation\EventPrizes.txt");
            UIManager.InitializeComboBox(itemBoxes, @"GameInformation\ObtainableItems.txt");

        }

        private void comboBox_selectedOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string? selectedOption = (string)((ComboBoxItem)comboBox_selectedOption.SelectedItem).Content;
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

        private void OnSaveClicked(object sender, RoutedEventArgs e)
        {
            string location = ((string)comboBox_location.SelectedItem).ToLower();
            string[] winPrizeA = GetPrizeInfo(comboBox_successPrizeA, comboBox_winItemA, txtBox_winAmtA);
            string[] losePrizeA = GetPrizeInfo(comboBox_failPrizeA, comboBox_failItemA, txtBox_failAmtA);
            string[] winPrizeB = GetPrizeInfo(comboBox_successPrizeB, comboBox_winItemB, txtBox_winAmtB);
            string[] losePrizeB = GetPrizeInfo(comboBox_failPrizeB, comboBox_failItemB, txtBox_failAmtB);
            string[] winPrizeC = GetPrizeInfo(comboBox_successPrizeC, comboBox_winItemC, txtBox_winAmtC);
            string[] losePrizeC = GetPrizeInfo(comboBox_failPrizeC, comboBox_failItemC, txtBox_failAmtC);

            List<EventOption> eventOptions = new List<EventOption>();
            string statTestA = (string)comboBox_statTestA.SelectedItem;
            string statTestB = (string)comboBox_statTestB.SelectedItem;
            string statTestC = (string)comboBox_statTestC.SelectedItem;

            statTestA = eventStatDecoder.ContainsValue(statTestA) ? UIManager.GetDecoderKey(eventStatDecoder, statTestA) : statTestA;
            statTestB = eventStatDecoder.ContainsValue(statTestB) ? UIManager.GetDecoderKey(eventStatDecoder, statTestB) : statTestB;
            statTestC = eventStatDecoder.ContainsValue(statTestC) ? UIManager.GetDecoderKey(eventStatDecoder, statTestC) : statTestC;

            EventOption eoA = new EventOption(txtBox_optionA.Text, statTestA,
                txtBox_successTextA.Text, winPrizeA[0], winPrizeA[1],
                txtBox_failTextA.Text, losePrizeA[0], losePrizeA[1]);
            EventOption eoB = new EventOption(txtBox_optionB.Text, statTestB,
                txtBox_successTextB.Text, winPrizeB[0], winPrizeB[1],
                txtBox_failTextB.Text, losePrizeB[0], losePrizeB[1]);
            EventOption eoC = new EventOption(txtBox_optionC.Text, statTestC,
                txtBox_successTextC.Text, winPrizeC[0], winPrizeC[1],
                txtBox_failTextC.Text, losePrizeC[0], losePrizeC[1]);


            eventOptions.Add(eoA);
            eventOptions.Add(eoB);
            eventOptions.Add(eoC);

            string? sprEventName = System.IO.Path.GetFileName(eventImagePath);

            ItoWriter.WriteEvent(directoryPath, fileName, txtBox_name.Text, txtBox_author.Text, location,
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

        private void ChangeVisibilityOfItemBox(ComboBox prizeBox, ComboBox itemBox) {
            string currentPrize = ((string)prizeBox.SelectedItem).ToLower();
            switch (currentPrize)
            {
                case "stamina":
                case "reason":
                case "doom":
                case "experience":
                case "funds":
                case "ending_trigger":
                    itemBox.Visibility = Visibility.Hidden;
                    break;
                case "item":
                case "itempool":
                    itemBox.Visibility = Visibility.Visible;
                    break;
                default:
                    itemBox.Visibility = Visibility.Hidden;
                    break;
            }
        }
        private string[] GetPrizeInfo(ComboBox prizeComboBox, ComboBox itemBox, TextBox txtBox)
        {
            string[] prizeData = new string[2];
            prizeData[0] = ((string)prizeComboBox.SelectedItem).ToLower();
            switch (prizeData[0])
            {
                case "stamina":
                case "reason":
                case "doom":
                case "experience":
                case "funds":
                case "ending_trigger":
                    prizeData[1] = txtBox.Text;
                    break;
                case "item":
                case "itempool":
                    prizeData[1] = ((string)itemBox.SelectedItem).ToUpper();
                    break;
                default:
                    prizeData[1] = "";
                    break;
            }
            return prizeData;
        }


        private void comboBox_successPrizeA_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeVisibilityOfItemBox(comboBox_successPrizeA, comboBox_winItemA);
        }
        private void comboBox_failPrizeA_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeVisibilityOfItemBox(comboBox_failPrizeA, comboBox_failItemA);
        }
        private void comboBox_successPrizeB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeVisibilityOfItemBox(comboBox_successPrizeB, comboBox_winItemB);
        }
        private void comboBox_failPrizeB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeVisibilityOfItemBox(comboBox_failPrizeB, comboBox_failItemB);
        }
        private void comboBox_successPrizeC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeVisibilityOfItemBox(comboBox_successPrizeC, comboBox_winItemC);
        }
        private void comboBox_failPrizeC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeVisibilityOfItemBox(comboBox_failPrizeC, comboBox_failItemC);
        }
    }
}
