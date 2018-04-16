/****************************************************************
 * 
 * Program Name: PigLatin
 * Author: Ben Jasperson
 * Purpose: Allows user to type text, or select text from a file.
 *          the text is then translated into pig latin.
 *   
 *   **********************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.IO;

namespace PigLatin.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //This method will translate text in the input textbox as the user types
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Check that both boxes have been instantiated and input contains text
            if (txtInput != null && txtResult != null && txtInput.Text.Length > 0)
            {
                txtResult.Text = Translator.Translator.convertToLatin(txtInput.Text);
            }
        }

        //Calls the clear text method when clear button pressed
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearText();
        }

        //Sets text of both text boxes blank
        private void ClearText()
        {
            txtInput.Text = "";
            txtResult.Text = "";
        }

        //Allows user to open a file of text to be translated
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            //Initiate OpenFileDialog object
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;

            //Declar stream variable
            Stream stream = null;
            
            //Check that a file was selected
            if ((bool)openFileDialog.ShowDialog())
            {
                try
                {
                    ClearText();

                    //Check that stream is not null
                    if ((stream = openFileDialog.OpenFile()) != null)
                    {
                        using (StreamReader sr = new StreamReader(stream))
                        {
                            //Initiate list to store translated lines
                            List<string> latin = new List<string>();

                            //Continue reading and translating line until the end of the file is reached
                            while (!sr.EndOfStream)
                            {
                                string line = sr.ReadLine();
                                txtInput.Text += line + Environment.NewLine;
                                latin.Add(Translator.Translator.convertToLatin(line));
                            }

                            //Use list of translated lines to update the result textbox
                            txtResult.Text = "";
                            for (int i = 0; i < latin.Count(); i++)
                            {
                                txtResult.Text += latin[i] + Environment.NewLine; 
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Show message if there is an error while opening the file
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

        }
    }
}
