using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace OilSimulationModel
{ 
    public class MultiThreadModel
    {
        private stMultiTread Parame;//参数 
        //构造函数
        public MultiThreadModel(stMultiTread parame)
        {
            this.Parame = parame;
            Parame.thread = new Thread(new ThreadStart(Run));
            //Parame.thread.Name = "DecoratorThread";
        }
        //
        public void Start()
        {
            if (Parame.thread != null)
            {
                Parame.thread.Start();
            }
        }
        //线程执行方法
        public void Run()
        {
            
            //线程执行完成
            Parame.bThreadBusy = false;
        }
    }
}
