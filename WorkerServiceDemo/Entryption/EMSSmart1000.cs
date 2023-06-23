using System;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;

namespace WorkerServiceDemo
{
    public class EMSSmart1000
    {
        long Rtn = 1;//Rtn函数方法调用后接收返回值，如果出现错误则用GETLASTERROR获取错误码
        long[] keyHandles;

        public bool GetUID(ref string strUID, ref string strError)
        {
            //if (FindSmart1000(ref strError))
            //{
            //    //获取硬件ID号
            //    Rtn = Smart1000App.Smart1000GetUID(keyHandles[0], ref strUID);
            //    if (0 != Rtn)
            //    {
            //        strError = String.Format("获取U-Key失败!,Errorcode： {0}", Smart1000App.Smart1000GetLastError());
            //    }
            //}
            //else
            //{
            //    return false;
            //}
            return true;
        }

        public bool FindSmart1000(ref string strError)
        {
            //if (keyHandles == null || keyHandles.Length == 0)
            //{
            //    //加密锁查找
            //    long keyNumber = 0;
            //    uint appID = 0xFFFFFFFF;
            //    Rtn = Smart1000App.Smart1000Find(appID, out keyHandles, ref keyNumber);
            //    if (Rtn != 0)
            //    {
            //        strError = String.Format("查找U-Key失败,Errorcode： = {0}", Smart1000App.Smart1000GetLastError());
            //        return false;
            //    }
            //}
            return true;
        }

        private bool IsOpen = true;
        public bool OpenSmart1000(ref string strError)
        {
            //if (!IsOpen)
            //{
            //    if (FindSmart1000(ref strError))
            //    {
            //        //打开第一只加密锁
            //        //用户密码，通过设号工具设置,以下数据是出厂默认设置 由 超级密码（admin） + 种子码（FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF）生成的十六进制数
            //        int userPin1 = Convert.ToInt32("1ADC0193", 16);
            //        int userPin2 = Convert.ToInt32("854110F2", 16);
            //        int userPin3 = Convert.ToInt32("5DAE0EBC", 16);
            //        int userPin4 = Convert.ToInt32("A89011EE", 16);
            //        int requestFromKeyA = 0;//认证返回值
            //        Rtn = Smart1000App.Smart1000Open(keyHandles[0], userPin1, userPin2, userPin3, userPin4, ref requestFromKeyA);
            //        if (0 != Rtn)
            //        {
            //            strError = String.Format("打开U-Key失败, Errorcode ： {0}", Smart1000App.Smart1000GetLastError());
            //            IsOpen = false;
            //        }
            //        else
            //        {
            //            int RequestKeyB = requestFromKeyA;
            //            Rtn = Smart1000App.Smart1000Verify(keyHandles[0], RequestKeyB);
            //            if (0 != Rtn)
            //            {
            //                strError = String.Format("U-Key校验失败,Errorcode  ： {0}", Smart1000App.Smart1000GetLastError());
            //                IsOpen = false;
            //            }
            //            else
            //            {
            //                IsOpen = true;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        IsOpen = false;
            //    }
            //}
            return IsOpen;
        }

        public bool GetUserName(ref byte[] ReadFileStorage, ref string strError)
        {
            bool IsReChecked = false;
        //rechk:
        //    if (OpenSmart1000(ref strError))
        //    {
        //        int SAddrR = 0;
        //        Rtn = Smart1000App.Smart1000ReadFileStorage(keyHandles[0], SAddrR, ReadFileStorage);
        //        if (0 != Rtn)
        //        {
        //            keyHandles = null;
        //            IsOpen = false;
        //            if (!IsReChecked)
        //            {
        //                IsReChecked = true;
        //                goto rechk;
        //            }
        //            else
        //            {
        //                strError = String.Format("读取注册信息失败,errorcode： {0}", Smart1000App.Smart1000GetLastError());
        //                return false;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        return false;
        //    }
            return true;
        }

        public void CloseUkey()
        {
            //加密锁关闭
           // Smart1000App.Smart1000Close(keyHandles[0]);
        }
    }
}
