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
using CefSharp;
using CefSharp.Wpf;

namespace WpfAppCEFSharpBrowser1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string MainPage = "https://google.com";
        int tabCount = 0;

        TabItem currentTabItem = null;
        ChromiumWebBrowser browser;
        ChromiumWebBrowser currentBrowserShowing = null;


        public MainWindow()
        {
            InitializeComponent();
            InitializeBrowser();
        }

        public void InitializeBrowser()
        {
            browser = new ChromiumWebBrowser();
            browser.LoadUrl($"{MainPage}");
            TabItem.Content = browser;
        }

        private void Search()
        {
            if (currentBrowserShowing != null && AddressBar.Text != string.Empty)
            {
                currentBrowserShowing.Address = "https://www.google.com/search?q=" + AddressBar.Text;
            }
        }

        private void AddressBar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //currentBrowserShowing.LoadUrl(AddressBar.Text);
                Search();
            }
        }

        private void GoBtn_Click(object sender, RoutedEventArgs e)
        {
            currentBrowserShowing.LoadUrl(AddressBar.Text);
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            currentBrowserShowing.LoadUrl(MainPage);
        }

        private void RldBtn_Click(object sender, RoutedEventArgs e)
        {
            currentBrowserShowing.Reload();
        }

        private void FrntBtn_Click(object sender, RoutedEventArgs e)
        {
            if (currentBrowserShowing.CanGoForward)
            {
                currentBrowserShowing.Forward();
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (currentBrowserShowing.CanGoBack)
            {
                currentBrowserShowing.Back();
            }
        }

        private void SettingsMenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            SettingsWindow settings = new SettingsWindow();
            settings.ShowDialog();
        }

        private void FinishedLoadingWebPage(object sender, RoutedEventArgs e)
        {


        }

        private void newTabMenuItem_Click(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            ChromiumWebBrowser browser = new ChromiumWebBrowser();
            browser.Name = "browser_" + tabCount;

            tabControl.Items.Add(tabItem);
            tabItem.Name = "tab_" + tabCount;
            tabCount++;

            tabItem.Content = browser;
            browser.Address = MainPage;
            tabItem.Header = "New Page (" + tabCount + ")";

            tabControl.SelectedItem = tabItem;

            browser.Loaded += FinishedLoadingWebPage;
            currentBrowserShowing = browser;
            currentTabItem = tabItem;


        }

        

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabControl.SelectedItem != null)
            {
                currentTabItem = tabControl.SelectedItem as TabItem;
            }

            if (currentTabItem != null)
            {
                currentBrowserShowing = currentTabItem.Content as ChromiumWebBrowser;
            }
        }

        //Unicodes(symbols )
        //← → ⟳ ⌂
    }
}
