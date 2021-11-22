using ATM_Home_Work.Command;
using ATM_Home_Work.Model;
using ATM_Home_Work.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ATM_Home_Work.ViewModel
{
	class MainViewModel:BaseViewModel
	{
		public RelayCommand InsertCommand { get; set; }
		public RelayCommand LoadCommand { get; set; }
		public RelayCommand TransferCommand { get; set; }
		public Card Card { get; set; }
		public List<Card> Cards { get; set; }
		public FRepos _repo { get; set; }
        public static object obj = new object();
        public MainWindow MainWindow { get; set; }
        DispatcherTimer dispatcher = new DispatcherTimer();

        public MainViewModel(MainWindow window)
		{
			Cards = new List<Card>();
			_repo = new FRepos();
			Cards = _repo.GetAll();
			InsertCommand = new RelayCommand((sender) => {
				window.cardNumberTxtbx.Visibility = Visibility.Visible;
				window.cardlabel.Visibility = Visibility.Visible;
				window.loadBtn.Visibility = Visibility.Visible;
			});

            LoadCommand = new RelayCommand((sender) =>
            {
                foreach (var item in Cards)
                {
                    if (item.CardCode == window.cardNumberTxtbx.Text)
                    {
                        window.fullNameLbl.Content = item.UserName;
                        window.balanceLbl.Content = item.Balance;
                        Card = item;
                    }
                }
            });
            TransferCommand = new RelayCommand((sender) =>
            {
                lock (obj)
                {
                    dispatcher.Start();
                    if (decimal.Parse(Card.Balance.ToString()) >= decimal.Parse(window.moneyCountTxtbx.Text))
                    {
                        Thread.Sleep(5000);
                        Card.Balance = (decimal.Parse(Card.Balance.ToString()) - decimal.Parse(window.moneyCountTxtbx.Text)).ToString();
                        window.balanceLbl.Content = Card.Balance;
                    }
                    else
                    {
                        MessageBox.Show("Transfer Declined");
                    }
                }
            });

        }

        private void Dispatcher_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Decimal.Parse(MainWindow.moneyCountTxtbx.Text) >= 0)
                {
                    MainWindow.moneyCountTxtbx.Text = (Decimal.Parse(MainWindow.moneyCountTxtbx.Text) - 10).ToString();
                    MainWindow.moneyAnimationLbl.Content = MainWindow.moneyCountTxtbx.Text;
                }
                else
                {
                    dispatcher.Stop();
                }

            }
            catch (Exception)
            {

            }
        }
    }
}