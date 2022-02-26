using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Mod_the_Horror
{
    class UIManager
    {
        public static void UpdateComboBox(ComboBox comboBox, string itemToSelect, Dictionary<string, string>? decoder = null) {
            if (decoder != null && decoder.ContainsKey(itemToSelect)) comboBox.SelectedItem = decoder[itemToSelect];
            else comboBox.SelectedItem = itemToSelect;
        }

        public static void InitializeComboBox(ComboBox comboBox, string path, Dictionary<string, string>? decoder = null)
        {
            string[] information = System.IO.File.ReadAllLines(path);
            foreach (string line in information) {
                if (decoder != null && decoder.ContainsKey(line)) comboBox.Items.Add(decoder[line]);
                else comboBox.Items.Add(line);
            }
            comboBox.SelectedIndex = 0;
        }
        public static void InitializeComboBox(ComboBox[] comboBoxes, string path, Dictionary<string, string>? decoder = null)
        {
            foreach (ComboBox box in comboBoxes) InitializeComboBox(box, path, decoder);
        }

        public static string GetDecoderKey(Dictionary<string, string> decoder, string valueOfKey) {
            if (decoder.ContainsValue(valueOfKey)) {
                foreach (KeyValuePair<string, string> pair in decoder) {
                    if (pair.Value == valueOfKey) return pair.Key;
                }
            }
            return "";
        }
    }
}
