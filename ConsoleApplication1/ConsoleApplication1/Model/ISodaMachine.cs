using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Model
{
    public interface ISodaMachine
    {
        void Start();
        void AddMoney(decimal amount);
        void AddProduct(Soda soda);
        void RemoveProduct(Soda soda);
        void RecallMoney();
        void Order(string productName);
        void SmsOrder(string productName);
    }
}
