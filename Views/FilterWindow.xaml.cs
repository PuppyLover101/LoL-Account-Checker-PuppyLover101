using System;
using LoLAccountChecker.Classes;
using System.Windows.Data;
using System.Linq;
using System.Globalization;

namespace LoLAccountChecker.Views
{
    public partial class SearchWindow
    {
        public SearchWindow()
        {
            InitializeComponent();

            FilterTypeComboBox.ItemsSource = new[] { "Champion Name", "Skin Name", "Username" };
            FilterTypeComboBox.SelectedIndex = 0;

            FilterTextBox.Focus();
        }

        private void SearchButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FilterTextBox.Text))
            {
                return;
            }

            CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(MainWindow.Instance.AccountsDataGrid.ItemsSource);
            cv.Filter = AccountsFilter;

            MainWindow.Instance.IsFilterActive = true;
            MainWindow.Instance.FilterButton.Content = "Clear Filter";

            Title = $"Filter - Results: {cv.Count}";
        }

        private bool AccountsFilter(object item)
        {
            Account account = item as Account;

            if (account?.State == Account.Result.Success)
            {
                switch (FilterTypeComboBox.SelectedIndex)
                {
                    case 0:
                        if (ChampsWithSkinCheckBox.IsChecked == true)
                        {
                            return account.SkinList.Any(s => s.Champion.Name.Contains(FilterTextBox.Text, true));
                        }
                        return account.ChampionList.Any(c => c.Name.Contains(FilterTextBox.Text, true));

                    case 1:
                        return account.SkinList.Any(s => s.Name.Contains(FilterTextBox.Text, true));

                    case 2:
                        return account.Username.StartsWith(FilterTextBox.Text, true, CultureInfo.CurrentCulture);
                }
            }
            return false;
        }

        private void FilterTypeComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (FilterTypeComboBox.SelectedIndex == 0)
            {
                ChampsWithSkinCheckBox.Visibility = System.Windows.Visibility.Visible;
                return;
            }
            ChampsWithSkinCheckBox.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
