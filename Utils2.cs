using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;

namespace LoLAccountChecker
{
    internal partial class Utils
    {
        public static object GetPropertyValue(object src, string propName)
        {
            if (propName.Contains("."))
            {
                var temp = propName.Split(new[] { '.' }, 2);
                return GetPropertyValue(GetPropertyValue(src, temp[0]), temp[1]);
            }
            var prop = src.GetType().GetProperty(propName);
            return prop?.GetValue(src, null);
        }

        public static void DataGridSearchByLetterKey(DataGrid dataGrid, Key key, Func<object, bool> itemCompareMethod)
        {
            if (dataGrid.Items.Count == 0 || Keyboard.Modifiers.HasFlag(ModifierKeys.Control) || key < Key.A || key > Key.Z)
            {
                return;
            }

            int currentIndex = dataGrid.SelectedIndex;
            int foundIndex = FindDataGridRecordWithinRange(dataGrid, currentIndex, dataGrid.Items.Count - 1, itemCompareMethod);

            if (foundIndex == -1)
            {
                foundIndex = FindDataGridRecordWithinRange(dataGrid, 0, currentIndex - 1, itemCompareMethod);
            }

            if (foundIndex > -1)
            {
                dataGrid.ScrollIntoView(dataGrid.Items[foundIndex]);
                dataGrid.SelectedIndex = foundIndex;
            }
        }

        private static int FindDataGridRecordWithinRange(DataGrid dataGrid, int min, int max, Func<object, bool> itemCompareMethod)
        {
            for (int i = min; i <= max; i++)
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

        public static void UseDefaultExtAsFilterIndex(FileDialog dialog)
        {
            var ext = "*." + dialog.DefaultExt;
            var filter = dialog.Filter;
            var filters = filter.Split('|');
            for (int i = 1; i < filters.Length; i += 2)
            {
                if (filters[i] == ext)
                {
                    dialog.FilterIndex = 1 + (i - 1) / 2;
                    return;
                }
            }
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
                var children = child as T;
                if (children != null)
                {
                    yield return children;
                }
                foreach (T descendant in FindChildren<T>(child))
                {
                    yield return descendant;
                }
            }
        }
    }

    internal static class ConcurrentQueueExtensions
    {
        public static void Clear<T>(this ConcurrentQueue<T> queue)
        {
            T item;
            while (queue.TryDequeue(out item)) { }
        }
    }

    public static class StringExtensions
    {
        public static bool Contains(this string source, string toCheck, bool bCaseInsensitive)
        {
            return source.IndexOf(toCheck, bCaseInsensitive ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture) >= 0;
        }
    }
}
