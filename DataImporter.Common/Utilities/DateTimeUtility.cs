using System;

namespace DataImporter.Common.Utilities
{
    public class DateTimeUtility : IDateTimeUtility
    {

        public DateTimeUtility()
        {

        }

        public DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}
