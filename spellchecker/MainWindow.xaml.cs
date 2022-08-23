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

namespace spellchecker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HashSet<string> m_linesset = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<string> lines = System.IO.File.ReadLines("words.txt");
            m_linesset = lines.ToHashSet();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            txtoutput.Text = "";
            string sinputtext = txtinput.Text;
            if (string.IsNullOrWhiteSpace(sinputtext))
            {
                txtoutput.Text = "No input word.";
                return;
            }
            if (m_linesset == null) return;

            if(m_linesset.Contains(sinputtext))
            {
                txtoutput.Text = "word found.";
                return;
            }

            string matchingwords = "";
            //Add a letter to the beginning of the word , Add a letter to the end of the word
            for (char ch = 'a'; ch < 'z'; ++ch)
            {
                string newstr1 = ch + sinputtext;
                string newstr2 = sinputtext + ch;
                if (m_linesset.Contains(newstr1))
                {
                    matchingwords += newstr1;
                    matchingwords += Environment.NewLine;
                }
                if (m_linesset.Contains(newstr2))
                {
                    matchingwords += newstr2;
                    matchingwords += Environment.NewLine;
                }
            }
            //Remove a letter from the word
            for(int i = 0; i < sinputtext.Length; ++i)
            {
                string newstr = sinputtext.Substring(0, i) + sinputtext.Substring(i + 1);
                if (m_linesset.Contains(newstr))
                {
                    matchingwords += newstr;
                    matchingwords += Environment.NewLine;
                }
            }
            //Interchange two adjacent letters in the word
            for (int i = 0; i < sinputtext.Length-1; ++i)
            {

                char[] arrstr = sinputtext.ToArray();
                char temp = arrstr[i];
                arrstr[i] = arrstr[i + 1];
                arrstr[i + 1] = temp;
                string newword = new string(arrstr);
                if (m_linesset.Contains(newword))
                {
                    matchingwords += newword;
                    matchingwords += Environment.NewLine;
                }
            }


            txtoutput.Text = matchingwords;
        }
    }
}
