using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace TestProject1;

public class UnitTest1
{
    private readonly ITestOutputHelper _output;
    
    public UnitTest1(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Seperate_Task_Creaion_From_Starting()
    {
        // Create the task 
        var t = new Task(() => SomeWork(_output));

        // Start the task 
        t.Start();

        // Wait for completion
        t.Wait();
    }

    // For performance reasons, the Task.Run or TaskFactory.StartNew method is the preferred mechanism
    // for creating and scheduling computational tasks,
    [Fact]
    public void Create_Task_And_Start_Immediatly()
    {
        var t = Task.Run(() => SomeWork(_output));
        t.Wait();
    }

    [Fact]
    public void Create_Task_With_Factory()
    {
        var factory = new TaskFactory();
        var t = factory.StartNew(() => SomeWork(_output));
        t.Wait();
        
        // Static variant 
        var t2 = Task.Factory.StartNew(() => SomeWork(_output));
        t2.Wait();
    }
    
    private void SomeWork(ITestOutputHelper output)
    {
        for (var i = 0; i < 5; i++)
        {
            output.WriteLine($"Sleeping {i}");
            Thread.Sleep(TimeSpan.FromSeconds(1));
        }
    }
}