using System;
using System.Collections.Generic;
using System.Text;

namespace SecondAttempt
{
    public enum TypeOperation
    {
        On = 1,
        Off,
        StartUse,
        StopUse
    }
    public class Operation
    {
        public Clients cl;
        public TypeOperation to;
        public Computer comp;
        public DateTime timeop;
        public string Message;
        public Operation() { comp = null; cl = null; timeop = DateTime.Now; }
        public override string ToString()
        {
            return string.Format("{0} {1} comp={2} {3} {4}", cl, to, comp.IDComp, timeop, Message);
        }
    }
}
