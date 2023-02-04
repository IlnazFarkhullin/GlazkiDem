using Microsoft.Win32;
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
using System.Windows.Shapes;
using System.IO;
using GlazkiSave.Classes;
using GlazkiSave.DataBaseModel;

namespace GlazkiSave.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddAgentWindow.xaml
    /// </summary>
    public partial class AddAgentWindow : Window
    {
        public AddAgentWindow()
        {
            InitializeComponent();
            AgentTypeCmb.ItemsSource = ConnectingClass.connecting.AgentType.ToList();
        }
        private void LogoBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files|*.bmp;*.jpg;*.png|All files|*.*";
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == true)
            {
                File.ReadAllBytes(openFileDialog.FileName);
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(openFileDialog.FileName);
                image.EndInit();
                LogoFrame.Source = image;
            }
        }

       

        private void AddAgentBtn_Click(object sender, RoutedEventArgs e)
        {
            var a = AgentTypeCmb.SelectedItem as AgentType;
            Agent agent = new Agent()
            {
                Title = TitleTxt.Text,
                AgentTypeID = ((AgentType)AgentTypeCmb.SelectedItem).ID,
                Address = AddressTxt.Text,
                INN = INNTxt.Text,
                KPP = KPPTxt.Text,
                DirectorName = DirectorNameTxt.Text,
                Phone = PhoneTxt.Text,
                Email = EmailTxt.Text,
                Priority = Convert.ToInt32(PriorTxt.Text)

            };
            ConnectingClass.connecting.Agent.Add(agent);
            ConnectingClass.connecting.SaveChanges();
            MessageBox.Show("Запись добавлена");
        }
    }
}
