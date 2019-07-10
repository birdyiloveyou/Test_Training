using System;

namespace XmasChecker
{
    public class XmasChecker
    {
        protected DateTime today = DateTime.Today;

        public bool IsTodayXmas()
        {
            if (today.Month == 12 && today.Day == 25)
            {
                return true;
            }
            return false;
        }
    }
}