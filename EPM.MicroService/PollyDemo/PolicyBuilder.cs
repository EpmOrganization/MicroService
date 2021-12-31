using Polly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PollyDemo
{
    public static class PolicyBuilder
    {
        public static IAsyncPolicy CreatePolly()
        {
            // 超时一秒
            var timeoutPolicy = Policy.TimeoutAsync(seconds: 1, onTimeoutAsync: (context, timespan, task) => 
            {
                Console.WriteLine("执行超时，抛出TimeoutRejectedException的异常");
                return Task.CompletedTask;
            });

            // 重试2次
            var retryPolicy = Policy.Handle<Exception>()
                            .WaitAndRetryAsync(
                             retryCount:2,// 重试2次
                             sleepDurationProvider:retryAttempt => TimeSpan.FromSeconds(2), // 每次间隔两秒
                             onRetry: (exception, timespan, retryCount, context) => 
                             {
                                 Console.WriteLine($"{DateTime.Now}-重试 {retryCount}次-抛出{exception.GetType()}");
                             });

            // 连续发生两次故障，就熔断3秒
            var circuitBreakerPolicy = Policy.Handle<Exception>()
                .CircuitBreakerAsync(
                  // 熔断前允许连续出现几次错误
                  exceptionsAllowedBeforeBreaking:2,
                  // 熔断时间
                  durationOfBreak:TimeSpan.FromSeconds(3),
                  // 熔断时触发 open
                  onBreak: (ex, breakDelay)=>
                  {
                      Console.WriteLine($"{DateTime.Now} - 断路器：开启状态（熔断时触发）");
                  },
                  // 熔断恢复时触发  close
                  onReset: () => 
                  {
                      Console.WriteLine($"{DateTime.Now} - 断路器：关闭状态（熔断恢复时触发）");
                  },
                  // 熔断时间到了之后触发，尝试放行少量（1次）的请求
                  onHalfOpen: () => 
                  {
                      Console.WriteLine($"{DateTime.Now} - 断路器：半开启状态（熔断时间到了之后触发）");
                  }
                );

            var fallbackPolicy = Policy.Handle<Exception>()
       .FallbackAsync(
                   fallbackAction: (cancellationToken) =>
                   {
                       Console.WriteLine("这是一个FallBack");
                       return Task.CompletedTask;
                   },
               onFallbackAsync: (exception) =>
               {
                   Console.WriteLine($"降级策略,  异常-> {exception.Message} ");
                   return Task.CompletedTask;
               });

            //// 回退策略，降级
            //var fallBackPolicy =Policy<string>.Handle<Exception>()
            //    .FallbackAsync(
                
            //    fallbackAction: () => 
            //    {
            //        Console.WriteLine("这是一个FallBack");
            //    },
            //    onFallbackAsync: (exception, context) => 
            //    {
            //        Console.WriteLine($"Fallback异常：{exception.GetType()}");
            //        return Task.CompletedTask;
            //    });

            // 策略从右到左依次进行调用
            // 首先判断调用是否超时，如果超时就会触发异常，发生超时故障，然后就触发重试策略
            // 如果重试两次中只要成功一次，就直接返回调用结果
            // 如果重试两次都失败，第三次再次失败，就会发生故障
            // 重试之后是断路器策略，所以这个故障会被断路器接收，当断路器收到两次故障，就会触发熔断，也就是说断路器开启
            // 断路器开启的3秒内，任何故障或者操作，都会通过断路器到达回退策略，触发降级操作
            // 3秒后，断路器进入到半开启状态，操作可以正常执行

            return fallbackPolicy.WrapAsync( Policy.WrapAsync(circuitBreakerPolicy, retryPolicy, timeoutPolicy));
        }
    }
}
