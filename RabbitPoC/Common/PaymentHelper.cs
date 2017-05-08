using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Common
{
    public static class PaymentHelper
    {
        public static List<Payment> GetDummyPayments()
        {
            List<Payment> ret = new List<Payment>();

            Payment payment1 = new Payment { AmountToPay = 25.0m, CardNumber = "1234123412341234", Name = "Manu Ginobili" };
            Payment payment2 = new Payment { AmountToPay = 5.0m, CardNumber = "1234123412341234", Name = "Manu Ginobili" };
            Payment payment3 = new Payment { AmountToPay = 2.0m, CardNumber = "1234123412341234", Name = "Manu Ginobili" };
            Payment payment4 = new Payment { AmountToPay = 17.0m, CardNumber = "1234123412341234", Name = "Manu Ginobili" };
            Payment payment5 = new Payment { AmountToPay = 300.0m, CardNumber = "1234123412341234", Name = "Manu Ginobili" };
            Payment payment6 = new Payment { AmountToPay = 350.0m, CardNumber = "1234123412341234", Name = "Manu Ginobili" };
            Payment payment7 = new Payment { AmountToPay = 295.0m, CardNumber = "1234123412341234", Name = "Manu Ginobili" };
            Payment payment8 = new Payment { AmountToPay = 5625.0m, CardNumber = "1234123412341234", Name = "Manu Ginobili" };
            Payment payment9 = new Payment { AmountToPay = 5.0m, CardNumber = "1234123412341234", Name = "Manu Ginobili" };
            Payment payment10 = new Payment { AmountToPay = 12.0m, CardNumber = "1234123412341234", Name = "Manu Ginobili" };

            ret.Add(payment1);
            ret.Add(payment2);
            ret.Add(payment3);
            ret.Add(payment4);
            ret.Add(payment5);
            ret.Add(payment6);
            ret.Add(payment7);
            ret.Add(payment8);
            ret.Add(payment9);
            ret.Add(payment10);

            return ret;
        }
    }
}
