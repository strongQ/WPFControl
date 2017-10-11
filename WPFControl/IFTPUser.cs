using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UcMyVoice
{
    public interface IFTPUser
    {
        string GetFtpUserName();
        string GetFtpPassword();

        string GetFtpIP();
    }
}
