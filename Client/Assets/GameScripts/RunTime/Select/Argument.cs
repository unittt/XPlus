using System;
using System.Collections.Generic;

namespace GameScripts.RunTime.Select
{
  
    /// <summary>
    /// 参数
    /// </summary>
    public class Argument
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public dynamic Default { get; set; }
        
        public virtual string xxxxxx()
        {
       
            return Default is null ? string.Empty : (string)Default.ToString();
        }
    }

    
    public class ArgumentXXX:Argument
    {
        public List<string> xxxxx()
        {
            return null;
        }

        public void OnSelect(string str)
        {
            Default = true;
        }
    }


    public class Tset
    {
        public Argument A;
        public ArgumentXXX B;
        public ArgumentXXX C;
        public ArgumentXXX D;
    }
}