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

using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Http;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;


namespace Overwatch_wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            while (count <= 2)
            {
                Microsoft.VisualBasic.Interaction.AppActivate("Overwatch");
                SendKeys.SendWait("{PRTSC}");
                System.Diagnostics.Process[] processList = System.Diagnostics.Process.GetProcesses();
                if (System.Windows.Clipboard.ContainsImage())
                {

                    string lngStr = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"tesseract-ocr\\");

                    //言語
                    string langPath = "eng";

                    //画像ファイルを取得

                    var img = (Bitmap)System.Windows.Forms.Clipboard.GetImage();
  
                    //フォーマットの変換
                    var bitmap24 = new Bitmap(img.Width, img.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    using (var gr = Graphics.FromImage(bitmap24))
                    {
                        gr.DrawImage(img, new Rectangle(0, 0, bitmap24.Width, bitmap24.Height));
                    }

                    using (var tesseract = new Tesseract.TesseractEngine(lngStr, langPath))
                    {
                        // 文字を指定
                        tesseract.SetVariable("SEARCH", "1");

                        // OCRの実行
                        Tesseract.Page page = tesseract.Process(bitmap24);

                        if (page.GetText().Contains("SEARCH") || page.GetText().Contains("TIME") || page.GetText().Contains("ELAPSED") || page.GetText().Contains("LEAGUE") || page.GetText().Contains("HIGHLIGHTS") || page.GetText().Contains("SOCIAL") || page.GetText().Contains("EXIT"))
                        {
                            count = 1;
                        }
                        else
                        {
                            if (true)
                            {

                                // ●プロセス起動情報の構築
                                DateTime dt = DateTime.Now;

                                string result = dt.ToString("yyyy/MM/dd HH:mm:ss");
                                string lnk = "https://maker.ifttt.com/trigger/match/with/key/xxxxxxxx/?value1=" + result;

                                System.Diagnostics.Process.Start(lnk);
                                count = 3;
                                Environment.Exit(0);
                            }
                        }
                    }
                }
                Thread.Sleep(50000);
            }
            Environment.Exit(0);

        }

      
    }  
    
}