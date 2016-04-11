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

            SearchTypeComboBox.ItemsSource = new string[] { "Champion Name", "Skin Name", "Username" };
            SearchTypeComboBox.SelectedIndex = 0;

            SearchTextBox.Focus();
        }

        private void SearchButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SearchTextBox.Text))
            {
                return;
            }

            CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(MainWindow.Instance.AccountsDataGrid.ItemsSource);
            cv.Filter = AccountsFilter;

            if (cv.Count > 0)
            {
                MainWindow.Instance.IsSearchActive = true;
                MainWindow.Instance.SearchButton.Content = "Clear Search";
            }

            Title = string.Format("Search - Results: {0}", cv.Count);

            if (cv.Count == 0)
            {
                cv.Filter = null;
                MainWindow.Instance.ClearSearch();
            }
        }

        private bool AccountsFilter(object item)
        {
            if (item == null)
            {
                return false;
            }

            Account account = item as Account;

            switch (SearchTypeComboBox.SelectedIndex)
            {
                case 0:
                    if (SearchChampsWithSkinCheckBox.IsChecked == true)
                    {
                        return account.SkinList.Any(s => s.Champion.Name.ToLower() == SearchTextBox.Text.ToLower());
                    }

                    return account.ChampionList.Any(c => c.Name.ToLower() == SearchTextBox.Text.ToLower());

                case 1:
                    string searchText = Utils.PrepStringForCompare(SearchTextBox.Text);

                    return account.SkinList.Any(s => Utils.PrepStringForCompare(s.Name).Contains(searchText));

                case 2:
                    return account.Username.StartsWith(SearchTextBox.Text, true, CultureInfo.CurrentCulture);

                default:
                    break;
            }

            return false;
        }

        private void SearchTypeComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (SearchTypeComboBox.SelectedIndex == 0)
            {
                SearchChampsWithSkinCheckBox.Visibility = System.Windows.Visibility.Visible;
                return;
            }

            SearchChampsWithSkinCheckBox.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
