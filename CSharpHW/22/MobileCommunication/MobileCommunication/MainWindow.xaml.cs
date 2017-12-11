using MobileCommunication.Model;
using MobileCommunication.Models;
using System.Data.Entity;
using System.Windows;

namespace MobileCommunication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MobileCommunicationContext dbContext;
        public MainWindow()
        {
            InitializeComponent();

            dbContext = new MobileCommunicationContext();
            dbContext.Phones.Load(); // загружаем данные
            //dbContext.Accounts.Load();
            //dbContext.AddressBook.Load();
            //dbContext.MobileOperator.Load();
            //dbContext.MobileAccounts.Load();

            phonesGrid.ItemsSource = dbContext.Phones.Local.ToBindingList(); // устанавливаем привязку к кэшу
            //phonesGrid.ItemsSource = dbContext.Accounts.Local.ToBindingList(); // устанавливаем привязку к кэшу
            //phonesGrid.ItemsSource = dbContext.AddressBook.Local.ToBindingList(); // устанавливаем привязку к кэшу
            //phonesGrid.ItemsSource = dbContext.MobileOperator.Local.ToBindingList(); // устанавливаем привязку к кэшу
            //phonesGrid.ItemsSource = dbContext.MobileAccounts.Local.ToBindingList(); // устанавливаем привязку к кэшу

            Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            dbContext.Dispose();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            dbContext.SaveChanges();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (phonesGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < phonesGrid.SelectedItems.Count; i++)
                {
                    if (phonesGrid.SelectedItems[i] is Phone phone)
                    {
                        dbContext.Phones.Remove(phone);
                    }
                }
            }
            dbContext.SaveChanges();
        }
    }
}
