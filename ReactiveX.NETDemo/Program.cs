using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace ReactiveX.NetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("欢迎探索响应式编程 ...");

            CreateObservable();
            Console.Read();
        }

        /// <summary>
        /// 创建可观察数据
        /// </summary>
        private static void CreateObservable()
        {
            // 1. 创建观察数据
            _ = Observable.Create<int>(observer =>
            {
                // 向观察者提供数据
                Enumerable.Range(1, 10)
                    .ToList()
                    .ForEach(index =>
                        observer.OnNext(index));

                // 完成
                observer.OnCompleted();

                return Disposable.Empty;
            });

            // 2. 延迟创建 (当有观察者订阅时才创建)
            _ = Observable.Defer(() =>
            {
                return Enumerable.Range(1, 10).ToObservable();
            });

            // 3. 迭代类型的可观察序列
            _ = Observable.Generate(
                0,                  // 初始化状态    
                i => i < 10,   // 循环条件
                i => i + 1,     // 迭代步长
                i => i * 2);     // 迭代返回数据

            // 4. 指定区间的可观察序列
            _ = Observable.Range(0, 10).Select(i => i * 2);

            // 5. 特殊用途的可观察序列
            // 5.1 创建单个元素的可观察序列
            _ = Observable.Return("Hello World");
            // 5.2 创建一个空的永远不会结束的可观察序列
            _ = Observable.Never<string>();
            // 5.3 创建一个抛出指定异常的可观察序列
            _ = Observable.Throw<ApplicationException>(new ApplicationException());
            // 5.4 创建一个空的立即结束的可观察序列
            _ = Observable.Empty<string>();
        }

        /// <summary>
        /// 基础响应式演示
        /// </summary>
        private static void BasicReactiveDemo()
        {
            // 使用数据集合创建观察数据
            IObservable<int> nums = Enumerable.Range(1, 10).ToObservable();
            // 创建订阅者
            IDisposable subscription = nums.Where(num => num > 5).Subscribe(Console.WriteLine);
            subscription.Dispose();
        }
    }
}
