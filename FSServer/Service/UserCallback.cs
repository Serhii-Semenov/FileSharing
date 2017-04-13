using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSServer.Service
{
    public class UserCallback
    {
         public ICallback Callback { get; private set; }

         public int Id { get; set; }

         public UserCallback(int id, ICallback callback)
         {
             Callback = callback;
             Id = id;
         }
    }
}
