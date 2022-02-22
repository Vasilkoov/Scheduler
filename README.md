# Scheduler
Scheduler based on Quartz.NET

Usage:

```csharp

var scheduler = new Scheduler();

scheduler.AddJob(() =>
{
    Console.WriteLine("action job");
}, "*/30 * * ? * *");


scheduler.AddAsyncJob(async () =>
{
    await Task.Delay(10);
    await Task.Run(() => Console.WriteLine("async job"));
}, "*/30 * * ? * *");

```