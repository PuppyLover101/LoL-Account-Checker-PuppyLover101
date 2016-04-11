using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace LoLAccountChecker.Classes
{
    static class Common
    {
        public static void CopyCombo(DataGrid accountsDataGrid)
        {
            if (accountsDataGrid.SelectedItems.Count == 0)
            {
                return;
            }

            StringBuilder sb = new StringBuilder();

            foreach (Account account in accountsDataGrid.SelectedItems)
            {
                sb.Append(string.Format("{0}:{1}", account.Username, account.Password));
                sb.Append(Environment.NewLine);
            }

            Clipboard.SetDataObject(sb.ToString());
        }

        private static Key lastKey;
        private static int lastFoundIndex = 0;

        public static void AccountsDataGrid_SearchByLetterKey(object sender, KeyEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;

            Func<object, bool> itemCompareMethod = (item) =>
            {
                Account account = item as Account;

                if (account.Username.StartsWith(e.Key.ToString(), true, CultureInfo.CurrentCulture))
                {
                    return true;
                }

                return false;
            };

            DataGridSearchByLetterKey(dataGrid, e.Key, itemCompareMethod);
        }

        public static void DataGridSearchByLetterKey(DataGrid dataGrid, Key key, Func<object, bool> itemCompareMethod)
        {
            if ((dataGrid.Items.Count == 0) || !(key >= Key.A && key <= Key.Z))
            {
                return;
            }

            if ((lastKey != key) || (lastFoundIndex == dataGrid.Items.Count - 1))
            {
                lastFoundIndex = 0;
            }

            lastKey = key;

            lastFoundIndex = FindDataGridRecordWithinRange(dataGrid, lastFoundIndex, dataGrid.Items.Count, itemCompareMethod);

            if (lastFoundIndex == -1)
            {
                lastFoundIndex = FindDataGridRecordWithinRange(dataGrid, 0, dataGrid.Items.Count, itemCompareMethod);
            }

            if (lastFoundIndex != -1)
            {
                dataGrid.ScrollIntoView(dataGrid.Items[lastFoundIndex]);
                dataGrid.SelectedIndex = lastFoundIndex;
            }

            if (lastFoundIndex == -1)
            {
                lastFoundIndex = 0;
                dataGrid.SelectedItem = null;
            }
        }

        private static int FindDataGridRecordWithinRange(DataGrid dataGrid, int min, int max, Func<object, bool> itemCompareMethod)
        {
            for (int i = min; i < max; i++)
            {
                if (dataGrid.SelectedIndex == i)
                {
                    continue;
                }

                if (itemCompareMethod(dataGrid.Items[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        public static IEnumerable<DependencyObject> GetChildObjects(DependencyObject parent)
        {
            if (parent == null)
            {
                yield break;
            }
            if (parent is ContentElement || parent is FrameworkElement)
            {
                foreach (object obj in LogicalTreeHelper.GetChildren(parent))
                {
                    var depObj = obj as DependencyObject;
                    if (depObj != null)
                    {
                        yield return (DependencyObject)obj;
                    }
                }
            }
            else
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
                {
                    yield return VisualTreeHelper.GetChild(parent, i);
                }
            }
        }

        public static IEnumerable<T> FindChildren<T>(DependencyObject parent) where T : DependencyObject
        {
            if(parent == null)
            {
                yield break;
            }
            foreach (DependencyObject child in GetChildObjects(parent))
            {
                if (child != null && child is T)
                {
                    yield return (T)child;
                }
                foreach (T descendant in FindChildren<T>(child))
                {
                    yield return descendant;
                }
            }
        }
    }
}
