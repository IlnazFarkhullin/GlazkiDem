using GlazkiSave.Classes;
using GlazkiSave.DataBaseModel;
using GlazkiSave.Windows;
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

namespace GlazkiSave.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            RefreshComboBox();
            RefreshPagination();
            RefreshButtons();
            SortInfo();
            CmBTypeAgent.ItemsSource = ConnectingClass.connecting.AgentType.ToList();
            LvAgents.ItemsSource = ConnectingClass.connecting.Agent.OrderBy(x => x.Title).ToList();
        }
        int pageNumber;
        private void RefreshPagination()
        {
            LvAgents.ItemsSource = null;
            if (CmBSort.Text != null)
            {
                SortInfo();

            }
            else
            {
                LvAgents.ItemsSource = ConnectingClass.connecting.Agent.OrderBy(x => x.Title).Skip(pageNumber * 10).Take(10).ToList();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            pageNumber = Convert.ToInt32(button.Content) - 1;
            RefreshPagination();
        }
        private void RefreshButtons()
        {
            WPButtons.Children.Clear();
            if (ConnectingClass.connecting.Agent.ToList().Count % 10 == 0)
            {
                for (int i = 0; i < ConnectingClass.connecting.Agent.ToList().Count / 10; i++)
                {
                    Button button = new Button();
                    button.Content = i + 1;
                    button.Click += Button_Click;
                    button.Margin = new Thickness(5);
                    button.Width = 20;
                    button.Height = 20;
                    button.FontSize = 14;
                    WPButtons.Children.Add(button);
                }
            }
            else
            {
                for (int i = 0; i < ConnectingClass.connecting.Agent.ToList().Count / 10 + 1; i++)
                {
                    Button button = new Button();
                    button.Content = i + 1;
                    button.Click += Button_Click;
                    button.Margin = new Thickness(5);
                    button.Width = 20;
                    button.Height = 20;
                    button.FontSize = 14;
                    WPButtons.Children.Add(button);
                }
            }
        }

        private void CmBTypeAgent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = CmBTypeAgent.SelectedItem as AgentType;
            LvAgents.ItemsSource = ConnectingClass.connecting.Agent.Where(z => z.ID == a.ID).ToList();

        }

        private void CmBSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortInfo();
        }

        private void BtnLeft_Click(object sender, RoutedEventArgs e)
        {
            if (pageNumber == 0)
                return;
            pageNumber--;
            RefreshPagination();
        }

        private void BtnRight_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectingClass.connecting.Agent.ToList().Count % 10 == 0)
            {
                if (pageNumber == (ConnectingClass.connecting.Agent.ToList().Count / 10) - 1)
                    return;
            }
            else
            {
                if (pageNumber == (ConnectingClass.connecting.Agent.ToList().Count / 10))
                    return;
            }
            pageNumber++;
            RefreshPagination();
        }
        private void SortInfo()
        {
            switch (CmBSort.SelectedItem)
            {
                case "От А-Я":
                    LvAgents.ItemsSource = null;
                    LvAgents.ItemsSource = ConnectingClass.connecting.Agent.OrderBy(x => x.Title).Skip(pageNumber * 10).Take(10).ToList();
                    break;
                case "От Я-А":
                    LvAgents.ItemsSource = null;
                    LvAgents.ItemsSource = ConnectingClass.connecting.Agent.OrderByDescending(x => x.Title).Skip(pageNumber * 10).Take(10).ToList();
                    break;
                case "Скидка по возростанию":
                    LvAgents.ItemsSource = null;
                    LvAgents.ItemsSource = ConnectingClass.connecting.Agent.OrderBy(x => x.Title).Skip(pageNumber * 10).Take(10).ToList();
                    break;
                case "Скидка по убыванию":
                    LvAgents.ItemsSource = null;
                    LvAgents.ItemsSource = ConnectingClass.connecting.Agent.OrderBy(x => x.Title).Skip(pageNumber * 10).Take(10).ToList();
                    break;
                case "Приоритет по возрастанию":
                    LvAgents.ItemsSource = null;
                    LvAgents.ItemsSource = ConnectingClass.connecting.Agent.OrderBy(x => x.Priority).Skip(pageNumber * 10).Take(10).ToList();
                    break;
                case "Приоритет по убыванию":
                    LvAgents.ItemsSource = null;
                    LvAgents.ItemsSource = ConnectingClass.connecting.Agent.OrderByDescending(x => x.Priority).Skip(pageNumber * 10).Take(10).ToList();
                    break;
                default:
                    LvAgents.ItemsSource = null;
                    LvAgents.ItemsSource = ConnectingClass.connecting.Agent.ToList();
                    break;
            }
        }
        private void RefreshComboBox()
        {
            CmBSort.Items.Add("От А-Я");
            CmBSort.Items.Add("От Я-А");
            CmBSort.Items.Add("Скидка по возростанию");
            CmBSort.Items.Add("Скидка по убыванию");
            CmBSort.Items.Add("Приоритет по возрастанию");
            CmBSort.Items.Add("Приоритет по убыванию");
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddAgentWindow add = new AddAgentWindow();
            add.Show();
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            LvAgents.ItemsSource = ConnectingClass.connecting.Agent.Where(z => z.Title.Contains(TxtSearch.Text)).ToList();
        }



        private void BtnDeleted_Click(object sender, RoutedEventArgs e)
        {
            var b = LvAgents.SelectedItem as Agent;
            if (b != null)
            {
                ConnectingClass.connecting.Agent.Remove(b);
                ConnectingClass.connecting.SaveChanges();
                LvAgents.ItemsSource = ConnectingClass.connecting.Agent.ToList();
                MessageBox.Show($"Запись удалена", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Для начала выберите запись!!!");
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var a = LvAgents.SelectedItem as Agent;
            if (a == null)
            {
                MessageBox.Show("Сначало выбериде запись для редактирования","Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
               var agent = ((sender as Button).DataContext as Agent);
               NavigationService.Navigate(new EditPage(agent));
            }
        }
    }
}
