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
using System.IO;

namespace TestProject
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "All files (*.*)|*.*|Text file (*.txt)|*.txt|Log file (*.log)|*.log";
            dialog.FilterIndex = 1;

            if (dialog.ShowDialog() == true)
            {
                string filename = dialog.FileName;
                FileInfo file = new FileInfo(filename);
                long size = file.Length;
                textBlock.Text = filename + " | size: " + size;

                int n = 100;
                int count = 0;
                string content;
                byte[] buffer = new byte[1];
                textBox.Text = "";

                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    fs.Seek(0, SeekOrigin.End);

                    while (count < n + 1)
                    {
                        fs.Seek(-1, SeekOrigin.Current);
                        fs.Read(buffer, 0, 1);
                        if (buffer[0] == '\n')
                        {
                            count++;
                        }
                        fs.Seek(-1, SeekOrigin.Current);
                    }
                    fs.Seek(1, SeekOrigin.Current);

                    using (StreamReader sr = new StreamReader(fs))
                    {
                        content = sr.ReadToEnd();
                        string[] contentStr = content.Split('\n'); 

                        for (int i = n; i >= 0; i--)
                        {
                            textBox.Text += contentStr[i] + "\n";
                        }
                    }
                }
            }
        }
    }
}
