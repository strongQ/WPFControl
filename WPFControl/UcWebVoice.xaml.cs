using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
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

namespace UcMyVoice
{
    /// <summary>
    /// UcWebVoice.xaml 的交互逻辑
    /// </summary>
    public partial class UcWebVoice : UserControl
    {
        private static FTP Ftp = null;
        private static IFTPUser _user;
        private static Dictionary<string, string> storeDatas = new Dictionary<string, string>();

        public static IFTPUser User
        {
            get { return _user; }
            set { _user = value; }
        }

        public UcWebVoice()
        {
            InitializeComponent();

            this.btnhorn.Click += async (send, e) =>

            {
                string result = string.Empty;
                bool canDownload = true;
                if (string.IsNullOrEmpty(VoicePath)) return;
                // 从缓存中寻找
                if (storeDatas.ContainsKey(VoicePath) )
                {
                    canDownload = false;
                    result = storeDatas[VoicePath];
                    if (new FileInfo(result).Extension == ".amr") result = result.Replace(".amr", ".wav");
                    if (!File.Exists(result))
                    {
                        storeDatas.Remove(VoicePath);
                        canDownload = true;
                    }
                }
                
                if (canDownload)
                switch (AccessWay)
                {
                    case AccessEnum.HTTP:
                      result=  await AsyncReadWeb();
                        break;
                    case AccessEnum.FTP:
                      result=  await AsyncReadFTP();
                        break;
                    case AccessEnum.Local:
                        result = VoicePath;
                        break;
                    default:
                        break;
                }
                if (string.IsNullOrEmpty(result))
                {
                    ucMedia.Stop();
                    btnhorn.Background = (Brush)this.FindResource("horn0");
                }
                else
                {
                    if (!storeDatas.ContainsKey(VoicePath)) storeDatas.Add(VoicePath, result);
                    btnhorn.Background = (Brush)this.FindResource("horn1");
                   
                    ucMedia.Source = new Uri(result);
                    
                    ucMedia.Play();
                }
               
            };

            this.ucMedia.MediaEnded += ucMedia_MediaEnded;
            btnhorn.AddHandler(Button.MouseRightButtonDownEvent, new MouseButtonEventHandler(Btn_MouseRightButtonDown), true);
        }
        /// <summary>
        /// ftp下载
        /// </summary>
        /// <returns></returns>
        private async Task<string> AsyncReadFTP()
        {

            string localfile;
            string fileName = GetLocalFile(out localfile);
            if (string.IsNullOrEmpty(fileName))
            {
                if (Ftp == null)

                    Ftp = new FTP(User.GetFtpIP(), User.GetFtpUserName(), User.GetFtpPassword());
                try
                {
                    var url = new Uri(VoicePath);
                   
                   
                    string remotePath = User.GetFtpIP() + url.LocalPath.Replace("/httpServer/","");
                   
                    fileName = await Ftp.downloadFull(remotePath, localfile);
                  
                    FileInfo file=new FileInfo(fileName);
                    if (file.Extension==".amr")
                    fileName = ChangeAMR(fileName);

                }
                catch (WebException)
                {
                    return string.Empty;
                    
                }

              
            }
            return fileName;
        }

        public string ChangeAMR(string fileName)
        {
            string newfile = fileName.Replace(".amr", ".wav");
            Process p = new Process();//建立外部调用线程
              string gvCommand = " -i " + fileName + " " + newfile;
              p.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "\\ffmpeg.exe";//要调用外部程序的绝对路径
            p.StartInfo.Arguments = gvCommand;//参数(这里就是FFMPEG的参数了)
            p.StartInfo.UseShellExecute = false;//不使用操作系统外壳程序启动线程(一定为FALSE,详细的请看MSDN)
            p.StartInfo.RedirectStandardError = true;//把外部程序错误输出写到StandardError流中(这个一定要注意,FFMPEG的所有输出信息,都为错误输出流,用StandardOutput是捕获不到任何消息的...这是我耗费了2个多月得出来的经验...mencoder就是用standardOutput来捕获的)
            p.StartInfo.CreateNoWindow = true;//不创建进程窗口
          //  p.ErrorDataReceived += new DataReceivedEventHandler(Output);//外部程序(这里是FFMPEG)输出流时候产生的事件,这里是把流的处理过程转移到下面的方法中,详细请查阅MSDN
            p.Start();//启动线程
            p.BeginErrorReadLine();//开始异步读取
            p.WaitForExit();//阻塞等待进程结束
            p.Close();//关闭进程
            p.Dispose();//释放资源
            return newfile;       
        }

        private void Btn_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ucMedia.Stop();
            btnhorn.Background = (Brush)this.FindResource("horn0");
        }

        private async Task<string> AsyncReadWeb()
        {
            string localfile;
            string fileName = GetLocalFile(out localfile);
            if (string.IsNullOrEmpty(fileName))
            {
                using (var client = new System.Net.WebClient())
                {
                    if (!File.Exists(fileName))
                    {
                        try
                        {
                            await client.DownloadFileTaskAsync(VoicePath, localfile);
                        }
                        catch (WebException)
                        {
                            try
                            {
                                File.Delete(localfile);
                            }
                            catch (FileNotFoundException) { }
                           
                            return string.Empty;
                        }
                        FileInfo file = new FileInfo(localfile);
                        if (file.Extension == ".amr")
                            localfile = ChangeAMR(localfile);
                        return localfile;
                    }
                }
            }
            return fileName;
        }

        private string GetLocalFile(out string downloadLocalFile)
        {
            string path = new Uri(VoicePath).LocalPath;
            FileInfo file = new FileInfo(path);
            string saveDirectroy = AppDomain.CurrentDomain.BaseDirectory + "\\Datas";
            if (!Directory.Exists(saveDirectroy)) Directory.CreateDirectory(saveDirectroy);
            FileInfo[] files = new DirectoryInfo(saveDirectroy).GetFiles();
            var result = files.Where(x => x.Name == file.Name);
            if (result != null && result.Count() != 0)
            {
                downloadLocalFile = string.Empty;
                return result.First().FullName;
                
            }
            string filename = AppDomain.CurrentDomain.BaseDirectory + "\\Datas\\" + file.Name;
            downloadLocalFile = filename;
            return string.Empty;
        }


        void ucMedia_MediaEnded(object sender, RoutedEventArgs e)
        {
            ucMedia.Stop();
            btnhorn.Background = (Brush)this.FindResource("horn0");
        }




        public static readonly DependencyProperty VoicePathProperty = DependencyProperty.Register("VoicePath", typeof(string), typeof(UcWebVoice), new PropertyMetadata(""));
        /// <summary>
        /// 绑定的音频路径
        /// </summary>
        public string VoicePath
        {
            get { return (string)GetValue(VoicePathProperty); }
            set { SetValue(VoicePathProperty, value); }
        }

        public enum AccessEnum
        {
            HTTP,
            FTP,
            Local
        }
        public static readonly DependencyProperty AcessProperty = DependencyProperty.Register("AccessWay", typeof(AccessEnum), typeof(UcWebVoice), new PropertyMetadata(AccessEnum.Local));
        /// <summary>
        /// 音频访问方式
        /// </summary>
        public AccessEnum AccessWay
        {
            get { return (AccessEnum)GetValue(AcessProperty); }
            set { SetValue(AcessProperty, value); }
        }
    }
}
