using ModelsClassLibrary.Models.SMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Bearer.MyPrograms.SmsStrategy
{
    public static class SmsContext
    {
        public static  ISmsStrategy CreateSmsStrategy(SmsStrategyEnum smsStrategyEnum)
        {
            ISmsStrategy s=new TestSmsStrategy();

            //var task = new Task(() =>
            //    {

                    switch (smsStrategyEnum)
                    {
                        case SmsStrategyEnum.MOBILINK:
                        case SmsStrategyEnum.TELENOR:
                        
                        case SmsStrategyEnum.TESTER:
                            s = new TestSmsStrategy();
                            break;
                        
                        case SmsStrategyEnum.UFONE:
                        case SmsStrategyEnum.UNKNOWN:
                        case SmsStrategyEnum.ZONG:
                        
                        default:  s =new TestSmsStrategy();
                            break;
                    }
                //});
            //task.Start();
            //await task;
            return s;
        }
    }
}