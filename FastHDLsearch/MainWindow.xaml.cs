using FastHDLsearch.ViewModels;
using Microsoft.WindowsAPICodePack.Dialogs;
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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Configuration;
using System.Windows.Threading;
using System.Threading;
using System.Runtime.InteropServices;

namespace FastHDLsearch
{
    /// <summary>
    /// Window1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        Button[] tabbuttons;

        #region EVERYTHING_API

        const int EVERYTHING_OK = 0;
        const int EVERYTHING_ERROR_MEMORY = 1;
        const int EVERYTHING_ERROR_IPC = 2;
        const int EVERYTHING_ERROR_REGISTERCLASSEX = 3;
        const int EVERYTHING_ERROR_CREATEWINDOW = 4;
        const int EVERYTHING_ERROR_CREATETHREAD = 5;
        const int EVERYTHING_ERROR_INVALIDINDEX = 6;
        const int EVERYTHING_ERROR_INVALIDCALL = 7;

        const int EVERYTHING_REQUEST_FILE_NAME = 0x00000001;
        const int EVERYTHING_REQUEST_PATH = 0x00000002;
        const int EVERYTHING_REQUEST_FULL_PATH_AND_FILE_NAME = 0x00000004;
        const int EVERYTHING_REQUEST_EXTENSION = 0x00000008;
        const int EVERYTHING_REQUEST_SIZE = 0x00000010;
        const int EVERYTHING_REQUEST_DATE_CREATED = 0x00000020;
        const int EVERYTHING_REQUEST_DATE_MODIFIED = 0x00000040;
        const int EVERYTHING_REQUEST_DATE_ACCESSED = 0x00000080;
        const int EVERYTHING_REQUEST_ATTRIBUTES = 0x00000100;
        const int EVERYTHING_REQUEST_FILE_LIST_FILE_NAME = 0x00000200;
        const int EVERYTHING_REQUEST_RUN_COUNT = 0x00000400;
        const int EVERYTHING_REQUEST_DATE_RUN = 0x00000800;
        const int EVERYTHING_REQUEST_DATE_RECENTLY_CHANGED = 0x00001000;
        const int EVERYTHING_REQUEST_HIGHLIGHTED_FILE_NAME = 0x00002000;
        const int EVERYTHING_REQUEST_HIGHLIGHTED_PATH = 0x00004000;
        const int EVERYTHING_REQUEST_HIGHLIGHTED_FULL_PATH_AND_FILE_NAME = 0x00008000;

        const int EVERYTHING_SORT_NAME_ASCENDING = 1;
        const int EVERYTHING_SORT_NAME_DESCENDING = 2;
        const int EVERYTHING_SORT_PATH_ASCENDING = 3;
        const int EVERYTHING_SORT_PATH_DESCENDING = 4;
        const int EVERYTHING_SORT_SIZE_ASCENDING = 5;
        const int EVERYTHING_SORT_SIZE_DESCENDING = 6;
        const int EVERYTHING_SORT_EXTENSION_ASCENDING = 7;
        const int EVERYTHING_SORT_EXTENSION_DESCENDING = 8;
        const int EVERYTHING_SORT_TYPE_NAME_ASCENDING = 9;
        const int EVERYTHING_SORT_TYPE_NAME_DESCENDING = 10;
        const int EVERYTHING_SORT_DATE_CREATED_ASCENDING = 11;
        const int EVERYTHING_SORT_DATE_CREATED_DESCENDING = 12;
        const int EVERYTHING_SORT_DATE_MODIFIED_ASCENDING = 13;
        const int EVERYTHING_SORT_DATE_MODIFIED_DESCENDING = 14;
        const int EVERYTHING_SORT_ATTRIBUTES_ASCENDING = 15;
        const int EVERYTHING_SORT_ATTRIBUTES_DESCENDING = 16;
        const int EVERYTHING_SORT_FILE_LIST_FILENAME_ASCENDING = 17;
        const int EVERYTHING_SORT_FILE_LIST_FILENAME_DESCENDING = 18;
        const int EVERYTHING_SORT_RUN_COUNT_ASCENDING = 19;
        const int EVERYTHING_SORT_RUN_COUNT_DESCENDING = 20;
        const int EVERYTHING_SORT_DATE_RECENTLY_CHANGED_ASCENDING = 21;
        const int EVERYTHING_SORT_DATE_RECENTLY_CHANGED_DESCENDING = 22;
        const int EVERYTHING_SORT_DATE_ACCESSED_ASCENDING = 23;
        const int EVERYTHING_SORT_DATE_ACCESSED_DESCENDING = 24;
        const int EVERYTHING_SORT_DATE_RUN_ASCENDING = 25;
        const int EVERYTHING_SORT_DATE_RUN_DESCENDING = 26;

        const int EVERYTHING_TARGET_MACHINE_X86 = 1;
        const int EVERYTHING_TARGET_MACHINE_X64 = 2;
        const int EVERYTHING_TARGET_MACHINE_ARM = 3;

        [DllImport("Everything64.dll", CharSet = CharSet.Unicode)]
        public static extern UInt32 Everything_SetSearchW(string lpSearchString);
        [DllImport("Everything64.dll")]
        public static extern void Everything_SetMatchPath(bool bEnable);
        [DllImport("Everything64.dll")]
        public static extern void Everything_SetMatchCase(bool bEnable);
        [DllImport("Everything64.dll")]
        public static extern void Everything_SetMatchWholeWord(bool bEnable);
        [DllImport("Everything64.dll")]
        public static extern void Everything_SetRegex(bool bEnable);
        [DllImport("Everything64.dll")]
        public static extern void Everything_SetMax(UInt32 dwMax);
        [DllImport("Everything64.dll")]
        public static extern void Everything_SetOffset(UInt32 dwOffset);

        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetMatchPath();
        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetMatchCase();
        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetMatchWholeWord();
        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetRegex();
        [DllImport("Everything64.dll")]
        public static extern UInt32 Everything_GetMax();
        [DllImport("Everything64.dll")]
        public static extern UInt32 Everything_GetOffset();
        [DllImport("Everything64.dll")]
        public static extern IntPtr Everything_GetSearchW();
        [DllImport("Everything64.dll")]
        public static extern UInt32 Everything_GetLastError();

        [DllImport("Everything64.dll")]
        public static extern bool Everything_QueryW(bool bWait);

        [DllImport("Everything64.dll")]
        public static extern void Everything_SortResultsByPath();

        [DllImport("Everything64.dll")]
        public static extern UInt32 Everything_GetNumFileResults();
        [DllImport("Everything64.dll")]
        public static extern UInt32 Everything_GetNumFolderResults();
        [DllImport("Everything64.dll")]
        public static extern UInt32 Everything_GetNumResults();
        [DllImport("Everything64.dll")]
        public static extern UInt32 Everything_GetTotFileResults();
        [DllImport("Everything64.dll")]
        public static extern UInt32 Everything_GetTotFolderResults();
        [DllImport("Everything64.dll")]
        public static extern UInt32 Everything_GetTotResults();
        [DllImport("Everything64.dll")]
        public static extern bool Everything_IsVolumeResult(UInt32 nIndex);
        [DllImport("Everything64.dll")]
        public static extern bool Everything_IsFolderResult(UInt32 nIndex);
        [DllImport("Everything64.dll")]
        public static extern bool Everything_IsFileResult(UInt32 nIndex);
        [DllImport("Everything64.dll", CharSet = CharSet.Unicode)]
        public static extern void Everything_GetResultFullPathName(UInt32 nIndex, StringBuilder lpString, UInt32 nMaxCount);
        [DllImport("Everything64.dll")]
        public static extern void Everything_Reset();

        [DllImport("Everything64.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr Everything_GetResultFileName(UInt32 nIndex);

        // Everything 1.4
        [DllImport("Everything64.dll")]
        public static extern void Everything_SetSort(UInt32 dwSortType);
        [DllImport("Everything64.dll")]
        public static extern UInt32 Everything_GetSort();
        [DllImport("Everything64.dll")]
        public static extern UInt32 Everything_GetResultListSort();
        [DllImport("Everything64.dll")]
        public static extern void Everything_SetRequestFlags(UInt32 dwRequestFlags);
        [DllImport("Everything64.dll")]
        public static extern UInt32 Everything_GetRequestFlags();
        [DllImport("Everything64.dll")]
        public static extern UInt32 Everything_GetResultListRequestFlags();
        [DllImport("Everything64.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr Everything_GetResultExtension(UInt32 nIndex);
        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetResultSize(UInt32 nIndex, out long lpFileSize);
        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetResultDateCreated(UInt32 nIndex, out long lpFileTime);
        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetResultDateModified(UInt32 nIndex, out long lpFileTime);
        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetResultDateAccessed(UInt32 nIndex, out long lpFileTime);
        [DllImport("Everything64.dll")]
        public static extern UInt32 Everything_GetResultAttributes(UInt32 nIndex);
        [DllImport("Everything64.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr Everything_GetResultFileListFileName(UInt32 nIndex);
        [DllImport("Everything64.dll")]
        public static extern UInt32 Everything_GetResultRunCount(UInt32 nIndex);
        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetResultDateRun(UInt32 nIndex, out long lpFileTime);
        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetResultDateRecentlyChanged(UInt32 nIndex, out long lpFileTime);
        [DllImport("Everything64.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr Everything_GetResultHighlightedFileName(UInt32 nIndex);
        [DllImport("Everything64.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr Everything_GetResultHighlightedPath(UInt32 nIndex);
        [DllImport("Everything64.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr Everything_GetResultHighlightedFullPathAndFileName(UInt32 nIndex);
        [DllImport("Everything64.dll")]
        public static extern UInt32 Everything_GetRunCountFromFileName(string lpFileName);
        [DllImport("Everything64.dll")]
        public static extern bool Everything_SetRunCountFromFileName(string lpFileName, UInt32 dwRunCount);
        [DllImport("Everything64.dll")]
        public static extern UInt32 Everything_IncRunCountFromFileName(string lpFileName);

        #endregion


        ////////////////////////////////


        private BackgroundWorker myThread = new BackgroundWorker()
        {
            WorkerReportsProgress = true,
            WorkerSupportsCancellation = true
        };
        private struct customWorkerArgument
        {

            public bool isWrapping { get; set; } = false;
            public int test { get; set; } = 0;

            public customWorkerArgument()
            {
            }
        }
        string searchText = "";

        //config
        string searchPath = "";
        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        JsonManager jsonManager = new JsonManager();


        private SolidColorBrush BrushFromColorCode(string colorCode)
        {
            SolidColorBrush colorBrush = new SolidColorBrush();
            colorBrush.Color = (Color)ColorConverter.ConvertFromString(colorCode);
            return colorBrush;
        }

        public MainWindow()
        {
            InitializeComponent();
            jsonManager.Load();
            //xn_TextboxPath.Text = config.AppSettings.Settings["Path"].Value;
            xn_TextboxPath.Text = jsonManager.configfile.path;
            //xn_isSearchWrapping.IsChecked = config.AppSettings.Settings["searchwrapping"].Value == "true";
            xn_isSearchWrapping.IsChecked = jsonManager.configfile.searchwrapping;

            DataContext = new MainViewModel();
            tabbuttons = new Button[] { xn_tabbutton1, xn_tabbutton2, xn_tabbutton3 };
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            //백그라운드 워커 초기화
            //작업의 진행율이 바뀔때 ProgressChanged 이벤트 발생여부
            //작업취소 가능여부 true로 설정
            //초기화 전역으로 이동


            //백그라운드에서 실행될 콜백 이벤트 생성
            //For the performing operation in the background.   

            //해야할 작업을 실행할 메소드 정의
            myThread.DoWork += myThread_DoWork;
            //UI쪽에 진행사항을 보여주기 위해
            //WorkerReportsProgress 속성값이 true 일때만 이벤트 발생
            //myThread.ProgressChanged += myThread_ProgressChanged;
            //작업이 완료되었을 때 실행할 콜백메소드 정의  
            myThread.RunWorkerCompleted += myThread_RunWorkerCompleted;
            //작업이 중단되었을때 실행할 메소드
            myThread.Disposed += MyThread_Disposed;

        }

        private void MyThread_Disposed(object? sender, EventArgs e)
        {
            MessageBox.Show("Disposed");
            throw new NotImplementedException();
        }

        private void myThread_DoWork(object? sender, DoWorkEventArgs e)
        {
            customWorkerArgument arg = e.Argument as customWorkerArgument? ?? default;
            // request name and size
            Everything_SetRequestFlags(EVERYTHING_REQUEST_FILE_NAME | EVERYTHING_REQUEST_PATH | EVERYTHING_REQUEST_DATE_MODIFIED | EVERYTHING_REQUEST_SIZE);
            Everything_SetSort(13);

            //separate gallery numbers
            string[] separatedStrings = searchText.Split(',', StringSplitOptions.TrimEntries);

            List<string> found = new List<string>();
            List<string> need = new List<string>();

            foreach (string s in separatedStrings)
            {
                // cancellation
                if (myThread.CancellationPending) { e.Cancel = true; return; }

                string finalSearchText;
                if (arg.isWrapping)
                {
                    finalSearchText = "(" + s + ")";
                }
                else
                {
                    finalSearchText = s;
                }

                Everything_SetSearchW(finalSearchText + " " + searchPath);
                bool success = Everything_QueryW(true);
                if (success == false)
                {
                    MessageBox.Show("searchFail");
                    return;
                }

                //Debug.Write(s);
                uint resultCount = Everything_GetNumResults();
                if (resultCount >= 1)
                {
                    //Debug.WriteLine(" is exist. count: " + resultCount);
                    found.Add(s);
                    this.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
            (ThreadStart)delegate ()
            {
                xn_foundList.Text += s + " is exist. count: " + resultCount + "\n";
            }
            );

                }
                else if (resultCount == 0)
                {
                    //Debug.WriteLine(" need to download.");
                    need.Add(s);
                    this.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
            (ThreadStart)delegate ()
            {
                xn_NeedList.Text += s + ", ";
            }
            );

                }
                else
                {
                    Debug.WriteLine(" something error. this can not be occur");
                }

            }


            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                        (ThreadStart)delegate ()
                        {
                            xn_NeedList.Text = xn_NeedList.Text.TrimEnd(',', ' ');
                        }
                        );
        }

        //작업완료
        private void myThread_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled) { xn_status.Text = "작업 취소..."; xn_status.Foreground = BrushFromColorCode("#E69705"); }

            else if (e.Error != null) { xn_status.Text = "에러발생..."; xn_status.Foreground = BrushFromColorCode("#D92B04"); }

            else
            {
                xn_status.Text = "작업 완료";
                xn_status.Foreground = BrushFromColorCode("#038C73");
            }

            //xn_BTsearch.Content = "Search";
            xn_BTsearch.IsEnabled = true;
            xn_BTsearch.Visibility = Visibility.Visible;

        }


        private void BTsearch_Click(object sender, RoutedEventArgs e)
        {
            //Debug.WriteLine("Search button clicked");
            //xn_BTsearch.Content = "...";
            xn_BTsearch.IsEnabled = false;
            xn_BTsearch.Visibility = Visibility.Collapsed;
            xn_status.Text = "작업중...";
            xn_status.Foreground = BrushFromColorCode("#0487D9");

            //initializing
            xn_foundList.Text = string.Empty;
            xn_NeedList.Text = string.Empty;
            searchPath = xn_TextboxPath.Text;
            searchText = xn_TextboxSearch.Text;

            customWorkerArgument arg = new customWorkerArgument();
            arg.isWrapping = xn_isSearchWrapping.IsChecked ?? false;
            arg.test = 10;

            myThread.RunWorkerAsync(arg);

            StringBuilder sb = new StringBuilder();


        }

        private void BTcancell_Click(object sender, RoutedEventArgs e)
        {
            myThread.CancelAsync();
        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //BTsearch_Click(this, new RoutedEventArgs());
            }
        }

        private void BTcopyit_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(xn_NeedList.Text);
            xn_BTcopyit.Content = "copied !";
            DispatcherTimer timer = new DispatcherTimer();    //객체생성

            timer.Interval = TimeSpan.FromMilliseconds(1000);    //시간간격 설정
            timer.Tick += new EventHandler(
                delegate (object? sender, EventArgs e)
                {
                    xn_BTcopyit.Content = "copy";
                    timer.Stop();
                }
            );          //이벤트 추가
            if (timer.IsEnabled) timer.Stop();
            timer.Start();                                       //타이머 시작. 종료는 timer.Stop(); 으로 한다


        }

        private void BTpath_Click(object sender, RoutedEventArgs e)
        {
            // CommonOpenFileDialog 클래스 생성
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            // 처음 보여줄 폴더 설정(안해도 됨)
            //dialog.InitialDirectory = "";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                xn_TextboxPath.Text = dialog.FileName; // 테스트용, 폴더 선택이 완료되면 선택된 폴더를 label에 출력
            }


        }

        private void TextboxPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            //config.AppSettings.Settings["path"].Value = xn_TextboxPath.Text;
            //config.Save(ConfigurationSaveMode.Modified);
            //ConfigurationManager.RefreshSection("appSettings");
            jsonManager.configfile.path = xn_TextboxPath.Text;
            jsonManager.Save();
        }


        private void isSearchWrapping_Clicked(object sender, RoutedEventArgs e)
        {
            //config.AppSettings.Settings["searchwrapping"].Value = xn_isSearchWrapping.IsChecked.ToString()?.ToLower();
            //config.Save(ConfigurationSaveMode.Modified);
            //ConfigurationManager.RefreshSection("appSettings");
            jsonManager.configfile.searchwrapping = xn_isSearchWrapping.IsChecked ?? true;
            jsonManager.Save();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            //xn_TabControl.SelectedIndex = 2;
        }

        private void xn_TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var btit in tabbuttons.Select((value, index) => new { value, index }))
            {
                if (btit.index == xn_TabControl.SelectedIndex)
                {
                    btit.value.IsEnabled = false;
                }
                else
                {
                    btit.value.IsEnabled = true;
                }
                
            }
        }
    }
}
