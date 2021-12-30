using Polly;
using Polly.Timeout;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PollyDemo
{
    internal class Program
    {
        static  void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            // 使用Polly分为三个步骤

            // 1、定义故障,当发生了ArgumentException异常的时候会触发策略
            //Policy.Handle<ArgumentException>()
            //    // 2、指定策略  Fallback表示回退 即降级
            //    .Fallback(() =>
            //    {
            //        // 这里实际上就是降级方法
            //        Console.WriteLine("Policy Fallback");
            //    })
            //    // 3、执行策略
            //    .Execute(() => 
            //    {
            //        // 这里就是执行业务方法，跨服务调用写在这里
            //        // 是可以和HttpClient结合使用的
            //        Console.WriteLine("DoSomething");
            //        throw new ArgumentException("Hello");
            //    });

            // // 单个异常的故障
            // Policy.Handle<Exception>();

            // // 带条件的异常
            // Policy.Handle<Exception>(ex => ex.Message == "Hello")
            //     .Fallback(() => 
            //     {
            //         Console.WriteLine("single condition policy");
            //     })
            //     .Execute(() => {
            //         Console.WriteLine("with condition");
            //         throw new ArgumentException("Hello");
            //     });

            // // 多个异常类型
            // Policy.Handle<HttpRequestException>()
            //     .Or<ArgumentException>()
            //     .Or<AggregateException>();

            // // 多个异常类型带条件
            // Policy.Handle<HttpRequestException>(ex => ex.Message == "Hello HttpRequestException")
            //.Or<ArgumentException>(ex => ex.Message == "Hello ArgumentException")
            //.Or<AggregateException>(ex => ex.Message == "Hello AggregateException")
            //.Fallback(() =>
            //{
            //    Console.WriteLine("Hello Fallback");
            //})
            //.Execute(() => 
            //{
            //    Console.WriteLine("HttpRequestException");
            //    throw new HttpRequestException("HttpRequestException");
            //});


            // 弹性策略  响应式
            // 超时策略
            //var timeoutPolicy = Policy.TimeoutAsync(1, (context, timespan, task) =>
            //{
            //    Console.WriteLine("超时了，抛出TimeoutRejectedException异常。");
            //    return Task.CompletedTask;
            //});

     //       // 重试 默认重试一次
     //       Policy.Handle<HttpRequestException>().Retry();
     //       // 重试 默认3次
     //       Policy.Handle<HttpRequestException>().Retry(3);
     //       // 重试 一直重试，直到成功，非高并发情况下使用
     //       Policy.Handle<HttpRequestException>().RetryForever();


     //       // 重试且等待策略  默认重试一次
     //       var retryPolicy = Policy.Handle<HttpRequestException>().Or<TimeoutException>().Or<TimeoutRejectedException>()
     //.WaitAndRetryAsync(
     //    retryCount: 2,
     //    sleepDurationProvider: retryAttempt =>
     //    {
     //        var waitSeconds = TimeSpan.FromSeconds(Math.Pow(2, retryAttempt - 1));
     //        Console.WriteLine(DateTime.Now.ToString() + "----------------重试策略:[" + retryAttempt + "], 等待 " + waitSeconds + "秒!");
     //        return waitSeconds;
     //    });

     //       // 熔断策略
     //       var circuitBreakerPolicy = Policy.Handle<HttpRequestException>().Or<TimeoutException>().Or<TimeoutRejectedException>()
     //  .CircuitBreakerAsync(
     //      // 熔断前允许连续出现几次错误 
     //      exceptionsAllowedBeforeBreaking: 2,
     //      // 熔断时间  这里是3秒
     //      durationOfBreak: TimeSpan.FromSeconds(3),
     //      // 熔断时触发  open状态
     //      onBreak: (ex, breakDelay) =>
     //      {
     //          Console.WriteLine(DateTime.Now.ToString() + "----------------断路器->断路 " + breakDelay.TotalMilliseconds + "毫秒! 异常: ", ex.Message);
     //      },
     //      // 熔断恢复时触发  close状态
     //      onReset: () =>
     //      {
     //          Console.WriteLine(DateTime.Now.ToString() + "----------------断路器->好了! 再次闭合电路.");
     //      },
     //      // 在熔断时间到了之后触发 half-open
     //      onHalfOpen: () =>
     //      {
     //          Console.WriteLine(DateTime.Now.ToString() + "----------------断路器->半开，下一个呼叫是测试.");
     //      }
     //  );

     //       // 高级熔断策略
     //       // 如果在故障采样持续时间内，发生故障的比例超过故障阈值，则发生熔断
     //       // 前提是在此期间，通过断路器的操作的数量至少是最小吞吐量
     //       Policy.Handle<Exception>()
     //           .AdvancedCircuitBreaker(
     //             failureThreshold: 0.5,// 故障阈值 ，50%
     //             samplingDuration: TimeSpan.FromSeconds(10), // 故障采样时间，10秒
     //             minimumThroughput:8,// 最小吞吐量，10秒内最少执行了8次操作
     //             durationOfBreak:TimeSpan.FromSeconds(30) // 熔断时间，30秒
     //           );


     //       // 降级策略
     //       var fallbackPolicy = Policy<string>.Handle<Exception>()
     //  .FallbackAsync(
     //          fallbackValue: "替代数据",
     //          onFallbackAsync: (exception, context) =>
     //          {
     //              Console.WriteLine("降级策略,  异常->" + exception.Exception.Message + ", 返回替代数据.");
     //              return Task.CompletedTask;
     //          });


     //       // 超时策略  服务调用  主动式
     //       // 这里设置的是1秒超时
     //       var timeoutPolicy = Policy.TimeoutAsync(1, (context, timespan, task) =>
     //       {
     //           Console.WriteLine("超时了，抛出TimeoutRejectedException异常。");
     //           return Task.CompletedTask;
     //       });

     //       // 舱壁隔离，通过控制并发数量来管理负载  主动式
     //       // 最大并发数为12，超过12个都拒绝
     //       // maxQueuingActions 最大等待队列数量
     //       Policy.Bulkhead(12,maxQueuingActions:20);


            // 主动式降级策略不需要定义故障  没有故障也可以使用
            // 被动式降级策略需要定义故障

            // 策略包装 ，也叫策略组合

            // 重试策略
            var retry = Policy.Handle<Exception>().Retry(3, onRetry: (exception, i) => 
            {
                Console.WriteLine($"retryCount:{i}");
            });
            // 降级策略
            var fallback = Policy.Handle<Exception>()
                           .Fallback(() => 
                           {
                               Console.WriteLine("Policy Fallback");
                           });

            // 策略组合
            // 如果重试3次，仍然发生故障，就降级  策略执行顺序为从右到左
            Policy.Wrap(fallback, retry)
                .Execute(() => 
                {
                    Console.WriteLine("Policy Begin");
                    throw new ArgumentOutOfRangeException();
                });

            //循环执行50次，策略的执行是非常简单的，唯一需要注意的就是调用的顺序：如下是依次从右到左进行调用，
            //首先是进行超时的判断，一旦超时就触发TimeoutRejectedException异常，
            //然后就进入到了重试策略中，如果重试了一次就成功了，那就直接返回，不再触发其他策略，
            //否则就进入到熔断策略中：
            //Task.Run(async () =>
            //{
            //    for (int i = 0; i < 50; i++)
            //    {
            //        Console.WriteLine(DateTime.Now.ToString() + "----------------开始第[" + i + "]-----------------------------");
            //        var res = await fallbackPolicy
            //        .WrapAsync(Policy.WrapAsync(circuitBreakerPolicy, retryPolicy, timeoutPolicy))
            //        .ExecuteAsync(() => new Test().HttpInvokeAsync());
            //        Console.WriteLine(DateTime.Now.ToString() + "--------------- 开始[" + i + "]->响应" + ": Ok->" + res);
            //        await Task.Delay(1000);
            //        Console.WriteLine($"当前断路器状态:{circuitBreakerPolicy.CircuitState.ToString()}");
            //        Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");
            //    }
            //});
            Console.ReadKey();
        }
    }
}
