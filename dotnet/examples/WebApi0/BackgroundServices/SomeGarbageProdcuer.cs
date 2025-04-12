
namespace WebApi0.BackgroundServices
{

    public class SomeGarbageProdcuer : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (true)
            {

                for (int i = 2; i < 1000; i++)
                {
                    var v = new SomeHeavyObject()
                    {
                        ValueAsString = (new int[i]).Select(x => x.ToString()).Aggregate((x, y) => x + y)
                    };
                }
                await Task.Delay((int)Random.Shared.NextInt64(500, 5000));
            }
        }
    }

    public class SomeHeavyObject
    {
        public int Id { get; set; }
        public double ValueAsDouble { get; set; }
        public string ValueAsString { get; set; }
    }
}