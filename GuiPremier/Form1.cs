using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace GuiPremier
{
    public partial class Form1 : Form
    {
        private Dictionary<string, Premier> premiers = new Dictionary<string, Premier>();

        public Form1()
        {
            InitializeComponent();
            InitializeGui();
            listBox1.SelectedValueChanged += ListBox1_SelectedValueChanged;
        }

        private void InitializeGui()
        {
            // Initialize to an empty collection.
            premiers = new Dictionary<string, Premier>();

            // Obtain a list of premiers by calling the static GetPremier() method of the Premier class.
            var premierList = Premier.GetPremiers();

            // Populate the dictionary and the ListBox.
            foreach (var premier in premierList)
            {
                var key = premier.GenerateKey();
                premiers.Add(key, premier);
                listBox1.Items.Add(key);
            }

            // Select "Macdonald" by default
            int macdonaldIndex = listBox1.FindString("Macdonald");
            if (macdonaldIndex != -1)
            {
                listBox1.SelectedIndex = macdonaldIndex;
                UpdateGui(listBox1.SelectedItem.ToString());
            }
        }


        private void UpdateGui(string key)
        {
            if (premiers.TryGetValue(key, out Premier premier))
            {
                // Assign premier properties to labels.
                label1.Text = premier.Name;
                label2.Text = premier.Life;
                label3.Text = $"Start: {premier.Start}, End: {premier.End}";

                // Load image
                string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", $"{key}.jpg");
                if (File.Exists(imagePath))
                {
                    pictureBox1.Image = Image.FromFile(imagePath);
                }
                else
                {
                    pictureBox1.Image = null;
                }
            }
            else
            {
                MessageBox.Show("Premier not found!");
            }
        }

        private void ListBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                UpdateGui(listBox1.SelectedItem.ToString());
            }
        }
    }
}
