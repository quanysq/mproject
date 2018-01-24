using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WX.Hook.Service
{
    public sealed class CommonUtil
    {
        /// <summary>
        /// 转换类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T GetObjTranNull<T>(object obj)
        {
            if (obj == null) return default(T);
            if (obj is T) return (T)obj;
            try
            {
                return (T)Convert.ChangeType(obj, typeof(T));
            }
            catch
            {
                return default(T);
            }
        }
    }
}
