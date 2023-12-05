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
        public Form1()
        {
            InitializeComponent();

        }
        [DllImport("user32.dll")]// подключили библиотеку DLL
        //DLL - библиотека динамической компоновки DLL
        public static extern int MessageBox(IntPtr hWnd, string Text, string Caption, int Options);

        [DllImport("user32.dll")]
        public static extern bool SetWindowTextA(IntPtr hWnd, string Caption);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowA(string ClassName, string WindowName);

        [DllImport("user32.dll")]
        //здесь мы сделали прямое преобразование внутри функции за счет MarshalAl(UnNamedType)
        public static extern IntPtr SendMessage (IntPtr hWnd, uint Msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string LParam);
        // 
        const uint WM_SETTEXT = 0x0C;

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

    }
}
