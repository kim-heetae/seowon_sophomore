using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication2
{
    public class Receipt
    {
        public string 메뉴
        {
            get;
            set;
        }
        public int 수량
        {
            get;
            set;
        }
        public int 합계금액
        {
            get;
            set;
        }
        public Receipt(string item, int q, int t)
        {
            메뉴 = item;
            수량 = q;
            합계금액 = t;
        }
    }
}
