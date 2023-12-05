using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

// ТЕМА: ФУНКЦИИ WINAPI

namespace Lesson_2
{
    public partial class Form1 : Form
    {
        //создали LIST, чтобы хранить список открытых окон
        List<string> windows_list = new List<string>();
        public Form1()
        {
            InitializeComponent();

        }
        [DllImport("user32.dll")]// подключили библиотеку DLL
        //DLL - библиотека динамической компоновки DLL
        public static extern int MessageBox(IntPtr hWnd, string Text, string Caption, int Options);
        // hwnd - дескриптор ОКНА
        //
        // IntPrt - тип данных для дескриптора окна ( для hwnd)

        [DllImport("user32.dll")]
        public static extern bool SetWindowTextA(IntPtr hWnd, string Caption);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowA(string ClassName, string WindowName);

        [DllImport("user32.dll")]
        //здесь мы сделали прямое преобразование внутри функции за счет MarshalAs(UnNamedType)
        public static extern IntPtr SendMessage (IntPtr hWnd, uint Msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string LParam);
        // MARSHALAS - Этот метод преобразует данные между собственными и управляемыми средами.
        const uint WM_SETTEXT = 0x0C;

     

        // метод для хранения и вывода всех активных окон ( мы его сохраняем в список wnidows_list и выводим через 
        //методы 
        delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)] //используеится для преобразования типа данных
        static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam); // 

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsWindowVisible(IntPtr hWnd);


        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox(IntPtr.Zero, "Hello World", "Test", 1);
            SetWindowTextA(this.Handle, "LetsGo");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            IntPtr hWnd = FindWindowA(null, WinName.Text);
            //   SetWindowTextA(hWnd, NewCaption.Text);
            SendMessage(hWnd, WM_SETTEXT, 0, NewCaption.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EnumWindows((hWnd, lParam) => {
                if (IsWindowVisible(hWnd) && GetWindowTextLength(hWnd) != 0)
                {
                    windows_list.Add(GetWindowText(hWnd));
                    Windows_s.Text = GetWindowText(hWnd);
                }
                return true;
            }, IntPtr.Zero);


        }
        string GetWindowText(IntPtr hWnd) // метод преобразует bool to string чтобы вывести в текс бокс
        {
            int len = GetWindowTextLength(hWnd) + 1;
            StringBuilder sb = new StringBuilder(len);
            len = GetWindowText(hWnd, sb, len);
            return sb.ToString(0, len);
        }
    }
}
