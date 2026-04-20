using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.View.Core;

namespace ZdravoCorp.View.Manager.Model.Statistics
{
    public class StatisticDTO : ObservableObject
    {
        private DateTime issued;
        private int count;

        public StatisticDTO(StatisticDTO item)
        {
            this.issued = item.Issued;
            this.count = item.Count;
        }

        public StatisticDTO(DateTime issued, int count)
        {
            this.issued = issued;
            this.count = count;
        }

        public DateTime Issued
        {
            get => issued;
            set
            {
                if (value != issued)
                {
                    issued = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Count
        {
            get => count;
            set
            {
                if (value != count)
                {
                    count = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
