using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace Microsoft.IHE.XDS.Common
{
    public static class BSDSysLogUDP
    {
        public static int BSDSysLogAppend(string message, string sysLogServer, int sysLogPort)
        {
            byte[] auditMessageStream;
            int response;
            UdpClient sysLogEndPoint = new UdpClient();

            auditMessageStream = Encoding.UTF8.GetBytes(message);

            sysLogEndPoint.Client.Connect(sysLogServer, sysLogPort);
            response = sysLogEndPoint.Client.Send(auditMessageStream);

            return response;
        }
    }
}
