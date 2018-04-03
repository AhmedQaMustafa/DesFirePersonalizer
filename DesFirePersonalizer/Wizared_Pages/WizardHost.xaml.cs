using System;
using System.Windows;

namespace MBG.SimpleWizard
{
    /// <summary>
    /// Interaction logic for WizardHostWindow.xaml
    /// </summary>
    public partial class WizardHost : Window
    {
        private const string VALIDATION_MESSAGE = "Current page is not valid. Please fill in required information";

        #region Properties

        public WizardPageCollection WizardPages { get; set; }
        public bool ShowFirstButton
        {
            get { return btnFirst.Visibility == Visibility.Visible; }
            set { btnFirst.Visibility = value ? Visibility.Visible : Visibility.Hidden; }
        }
        public bool ShowLastButton
        {
            get { return btnLast.Visibility == Visibility.Visible; }
            set { btnLast.Visibility = value ? Visibility.Visible : Visibility.Hidden; }
        }

        private bool navigationEnabled = true;
        public bool NavigationEnabled
        {
            get { return navigationEnabled; }
            set
            {
                btnFirst.IsEnabled = value;
                btnPrevious.IsEnabled = value;
                btnNext.IsEnabled = value;
                btnLast.IsEnabled = value;
                navigationEnabled = value;
            }
        }

        #endregion

        #region Delegates & Events

        public delegate void WizardCompletedEventHandler();
        public event WizardCompletedEventHandler WizardCompleted;

        #endregion

        #region Constructor & Window Event Handlers

        public WizardHost()
        {
            InitializeComponent();
            WizardPages = new WizardPageCollection();
            WizardPages.WizardPageLocationChanged += new WizardPageCollection.WizardPageLocationChangedEventHanlder(WizardPages_WizardPageLocationChanged);
        }

        void WizardPages_WizardPageLocationChanged(WizardPageLocationChangedEventArgs e)
        {
            LoadNextPage(e.PageIndex, e.PreviousPageIndex, true);
        }

        #endregion

        #region Private Methods

        private void NotifyWizardCompleted()
        {
            if (WizardCompleted != null)
            {
                OnWizardCompleted();
                WizardCompleted();
            }
        }
        private void OnWizardCompleted()
        {
            WizardPages.LastPage.Save();
            WizardPages.Reset();
            this.DialogResult = true;
        }

        public void UpdateNavigation()
        {
            #region Reset

            btnNext.IsEnabled = true;
            btnNext.Visibility = Visibility.Visible;

            btnLast.Content = "Last >>";
            if (ShowLastButton)
            {
                btnLast.IsEnabled = true;
            }
            else
            {
                btnLast.IsEnabled = false;
            }

            #endregion

            bool canMoveNext = WizardPages.CanMoveNext;
            bool canMovePrevious = WizardPages.CanMovePrevious;

            btnPrevious.IsEnabled = canMovePrevious;
            btnFirst.IsEnabled = canMovePrevious;

            if (canMoveNext)
            {
                btnNext.Content = "Next >";
                btnNext.IsEnabled = true;

                if (ShowLastButton)
                {
                    btnLast.IsEnabled = true;
                }
            }
            else
            {
                if (ShowLastButton)
                {
                    btnLast.Content = "Finish";
                    btnNext.Visibility = Visibility.Hidden;
                }
                else
                {
                    btnNext.Content = "Finish";
                    btnNext.Visibility = Visibility.Visible;
                }
            }
        }

        private bool CheckPageIsValid()
        {
            if (!WizardPages.CurrentPage.PageValid)
            {
                MessageBox.Show(
                    string.Concat(VALIDATION_MESSAGE, Environment.NewLine, Environment.NewLine, WizardPages.CurrentPage.ValidationMessage),
                    "Details Required",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                return false;
            }
            return true;
        }

        #endregion

        #region Public Methods

        public void LoadWizard()
        {
            WizardPages.MovePageFirst();
        }
        public void LoadNextPage(int pageIndex, int previousPageIndex, bool savePreviousPage)
        {
            if (pageIndex != -1)
            {
                dockPanel.Children.Clear();
                dockPanel.Children.Add(WizardPages[pageIndex].Content);
                if (savePreviousPage && previousPageIndex != -1)
                {
                    WizardPages[previousPageIndex].Save();
                }
                WizardPages[pageIndex].Load();
                UpdateNavigation();
            }
        }

        #endregion

        #region Event Handlers

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            //if (!CheckPageIsValid()) //Maybe doesn't matter if move back; only matters if move forward
            //{ return; }

            WizardPages.MovePageFirst();
        }
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            //if (!CheckPageIsValid()) //Maybe doesn't matter if move back; only matters if move forward
            //{ return; }

            WizardPages.MovePagePrevious();
        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckPageIsValid())
            { return; }

            if (WizardPages.CanMoveNext)
            {
                WizardPages.MovePageNext();
            }
            else
            {
                //This is the finish button and it has been clicked
                NotifyWizardCompleted();
            }
        }
        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckPageIsValid())
            { return; }

            if (WizardPages.CanMoveNext)
            {
                WizardPages.MovePageLast();
            }
            else
            {
                //This is the finish button and it has been clicked
                NotifyWizardCompleted();
            }
        }

        #endregion
    }
}