﻿using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace SharpRUDP
{
    public class RUDPPacket
    {
        private static JavaScriptSerializer _js = new JavaScriptSerializer();

        [ScriptIgnore]
        public IPEndPoint Src { get; set; }
        [ScriptIgnore]
        public IPEndPoint Dst { get; set; }
        [ScriptIgnore]
        public DateTime Received { get; set; }
        [ScriptIgnore]
        public bool Confirmed { get; set; }

        public int Seq { get; set; }
        public int Id { get; set; }
        public int Qty { get; set; }
        public RUDPPacketType Type { get; set; }
        public RUDPPacketFlags Flags { get; set; }
        public byte[] Data { get; set; }
        public int[] ACK { get; set; }

        public static RUDPPacket Deserialize(byte[] header, byte[] data)
        {
            return _js.Deserialize<RUDPPacket>(Encoding.ASCII.GetString(data.Skip(header.Length).ToArray()));
        }

        public override string ToString()
        {
            return _js.Serialize(this);
        }

        public byte[] ToByteArray(byte[] header)
        {
            return header.Concat(Encoding.ASCII.GetBytes(_js.Serialize(this))).ToArray();
        }
    }
}
