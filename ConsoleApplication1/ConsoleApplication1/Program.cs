using ConsoleApplication1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            SodaMachine sodaMachine = new SodaMachine();
            sodaMachine.Start();
        }
    }

    public class SodaMachine : ISodaMachine
    {
        public decimal money;
        public List<Soda> inventory;
        private string availableProducts;

        public SodaMachine()
        {
            this.inventory = new List<Soda>() { new Soda { Name = "coke", Nr = 5, Price = 20 }, new Soda { Name = "sprite", Nr = 3, Price = 15 }, new Soda { Name = "fanta", Nr = 3, Price = 15 } };
            availableProducts = ListAvailableProducts(this.inventory);
        }

        public SodaMachine(List<Soda> products)
        {
            this.inventory = products;
            availableProducts = ListAvailableProducts(this.inventory);
        }



        private string ListAvailableProducts(IList<Soda> inventory)
        {
            bool first = true;
            foreach (Soda product in inventory)
            {
                if (first)
                {
                    availableProducts += product.Name;
                    first = false;
                }
                else
                {
                    availableProducts += ", " + product.Name;
                }
                
            }

            return availableProducts;
        }

        public void AddProduct(Soda soda)
        {
            var product = inventory.Where(x => x.Name.Equals(soda.Name)).FirstOrDefault();
            if (product == null)
            {
                inventory.Add(soda);
            }
            else
            {
                Console.WriteLine($"error: failed to add {soda.Name}. Product already exist in machine");
            }
        }

        public void RemoveProduct(Soda soda)
        {
            var product = inventory.Where(x => x.Name.Equals(soda.Name)).FirstOrDefault();
            if (product != null)
            {
                inventory.Remove(product);
            }
            else
            {
                Console.WriteLine($"error: failed to remove {soda.Name}. Product does not exist in machine");
            }
        }

        /// <summary>
        /// This is the starter method for the machine
        /// </summary>
        public void Start()
        {
            while (true)
            {
                ListMachineFunctions();

                var input = Console.ReadLine();

                if (input.StartsWith("insert"))
                {
                    var money = int.Parse(input.Split(' ')[1]);
                    AddMoney(money);
                }
                if (input.StartsWith("order"))
                {
                    var csoda = input.Split(' ')[1];
                    Order(csoda);
                }
                if (input.StartsWith("sms order"))
                {
                    var csoda = input.Split(' ')[2];
                    SmsOrder(csoda);
                }

                if (input.Equals("recall"))
                {
                    RecallMoney();
                }
            }
        }

        public void AddMoney(decimal amount)
        {
            money += amount;
            Console.WriteLine($"Adding {amount} to credit");
        }

        private void ListMachineFunctions()
        {
            Console.WriteLine("\n\nAvailable commands:");
            Console.WriteLine("insert (money) - Money put into money slot");
            Console.WriteLine($"order ({availableProducts}) - Order from machines buttons");
            Console.WriteLine($"sms order ({availableProducts}) - Order sent by sms");
            Console.WriteLine("recall - gives money back");
            Console.WriteLine("-------");
            Console.WriteLine("Inserted money: " + money);
            Console.WriteLine("-------\n\n");
        }

        public void RecallMoney()
        {
            Console.WriteLine("Returning " + money + " to customer");
            money = 0;
        }

        public void Order(string csoda)
        {
            var product = inventory.Where(x => x.Name.Equals(csoda)).FirstOrDefault();
            if (product != null)
            {
                if (money >= product.Price && product.Nr > 0)
                {
                    Console.WriteLine($"Giving {csoda} out");
                    money -= product.Price;
                    Console.WriteLine($"Giving {money} out in change");
                    money = 0;
                    product.Nr--;
                }
                else if (product.Nr == 0)
                {
                    Console.WriteLine($"No {csoda} left");
                }
                else if (product.Price > money)
                {
                    var missingAmount = product.Price - money;
                    Console.WriteLine($"Need {missingAmount} more");
                }
            }
            else
            {
                Console.WriteLine($"No such soda");
            }
        }

        public void SmsOrder(string csoda)
        {
            var product = inventory.Where(x => x.Name.Equals(csoda)).FirstOrDefault();
            if (product != null)
            {
                if (product.Nr > 0)
                {
                    Console.WriteLine($"Giving {csoda} out");
                    product.Nr--;
                }
                else
                {
                    Console.WriteLine($"No {csoda} left");
                }
            }
            else
            {
                Console.WriteLine($"No such soda");
            }
        }
    }
}
