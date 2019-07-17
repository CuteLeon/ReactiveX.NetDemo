using System;
using System.Linq;
using System.Reactive.Linq;

namespace ReactiveX.NetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("欢迎探索响应式编程 ...");

            // 使用数据集合创建观察者
            IObservable<int> nums = Enumerable.Range(1, 10).ToObservable();
            // 创建订阅者
            IDisposable subscription = nums.Where(num => num > 5).Subscribe(Console.WriteLine);
            subscription.Dispose();

            Console.Read();
        }
    }
}
