using System;
using System.Windows.Forms;

namespace JustNotification
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string appName = "JustNotification+";
            System.Threading.Mutex mutex = new System.Threading.Mutex(false, appName);

            bool hasHandle = false;
            try
            {
                try
                {
                    hasHandle = mutex.WaitOne(0, false);
                }
                catch (System.Threading.AbandonedMutexException)
                {
                    hasHandle = true;
                }
                if (hasHandle == false)
                {
                    // ���d�N������

                    // SteamVR����̋N���ł���΃��b�Z�[�W�͕\�����Ȃ�
                    if(Array.IndexOf(Environment.GetCommandLineArgs(), "--steamvr") == -1)
                    {
                        MessageBox.Show("���ɋN������Ă��邽�߁AJustNotification+���N���ł��܂���ł����B", "�N���G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    return;
                }

                // Initialize
                Init();

                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                new MainWindow();
                Application.Run();
            }
            finally
            {
                if (hasHandle)
                {
                    mutex.ReleaseMutex();
                }
                mutex.Close();
            }

        }

        static void Init()
        {
            Notification.Init();
            SteamVR.Init();
            OverlayHandler.Init();
        }
    }
}
