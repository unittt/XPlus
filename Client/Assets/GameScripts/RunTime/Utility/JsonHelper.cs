using System.Collections;

namespace GameScripts.RunTime.Utility
{
    public static class JsonHelper
    {
        public static IList GetListData(string key, IDictionary data)
        {
            if (data == null || !data.Contains(key)) return null;
            try
            {
                return (IList)(data[key]);
            }
            catch
            {
                return null;
            }
            
        }

        public static IDictionary GetDictData(string key, IDictionary data)
        {
            if (data == null || !data.Contains(key)) return null;
            try
            {
                return (IDictionary)(data[key]);
            }
            catch
            {
                return null;
            }
        }

        public static int GetIntData(string key, IDictionary data, int defaultValue = 0)
        {
            if (data == null || !data.Contains(key)) return defaultValue;
            try
            {
                return int.Parse(data[key].ToString());
            }
            catch
            {
                return defaultValue;
            }
        }

        public static long GetLongData(string key, IDictionary data, long defaultValue = 0)
        {
            if (data == null || !data.Contains(key)) return defaultValue;
            try
            {
                return long.Parse(data[key].ToString());
            }
            catch
            {
                return defaultValue;
            }
        }

        public static string GetStringData(string key, IDictionary data, string defaultValue = "")
        {
            if (data == null || !data.Contains(key)) return defaultValue;
            try
            {
                return data[key].ToString();
            }
            catch
            {
                return defaultValue;
            }
        }

        public static bool GetBoolData(string key, IDictionary data, bool defaultValue = false)
        {
            if (data == null || !data.Contains(key)) return defaultValue;
            try
            {
                return bool.Parse(data[key].ToString());
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}