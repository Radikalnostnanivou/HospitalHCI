using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.View.Core;

namespace ZdravoCorp.Exceptions
{
    public class LocalisedException : Exception
    {
        public LocalisedException(string message) : base(LocalisedMessage(message))
        {

        }

        private static string LocalisedMessage(string message)
        {
            return TranslationSource.Instance[message];
        }
    }
}
