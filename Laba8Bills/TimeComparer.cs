using System.Collections;

namespace Laba8Bills
{
    class TimeComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            var TransfersData1 = (TransfersData)x;
            var TransfersData2 = (TransfersData)y;
            return TransfersData1.Time.CompareTo(TransfersData2.Time);
        }
    }
}