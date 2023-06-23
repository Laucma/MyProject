using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace WorkerServiceDemo
{
    public class Smart1000API
    {
        //获取错误码
        [DllImport("Smart1000DLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Smart1000GetLastError();
        //查找加密锁
        [DllImport("Smart1000DLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Smart1000Find(int AppID, int[] keyHandles, out int keyNum);
        //打开加密锁
        [DllImport("Smart1000DLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Smart1000Open(int keyHandle, int Password1, int Password2, int Password3, int Password4, ref int requestFromKey);
        //写分页数据
        [DllImport("Smart1000DLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Smart1000WritePage(int keyHandle, int page, byte[] pBuffer);
        //读分页数据
        [DllImport("Smart1000DLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Smart1000ReadPage(int keyHandle, int page, byte[] pBuffer);
        //写文件存储区
        [DllImport("Smart1000DLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Smart1000WriteFileStorage(int keyHandle, int address, int Length, Byte[] pBuffer);
        //读文件存储区
        [DllImport("Smart1000DLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Smart1000ReadFileStorage(int keyHandle, int address, int Length, Byte[] pBuffer);
        //读内存区
        [DllImport("Smart1000DLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Smart1000ReadMemory(int keyHandle, byte[] pBuffer);
        //写内存区
        [DllImport("Smart1000DLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Smart1000WriteMemory(int keyHandle, byte[] pBuffer);
        //加密锁关闭
        [DllImport("Smart1000DLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Smart1000Close(int keyHandle);

        //获取硬件ID号
        [DllImport("Smart1000DLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Smart1000GetUID(int keyHandle, byte[] sGUID);
        //获取自定义数据            
        [DllImport("Smart1000DLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Smart1000GetKeyName(int keyHandle, byte[] UserData);
        //3DES加密
        [DllImport("Smart1000DLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Smart1000TriDESEncrypt(int nKeyHandle, byte[] iv, int dataLength, byte[] pDataBuffer);
        //3DES解密
        [DllImport("Smart1000DLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Smart1000TriDESDecrypt(int nKeyHandle, byte[] iv, int dataLength, byte[] pDataBuffer);

        //二次验证
        [DllImport("Smart1000DLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Smart1000Verify(int keyHandle, int response);
    }
}
